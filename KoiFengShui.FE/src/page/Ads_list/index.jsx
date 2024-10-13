import React, { useEffect, useState } from 'react'; // Thêm useEffect và useState
import { Link } from 'react-router-dom';
import './index.css';
import api from "../../config/axios";

import HeaderTemplate from '../../components/header-page';


import { useNavigate } from 'react-router-dom';


const Ads_list = () => {
  const [ads, setAds] = useState([]);
  const [featuredAd, setFeaturedAd] = useState(null);


  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await api.get('Advertisement/GetAllAdvertisement');
      if (response.data.length > 0) {
        setFeaturedAd(response.data[0]);
        setAds(response.data.slice(1));
      }
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };



  return (

    <>
      <HeaderTemplate />
      <div className="ads-container">
        <h1 className="ads-title">Our Advertisements</h1>
        
        {featuredAd && (
          <div className="featured-ad">
            <img src={featuredAd.image} alt={featuredAd.heading} className="featured-ad-image" />
            <div className="featured-ad-content">
              <h2 className="featured-ad-title">{featuredAd.heading}</h2>
              <p className="featured-ad-description">{featuredAd.description}</p>
              <Link to={`/advertisement-detail/${featuredAd.adId}`} className="view-details-btn">View Details</Link>

            </div>
          </div>
        )}

        <div className="ads-grid">
          {ads.map((ad) => (
            <article key={ad.adId} className="ad-item">
              <img src={ad.image} alt={ad.heading} className="ad-item-image" />
              <div className="ad-item-content">
                <h3 className="ad-item-title">{ad.heading}</h3>
                <p className="ad-item-date">{new Date(ad.startDate).toLocaleDateString()}</p>
                <Link to={`/advertisement-detail/${ad.adId}`} className="view-details-link">View Details</Link>
              </div>
            </article>
          ))}
        </div>
      </div>
    </>
  );
};

export default Ads_list;
