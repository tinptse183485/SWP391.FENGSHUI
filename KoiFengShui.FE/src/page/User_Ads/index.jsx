import React, { useState, useEffect } from "react";
import "./index.css";
import { Layout, Menu, Table, Button } from "antd";
import { Link, useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import Header_page from "../../components/header-page";
import api from "../../config/axios";

const { Content } = Layout;

const User_Ads = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [advertisements, setAdvertisements] = useState([]);
  const [filteredAds, setFilteredAds] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState("all");

  const menuItems = [
    { key: "all", label: "Tất cả" },
    { key: "Draft", label: "Bản nháp" },
    { key: "Pending", label: "Chờ duyệt" },
    { key: "Approved", label: "Đã duyệt" },
    { key: "Canceled", label: "Đã hủy" },
    { key: "Refunded", label: "Đã hoàn tiền" },
  ];

  useEffect(() => {
    fetchAdvertisements();
  }, []);

  useEffect(() => {
    filterAdvertisements(selectedStatus);
  }, [selectedStatus, advertisements]);

  const fetchAdvertisements = async () => {
    try {
      const response = await api.get("Advertisement/GetAllAdvertisement"); // Replace with your actual API endpoint
      setAdvertisements(response.data);
    } catch (error) {
      console.error("Error fetching advertisements:", error);
    }
  };

  const filterAdvertisements = (status) => {
    if (status === "all") {
      setFilteredAds(advertisements);
    } else {
      const filtered = advertisements.filter((ad) => ad.status === status);
      setFilteredAds(filtered);
    }
  };

  const handleMenuClick = (e) => {
    setSelectedStatus(e.key);
  };

  const handleCreate = () => {
    navigate("/create-ads", { state: { advertisement: null } });
  };
  const handleChoosePackage = () => {
    navigate("/choose-package");
  };

  const columns = [
    {
      title: "Tiêu đề",
      dataIndex: "heading",
      key: "heading",
    },
    {
      title: "Trạng thái",
      dataIndex: "status",
      key: "status",
    },
    {
      title: "Ngày tạo",
      dataIndex: "createdAt",
      key: "createdAt",
    },
    {
      title: "Hành động",
      key: "action",
      render: (_, record) =>
        record.status === "Draft" ? (
          <Button onClick={() => handleUpdateDraft(record)}>
            Cập nhật bản nháp
          </Button>
        ) : null,
    },
  ];

  const handleEditAd = (adId) => {
    navigate(`/edit-ad/${adId}`);
  };

  const handleUpdateDraft = (advertisement) => {
    navigate("/create-ads", { state: { advertisement } });
  };

  return (
    <Layout style={{ minHeight: "100vh", backgroundColor: "white" }}>
      <Header_page />
      <Layout>
        <Content style={{ margin: "16px" }}>
          <div
            className="container-ads"
            style={{ padding: 24, background: "#fff", minHeight: 360 }}
          >
            <div className="Heading">
              <h1>Chào mừng đến với trang Quảng cáo của bạn!</h1>
              <button onClick={handleCreate} className="btn-create">
                Tạo bản nháp
              </button>
            </div>
            <Menu
              className="menu"
              theme="light"
              mode="horizontal"
              selectedKeys={[selectedStatus]}
              onClick={handleMenuClick}
            >
              {menuItems.map((item) => (
                <Menu.Item key={item.key}>{item.label}</Menu.Item>
              ))}
            </Menu>
            <Table columns={columns} dataSource={filteredAds} rowKey="id" />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default User_Ads;
