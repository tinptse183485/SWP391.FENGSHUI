import React, { useEffect, useState } from "react";
import api from "../../../config/axios";
import { Table, Button, message, Image, Typography, Space } from "antd";
import { useNavigate } from "react-router-dom";
const { Text } = Typography;

const Ads = () => {
  const navigate = useNavigate();
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu
  const fetchData = async () => {
    const response = await api.get("Advertisement/GetAllAdvertisement");
    setData(response.data);
  };
  useEffect(() => {
    fetchData();
  }, []);
  const handleUpdateStatus = async (adId, status) => {
    try {
      await api.put(
        `Advertisement/UpdateAdvertisementStatus?adId=${adId}&status=${status}`
      );
      message.success(
        `Đã cập nhật trạng thái thành của bài quảng cáo ${adId} thành ${status}`
      );
      fetchData(); // Tải lại dữ liệu sau khi cập nhật
    } catch (error) {
      message.error("Có lỗi xảy ra khi cập nhật trạng thái");
    }
  };

  const statusPriority = {
    Pending: 1,
    Approved: 2,
    Canceled: 3,
    Refunded: 4
  };
  const columns = [
    {
      title: "AdID",
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
        const isRefunded = record.status === "Refunded";
        return (
          <Space size="small" direction="vertical">
            <Button
              type="primary"
              onClick={() => handleUpdateStatus(record.adId, "Approved")}
              disabled={record.status === "Approved" || isRefunded}
            >
              Phê duyệt
            </Button>
            {record.status === "Canceled" ? (
              <Button
                type="primary"
                danger
                onClick={() => handleUpdateStatus(record.adId, "Refunded")}
                disabled={isRefunded}
              >
                Hoàn trả
              </Button>
            ) : (
              <Button
                type="primary"
                danger
                onClick={() => handleUpdateStatus(record.adId, "Canceled")}
                disabled={isRefunded}
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
      <Table dataSource={data} columns={columns}/>
    </div>
  );
};

export default Ads;
