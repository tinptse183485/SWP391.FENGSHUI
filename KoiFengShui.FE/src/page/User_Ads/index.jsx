import React from 'react';
import './index.css';
import { Layout, Menu } from 'antd';
import { Link, useLocation } from 'react-router-dom';
import KoiImage from './koi.jpg';
const { Sider, Content } = Layout;
import Header_page from '../../components/header-page';
const User_Ads = () => {
  const location = useLocation();

  const menuItems = [
    { label: 'Bản nháp' },
    { label: 'Chờ duyệt' },
    { label: 'Đã duyệt' },
    {  label: 'Đã đăng' },
    { label: 'Đã hoàn tiền' },
  ];

  return (
    <Layout style={{ minHeight: '100vh', backgroundColor: 'white' }}>
      <Sider style={{ backgroundColor: 'white' }}>
        <div className="logo"> {/* Logo có thể được thêm vào đây */} 
            <img src={KoiImage}></img>
        </div>
        <Menu theme="light" mode="inline" selectedKeys={[location.pathname]}>
          {menuItems.map((item) => (
            <Menu.Item key={item.key} icon={item.icon}>
              <Link to={item.key}>{item.label}</Link>
            </Menu.Item>
          ))}
        </Menu>
      </Sider>
      <Layout>
        <Content style={{ margin: '16px' }}>
          <div style={{ padding: 24, background: '#fff', minHeight: 360 }}>
            {/* Nội dung chính của trang User_Ads sẽ nằm ở đây */}
            <h1>Chào mừng đến với trang User Ads!</h1>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default User_Ads;