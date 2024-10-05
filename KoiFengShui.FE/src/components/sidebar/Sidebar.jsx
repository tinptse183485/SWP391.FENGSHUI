import React, { useState } from 'react';
import { Layout, Menu, Button } from 'antd';
import {
  HomeOutlined,
  CalculatorOutlined,
  TeamOutlined,
  BarChartOutlined,
  SettingOutlined,
  LogoutOutlined,
} from '@ant-design/icons';
import { Link, useLocation } from 'react-router-dom';
import './Sidebar.css';
import KoiImage from './koi.jpg';
const { Sider } = Layout;

const Sidebar = () => {
  const [collapsed, setCollapsed] = useState(false);
  const location = useLocation();

  const menuItems = [
    { key: '/dashboard', icon: <HomeOutlined />, label: 'DASHBOARD' },
    { key: '/user', icon: <TeamOutlined />, label: 'MANAGE USER' },
    { key: '/ads', icon: <CalculatorOutlined />, label: 'MANAGE ADS' },
    { key: '/blog', icon: <CalculatorOutlined />, label: 'BLOG' },
    { key: '/koi', icon: <BarChartOutlined />, label: 'KOI FISH' },
    { key: '/pond', icon: <BarChartOutlined />, label: 'POND' },
  ];

  return (
    <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
      <div className="logo">
        {collapsed ? (
          <img src={KoiImage} alt="Logo" />
        ) : (
            <img src={KoiImage} alt="Logo" />
        )}
      </div>
      <Menu theme="dark" mode="inline" selectedKeys={[location.pathname]}>
        {menuItems.map((item) => (
          <Menu.Item key={item.key} icon={item.icon}>
            <Link to={item.key}>{item.label}</Link>
          </Menu.Item>
        ))}
      </Menu>
      <div className="logout-container">
        <Link to="/">
         <Button type="primary" icon={<LogoutOutlined />} onClick={() => console.log('Logout clicked')}>
          {collapsed ? '' : 'Logout'}
        </Button>
        </Link>
       
      </div>
    </Sider>
  );
};

export default Sidebar;
