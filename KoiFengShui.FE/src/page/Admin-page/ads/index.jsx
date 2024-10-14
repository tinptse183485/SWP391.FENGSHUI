import React, { useEffect, useState } from "react";
import api from "../../../config/axios";
import { Table, Button, message, Image, Typography, Space, Menu } from "antd";
import { useNavigate } from "react-router-dom";
const { Text } = Typography;

const Ads = () => {
  const navigate = useNavigate();
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu
  const [filteredData, setFilteredData] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState("all");

  const menuItems = [
    { key: "all", label: "Tất cả" },
    { key: "Pending", label: "Đang chờ" },
    { key: "Approved", label: "Đã duyệt" },
    { key: "Canceled", label: "Đã hủy" },
    { key: "Refunded", label: "Đã hoàn tiền" },
  ];

  const fetchData = async () => {
    const response = await api.get("Advertisement/GetAllAdvertisement");
    setData(response.data);
    filterAds("all");
  };

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    filterAds(selectedStatus);
  }, [selectedStatus, data]);

  const filterAds = (status) => {
    if (status === "all") {
      setFilteredData(data);
    } else {
      const filtered = data.filter((ad) => ad.status === status);
      setFilteredData(filtered);
    }
  };

  const handleMenuClick = (e) => {
    setSelectedStatus(e.key);
  };

  const statusPriority = {
    Pending: 1,
    Approved: 2,
    Canceled: 3,
    Refunded: 4
  };
  const columns = [
    {
      title: "Quảng cáo",
      dataIndex: "adId",
      key: "adId",
      render: (text) => <Text strong>{text}</Text>,
    },
    {
      title: "Tiêu đề",
      dataIndex: "heading",
      key: "heading",
      ellipsis: true,
    },
    {
      title: "Hình ảnh",
      dataIndex: "image",
      key: "image",
      render: (image) => (
        <Image
          src={image}
          alt="Ad"
          width={80}
          height={80}
          style={{ objectFit: "cover", borderRadius: "4px" }}
        />
      ),
    },
    {
      title: "Nội dung",
      key: "content",
      render: (_, record) => (
        <Button
          type="link"
          onClick={() => navigate(`/advertisement-detail/${record.adId}`)}
        >
          Xem chi tiết
        </Button>
      ),
    },
    {
      title: "Trạng thái",
      dataIndex: "status",
      key: "status",
      render: (status) => (
        <Text
          style={{
            color:
            status === "Approved"
            ? "#52c41a"
            : status === "Refunded"
            ? "#faad14"
            : status === "Canceled"
            ? "#f5222d"
            : "#1890ff", // Màu cho trạng thái Pending
          }}
        >
          {status}
        </Text>
      ),
      sorter: (a, b) => statusPriority[a.status] - statusPriority[b.status],
      defaultSortOrder: 'ascend',
    },
    {
      title: "Action",
      key: "action",
      width: 200,
      render: (_, record) => {
        const isApproved = record.status === "Approved";
        const isRefunded = record.status === "Refunded";
        const isCanceled = record.status === "Canceled";
        const isDisabled = isApproved || isRefunded || isCanceled;
        return (
          <Space size="small" direction="vertical">
            <Button
              type="primary"
              onClick={() => handleUpdateStatus(record.adId, "Approved")}
              disabled={isDisabled}
            >
              Phê duyệt
            </Button>
            {isCanceled ? (
              <Button
                type="primary"
                danger
                onClick={() => handleUpdateStatus(record.adId, "Refunded")}
                disabled={isApproved || isRefunded}
              >
                Hoàn trả
              </Button>
            ) : (
              <Button
                type="primary"
                danger
                onClick={() => handleUpdateStatus(record.adId, "Canceled")}
                disabled={isApproved || isRefunded}
              >
                Hủy bỏ
              </Button>
            )}
          </Space>
        );
      },
    },
  ];
  return (
    <div>
      <h1>Quản lý quảng cáo</h1>
      <Menu
        mode="horizontal"
        selectedKeys={[selectedStatus]}
        onClick={handleMenuClick}
        style={{ marginBottom: 16 }}
      >
        {menuItems.map((item) => (
          <Menu.Item key={item.key}>{item.label}</Menu.Item>
        ))}
      </Menu>
      <Table dataSource={filteredData} columns={columns} />
    </div>
  );
};

export default Ads;
