import React, { useState, useEffect } from 'react';
import HeaderTemplate from "../../components/header-page";
import koiImage from "./path-to-koi-image.jpg";
import "./index.css";
import FooterPage from "../../components/footer-page";
import { Carousel } from "antd";
import KoiImage1 from "./hinh-nen-ca-chep-2k-dep-cho-may-tinh_025211326.jpg";
import KoiImage2 from "./animals-aquatic-animal-fish-koi-fish.jpg";

const contentStyle = {
  margin: 0,
  height: "160px",
  color: "#fff",
  lineHeight: "160px",
  textAlign: "center",
  background: "#364d79",
};

function Home() {
 

  return (
    <>
      <HeaderTemplate></HeaderTemplate>
      <div className="home-content">
        <body>
        {/* Trending Feature */}
        <div className="container">
          <h2>Tính năng nổi bật</h2>
          <div className="Card-container">
            <div className="Card">
              <img
                src={KoiImage1}
                alt="Selecting Koi fish according to your Feng Shui element"
              ></img>
              <a href="#">
                {" "}
                <h3>Tư vấn cá và hồ theo bản mệnh</h3>
              </a>
            </div>
            <div className="Card">
              <img src={koiImage} alt="Post Advertisement Function"></img>
              <a href="#">
                {" "}
                <h3>Tính độ tương hợp của cá và hồ theo bản mệnh</h3>
              </a>
            </div>
            <div className="Card">
              <img src={KoiImage2} alt="Calculate compatibility"></img>
              <a href="#">
                {" "}
                <h3>Đăng tin quảng cáo</h3>
              </a>
            </div>
          </div>
        </div>
        {/* Blog */}
        <div id="feature" className="container">
          <h2>Blog</h2>
          <div className="Card-container">
            <div className="Card">
              <img
                src={koiImage}
                alt="Selecting Pond according to your Feng Shui element"
                className="img-feature"
              ></img>
              <a href="#">
                {" "}
                <h3>Selecting Koi fish according to your Feng Shui element</h3>
              </a>
            </div>
            <div className="Card">
              <img
                src={koiImage}
                alt="Selecting Koi fish according to your Feng Shui element"
              ></img>
              <a href="#">
                {" "}
                <h3>Selecting Koi fish according to your Feng Shui element</h3>
              </a>
            </div>
            <div className="Card">
              <img src={koiImage} alt="Post Advertisement Function"></img>
              <a href="#">
                {" "}
                <h3>Selecting Koi fish according to your Feng Shui element</h3>
              </a>
            </div>
          </div>
        </div>
      </body>
      <FooterPage></FooterPage>
      </div>
    </>
  );
}

export default Home;
