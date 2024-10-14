import React, { useState, useEffect, useRef } from 'react';
import { Button } from 'antd';

import { Link, useNavigate } from "react-router-dom";

import { useLocation } from 'react-router-dom';
import HeaderTemplate from "../../components/header-page";
import koiImage from "./path-to-koi-image.jpg";
import "./index.css";
import FooterPage from "../../components/footer-page";
import { Carousel } from "antd";
import KoiImage1 from "./hinh-nen-ca-chep-2k-dep-cho-may-tinh_025211326.jpg";
import KoiImage2 from "./animals-aquatic-animal-fish-koi-fish.jpg";
import api from "../../config/axios";


const contentStyle = {
  margin: 0,
  height: "160px",
  color: "#fff",
  lineHeight: "160px",
  textAlign: "center",
  background: "#364d79",
};

function Home() {
  const processedRef = useRef(false);
  const [advertisements, setAdvertisements] = useState({
    diamond: [],
    gold: []
  });
  const [blogs, setBlogs] = useState([]);
  const [adIndex, setAdIndex] = useState(0);

  const showPrevious = () => {
    setAdIndex(Math.max(adIndex - 3, 0));
  };

  const showNext = () => {
    setAdIndex(Math.min(adIndex + 3, Math.max(advertisements.gold.length - 3, 0)));
  };
  const fetchAds = async () => {
    try {
      const [diamondResponse, goldResponse] = await Promise.all([
        await api.get('Advertisement/GetAdvertisementByRank', { params: { rank: 'Diamond' } }),
        await api.get('Advertisement/GetAdvertisementByRank', { params: { rank: 'Gold' } })
      ]);

      setAdvertisements({
        diamond: diamondResponse.data,
        gold: goldResponse.data
      });
    } catch (error) {
      console.error("Error fetching advertisements:", error);
    }
  };
  const fetchBlogs = async () => {
    try {
      const response = await api.get('Blog/GetAllBlog');
      setBlogs(response.data.slice(0, 3)); // Get first 3 blogs
    } catch (error) {
      console.error("Error fetching blogs:", error);
    }
  };

  useEffect(() => {
    if (!processedRef.current) {
      fetchBlogs();
      fetchAds();
      processedRef.current = true;
    }



  }, []);

  return (
    <>
      <HeaderTemplate></HeaderTemplate>

      <Carousel autoplay className="carousel">
        {advertisements.diamond
          .filter(ad => ad.status === "Approved")
          .map((ad) => (
            <div className="carousel-item" key={ad.id}>
              <img
                style={contentStyle}
                src={ad.image}
                alt={ad.heading}
                className="header-img"
              />
              <div className="carousel-content">
                <Link style={{color: "white"}} to={`/advertisement-detail/${ad.adId}`}>
                  <h3>{ad.heading}</h3>
                </Link>
              </div>
            </div>
          ))}
      </Carousel>
      <div className="home-content">


        <body>
          {/* Trending Feature */}
          <div className="container">
            <div className="feature">
              <h2>Tính năng nổi bật</h2>
              <div class="rectangle"></div>
              </div>
              <div className="Card-container">
              <div className="Card">
                <img
                  src={KoiImage1}
                  alt="Selecting Koi fish according to your Feng Shui element"
                ></img>
                <a href="#">
                  {" "}

                  <Link to="calculation">
                    <h3>Tư vấn cá và hồ theo bản mệnh</h3>
                  </Link>

                </a>
              </div>
              <div className="Card">
                <img src={koiImage} alt="Post Advertisement Function"></img>
                <a href="#">
                  {" "}

                  <Link to="calculate-compability">
                    <h3>Tính độ tương hợp của cá và hồ theo bản mệnh</h3>
                  </Link>

                </a>
              </div>
              <div className="Card">
                <img src={KoiImage2} alt="Calculate compatibility"></img>
                <a href="#">
                  {" "}
                  <Link to="user-ads">
                    <h3>Đăng tin quảng cáo</h3>
                  </Link>

                </a>
              </div>
            </div>
          </div>
          <div id="Advertisements" className="container">
            <div className="feature">
            <h2>Quảng cáo</h2>
            <div class="rectangle"></div>
            </div>
            <div className="advertisement-container">
              <button
                className="nav-button nav-button-left"
                onClick={showPrevious}
                disabled={adIndex === 0}
              >
                <svg viewBox="0 0 24 24" width="24" height="24">
                  <path d="M15.41 7.41L14 6l-6 6 6 6 1.41-1.41L10.83 12z" />
                </svg>
              </button>
              <div className="Card-container">
                {advertisements.gold.filter(ad => ad.status === "Approved").length > 0 ? (
                  advertisements.gold
                    .filter(ad => ad.status === "Approved")
                    .slice(adIndex, adIndex + 3)
                    .map((ad) => (
                      <div className="Card" key={ad.adId}>
                        <img
                          src={ad.image}
                          alt={ad.heading}
                        />
                        <Link to={`/advertisement-detail/${ad.adId}`}>
                          <h3>{ad.heading}</h3>
                        </Link>
                      </div>
                    ))
                ) : (
                  <p>No approved gold advertisements available at the moment.</p>
                )}
              </div>
              <div className="view-all-blogs-container">
              <Link to="/ads-list" className="view-all-blogs-btn">
                View All Advertisements
              </Link>
            </div>
              <button
                className="nav-button nav-button-right"
                onClick={showNext}
                disabled={adIndex >= advertisements.gold.length - 3}
              >
                <svg viewBox="0 0 24 24" width="24" height="24">
                  <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" />
                </svg>
              </button>
            </div>
          </div>
          {/* Blog */}
          <div className="container">
            <div className="feature">
              <h2>Blog</h2>
              <div class="rectangle"></div>
              </div>
            <div className="Card-container">
              {blogs.map((blog) => (
                <div className="Card" key={blog.blogId}>
                  <img
                    src={blog.image}
                    alt={blog.heading}
                    className="img-feature"
                  />
                  <Link to={`/blog-detail/${blog.blogId}`}>
                    <h3>{blog.heading}</h3>
                  </Link>
                </div>
              ))}
            </div>
            <div className="view-all-blogs-container">
              <Link to="/blogs-list" className="view-all-blogs-btn">
                View All Blogs
              </Link>
            </div>
          </div>
        </body>
        <FooterPage></FooterPage>
      </div>
    </>
  );
}

export default Home;
