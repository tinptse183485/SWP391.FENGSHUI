import { useEffect, useState } from "react";
import {
  DesktopOutlined,
  FileOutlined,
  PieChartOutlined,
  TeamOutlined,
  UserOutlined,
  LogoutOutlined,
} from "@ant-design/icons";
import { Breadcrumb, Button, Layout, Menu, theme } from "antd";
import { Link, Outlet } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import KoiImage from './koi.jpg';
import "./index.css";
const { Header, Content, Footer, Sider } = Layout;
function getItem(label, key, icon, children) {
  return {
    key,
    icon,
    children,
    label: <Link to={`/dashboard/${key}`}>{label}</Link>,
  };
}
const items = [
  getItem("Dashboard", "AdminDashboard", <PieChartOutlined />),
  getItem("Manage Ads", "ads", <PieChartOutlined />),
  getItem("Manage User", "user", <PieChartOutlined />),
  getItem("Manage Blog", "blog", <PieChartOutlined />),
  getItem("Manage Koi", "koi", <PieChartOutlined />),
  getItem("Manage Pond", "pond", <PieChartOutlined />),
];
const Dashboard = () => {
  const navigate = useNavigate();
  const [userId, setUserId] = useState(null);
  useEffect(() => {
    const user = localStorage.getItem("userId");
    if (user) {
      setUserId(user);
    }
  }, []);
  const handleLogout = () => {
    localStorage.removeItem("userId");
    localStorage.removeItem("token");
    setUserId(null);
    navigate("/");
  };
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  return (
    <Layout
      style={{
        minHeight: "100vh",
        
      }}
    >
      <Sider
        collapsible
        collapsed={collapsed}
        onCollapse={(value) => setCollapsed(value)}
      >
        <div className="logo">
        {collapsed ? (
          <img src={KoiImage} alt="Logo" />
        ) : (
            <img src={KoiImage} alt="Logo" />
        )}
      </div>
        <div className="demo-logo-vertical" />
        
        <Menu
          theme="dark"
          defaultSelectedKeys={["1"]}
          mode="inline"
          items={items}
          style={{
            marginTop: "50px",
          }}
        />
        
        <Button
        style={{
          marginTop: "100px",
        }}
          type="primary"
          icon={<LogoutOutlined />}
          onClick={handleLogout}
        >
          {collapsed ? "" : "Logout"}
        </Button>
      </Sider>

      <Layout>
        <Header
          style={{
            padding: 0,
            background: colorBgContainer,
          }}
        />
        <Content
          style={{
            margin: "0 16px",
          }}
        >
          <Breadcrumb
            style={{
              margin: "16px 0",
            }}
          >
            <Breadcrumb.Item>User</Breadcrumb.Item>
            <Breadcrumb.Item>{userId}</Breadcrumb.Item>
          </Breadcrumb>
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            <Outlet />
          </div>
        </Content>
        <Footer
          style={{
            textAlign: "center",
          }}
        >
        </Footer>
      </Layout>
    </Layout>
  );
};
export default Dashboard;
