import { Link } from "react-router-dom";
import "./index.css";
import koiImage from "./path-to-koi-image.jpg";
import Header from "./Header.png";
import Koi2 from "./Koi2.jpg";
import { Carousel } from "antd";
import { useNavigate } from "react-router-dom";

const contentStyle = {
  height: "500px",
  color: "#fff",
  lineHeight: "500px",
  textAlign: "center",
  background: "#364d79",
};

function HeaderTemplate() {
  const navigate = useNavigate();

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
            <h1 className="website-name">KOI PHONG THỦY</h1>
            <p className="website-description">
          Web phong thủy về cá Koi uy tín nhất VN
            </p>
          </div>
        </div>
       
        <div className="user-actions">
        <nav className="main-nav">
          <ul>
            <li>
              <a href="#home">Trang chủ</a>
            </li>
            <li>
              <a href="#about">About Us</a>
            </li>
            <li>
              <a href="#Advertisements" onClick={handleScrollToAdvertisements}>Quảng cáo</a>
            </li>
            <li>
              <a href="#blog">Blog</a>
            </li>
            <li>
              <a href="#contact">Liên hệ</a>
            </li>
          </ul>
        </nav>
          <button className="login-btn">
            <Link to="login">Log in / Sign up</Link>
          </button>
        </div>
      </div>
     
      <Carousel autoplay className="carousel">
        <div className="first">
          <img
            style={contentStyle}
            src={Header}
            alt="header-img"
            className="header-img"
          />
        </div>
        <div>
          <img
            style={contentStyle}
            src={Koi2}
            alt="header-img"
            className="header-img"
          />
        </div>
        <div>
          <img
            style={contentStyle}
            src={Koi2}
            alt="header-img"
            className="header-img"
          />
        </div>
        <div>
          <img
            style={contentStyle}
            src={Header}
            alt="header-img"
            className="header-img"
          />
        </div>
      </Carousel>
    </div>
  );
}

export default HeaderTemplate;
