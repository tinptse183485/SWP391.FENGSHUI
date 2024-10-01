import { Link } from "react-router-dom";
import "./index.css";
import koiImage from "./path-to-koi-image.jpg";
import Header from "./Header.png";
import Koi2 from "./Koi2.jpg";

function HeaderTemplate() {
  return (
    <div>
      <div className="top-bar">
        <div className="logo-container">
          <img src={koiImage} alt="Website Logo" className="logo" />
          <div className="website-info">
            <h1 className="website-name">Name of Website</h1>
            <p className="website-subtitle">Title of website</p>
          </div>
        </div>
        <div className="user-actions">
          <button className="calculate-btn">Calculate Feng Shui element</button>
          <button className="login-btn">
            <Link to="login">Log in/Sign up</Link>
          </button>
        </div>
      </div>
      <div className="description-bar">
        <p className="website-description">Description of website</p>
        <nav className="main-nav">
          <ul>
            <li>
              <a href="#home">Home Page</a>
            </li>
            <li>
              <a href="#about">About Us</a>
            </li>
            <li>
              <a href="#product">Product</a>
            </li>
            <li>
              <a href="#blog">Blog</a>
            </li>
            <li>
              <a href="#contact">Contact</a>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  );
}

export default HeaderTemplate;
