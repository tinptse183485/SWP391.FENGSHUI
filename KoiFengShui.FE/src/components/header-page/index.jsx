import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./index.css";
import koiImage from "./path-to-koi-image.jpg";
import { Button, Dropdown, Menu } from "antd";
import { UserOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";

function HeaderTemplate() {
  const navigate = useNavigate();
  const [userId, setUserId] = useState(null);
  const [userRole, setUserRole] = useState(null);
  const [dropdownVisible, setDropdownVisible] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("userId");
    const role = localStorage.getItem("role");
    if (user) {
      setUserId(user);
      setUserRole(role);
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("userId");
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    setUserId(null);
    navigate("/");
  };

  const handleMenuClick = (e) => {
    if (e.key === "logout") {
      handleLogout();
    }
    setDropdownVisible(false);
  };

  const menu = (
    <Menu onClick={handleMenuClick}>
      {userRole === "Admin" && (
        <Menu.Item key="dashboard">
          <Link to="/dashboard">Dashboard</Link>
        </Menu.Item>
      )}
      <Menu.Item key="logout">Đăng xuất</Menu.Item>
    </Menu>
  );

  const handleScrollToAdvertisements = (event) => {
    event.preventDefault();
    const advertisementsSection = document.getElementById("Advertisements");
    if (advertisementsSection) {
      advertisementsSection.scrollIntoView({ behavior: "smooth" });
    } else {
      // If the section is not on the current page, navigate to home and then scroll
      navigate("/", { state: { scrollTo: "Advertisements" } });
    }
  };

  return (
    <div>
      <div className="top-bar">
        <div className="logo-container">
          <img src={koiImage} alt="Website Logo" className="logo" />
          <div className="website-info">
            <Link
              style={{ textDecoration: "none" }}
              className="website-name"
              to="/"
            >
              <h1>KOI PHONG THỦY </h1>
            </Link>

            <p className="website-description">
              Web phong thủy về cá Koi uy tín nhất VN
            </p>
          </div>
        </div>

        <div className="user-actions">
          <nav className="main-nav">
            <ul>
              <li>
                <Link to="/">
                  <a href="#home">Trang chủ</a>
                </Link>
              </li>
              <li>
                <Link to="/ads-list">
                  <a href="#about">About Us</a>
                </Link>
              </li>
              <li>
                <a
                  href="#Advertisements"
                  onClick={handleScrollToAdvertisements}
                >
                  Quảng cáo
                </a>
              </li>
              <li>
                <Link to="/user-ads">
                  <a href="#blog">Blog</a>
                </Link>
              </li>
              <li>
                <a href="#contact">Liên hệ</a>
              </li>
            </ul>
          </nav>

          {userId ? (
            <Dropdown
              overlay={menu}
              visible={dropdownVisible}
              onVisibleChange={setDropdownVisible}
              trigger={["click"]}
            >
              <Button className="user-button">
                <UserOutlined /> {userId}
              </Button>
            </Dropdown>
          ) : (
            <Button className="login-btn">
              <Link to="login">Log in / Sign up</Link>
            </Button>
          )}
        </div>
      </div>
    </div>
  );
}

export default HeaderTemplate;
