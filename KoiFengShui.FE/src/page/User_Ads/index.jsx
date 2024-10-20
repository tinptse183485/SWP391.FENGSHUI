import React, { useState, useEffect } from "react";
import "./index.css";
import { Layout, Menu, Table, Button } from "antd";
import { Link, useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import api from "../../config/axios";
import HeaderTemplate from "../../components/header-page";

const { Content } = Layout;

const User_Ads = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [advertisements, setAdvertisements] = useState([]);
  const [filteredAds, setFilteredAds] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState("all");
  const [adsPackages, setAdsPackages] = useState([]);

  const menuItems = [
    { key: "all", label: "Tất cả" },
    { key: "Draft", label: "Bản nháp" },
    { key: "Pending", label: "Chờ duyệt" },
    { key: "Approved", label: "Đã duyệt" },
    { key: "Canceled", label: "Đã hủy" },
    { key: "Refunded", label: "Đã hoàn tiền" },
  ];
  const userId = localStorage.getItem("userId");

  useEffect(() =>  {
    fetchAdsPackages(); 
    fetchAdvertisements();
    
  }, []);

  useEffect(() => {
    if (adsPackages.length > 0) {
      fetchAdvertisements();
    }
  }, [adsPackages]);

  useEffect(() => {
    filterAdvertisements(selectedStatus);
  }, [selectedStatus, advertisements]);

  const fetchAdsPackages = async () => {
    try {
      const response = await api.get('AdsPackage/GetAllAdsPackage');
      setAdsPackages(response.data);
      console.log(adsPackages);
    } catch (error) {
      console.error("Error fetching ads packages:", error);
    }
  };

  const fetchAdvertisements = async () => {
    try {
      const response = await api.get(`Advertisement/GetAdvertisementByUserId?UserId=${userId}`);
      const adsWithExpirationDate = response.data.map(ad => {
        const adPackage = adsPackages.find(pkg => pkg.adId === ad.adId);
        return {
          ...ad,
          expirationDate: adPackage ? adPackage.expiredDate : null
        };
      });
      setAdvertisements(adsWithExpirationDate);
      console.log(adsWithExpirationDate);
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
      title: "Ngày hết hạn",
      dataIndex: "expirationDate",
      key: "expirationDate",
      render: (expirationDate) => {
        if (!expirationDate) return "N/A";
        const date = new Date(expirationDate);
        return date.toLocaleDateString("vi-VN", {
          year: 'numeric',
          month: '2-digit',
          day: '2-digit'
        });
      },
    },
    {
      title: "Hành động",
      key: "action",
      render: (_, record) => {
        const currentDate = new Date();
        const expirationDate = new Date(record.expirationDate);
        console.log(record.expirationDate);
        const sevenDaysAfterExpiration = new Date(expirationDate.getTime() + 7 * 24 * 60 * 60 * 1000);
        console.log(sevenDaysAfterExpiration);

        if (record.status === "Draft") {
          return (
            <Button onClick={() => handleUpdateDraft(record)}>
              Cập nhật bản nháp
            </Button>
          );
        } else if (record.status === "Approved" && currentDate > expirationDate && currentDate <= sevenDaysAfterExpiration) {
          return (
            <Button onClick={() => handleExtendAd(record.adId)}>
              Gia hạn
            </Button>
          );
        }
        return null;
      },
    },
  ];

  const handleEditAd = (adId) => {
    navigate(`/edit-ad/${adId}`);
  };

  const handleUpdateDraft = (advertisement) => {
    navigate("/create-ads", { state: { advertisement } });
  };

  const handleExtendAd = (adId) => {
    // Implement the logic to extend the advertisement
    console.log(`Extending advertisement with ID: ${adId}`);
    // You might want to navigate to an extension page or open a modal
    // For example:
    // navigate(`/extend-ad/${adId}`);
  };

  return (
    <>
    <HeaderTemplate/>
    <Layout style={{ minHeight: "100vh", backgroundColor: "white" }}>
      
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
    </>
  );
};

export default User_Ads;