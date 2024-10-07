import React from 'react';
import './index.css';
import { Layout, Menu } from 'antd';
import { Link, useLocation } from 'react-router-dom';
import KoiImage from './koi.jpg';
const { Sider, Content } = Layout;
import Header_page from '../../components/header-page';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
const User_Ads = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const menuItems = [
    { label: 'Bản nháp' },
    { label: 'Chờ duyệt' },
    { label: 'Đã duyệt' },
    {  label: 'Đã đăng' },
    { label: 'Đã hoàn tiền' },
  ];
  const handleCreate = () => {

    navigate('/create-ads');
  };

  return (
    
    <Layout style={{ minHeight: '100vh', backgroundColor: 'white' }}>
      <Header_page />
      
     
        
     
      <Layout>
        <Content style={{ margin: '16px' }}>
          <div className='container' style={{ padding: 24, background: '#fff', minHeight: 360 }}>
            {/* Nội dung chính của trang User_Ads sẽ nằm ở đây */}
            <div className='Heading'>
            <h1>Chào mừng đến với trang User Ads!</h1>
            <button onClick={handleCreate} className='btn-create'>Tạo bản nháp</button>
            </div>
            <Menu className='menu' theme="light" mode="horizontal" selectedKeys={[location.pathname]}>
          {menuItems.map((item) => (
            <Menu.Item key={item.key} icon={item.icon}>
              <Link to={item.key}>{item.label}</Link>
            </Menu.Item>
          ))}
        </Menu>
          </div>
        </Content>
      </Layout>
      
    </Layout>
  );
};

export default User_Ads;