import React, { useEffect, useState } from "react";
import Sidebar from "../../../components/sidebar/Sidebar";
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
              status === "Approve"
                ? "#52c41a"
                : status === "Refunded"
                ? "#faad14"
                : "#f5222d",
          }}
        >
          {status}
        </Text>
      ),
    },
    {
      title: "Action",
      key: "action",
      width: 200,
      render: (_, record) => (
        <Space size="small" direction="vertical">
          <Button
            type="primary"
            onClick={() => handleUpdateStatus(record.adId, "Approve")}
            disabled={record.status === "Approve"}
          >
            Phê duyệt
          </Button>
          {record.status === "Cancel" ? (
            <Button
              type="primary"
              danger
              onClick={() => handleUpdateStatus(record.adId, "Refunded")}
            >
              Hoàn trả
            </Button>
          ) : (
            <Button
              type="primary"
              danger
              onClick={() => handleUpdateStatus(record.adId, "Cancel")}
            >
              Hủy bỏ
            </Button>
          )}
        </Space>
      ),
    },
  ];
  return (
    <div className="dashboard-container">
      <Table dataSource={data} columns={columns} />
    </div>
  );
};

export default Ads;
