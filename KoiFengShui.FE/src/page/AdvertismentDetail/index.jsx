import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import api from "../../config/axios";
import HeaderTemplate from "../../components/header-page";
import { Spin } from 'antd';
import './index.css';
import FooterTemplate from '../../components/footer-page';


function AdvertisementDetail() {
  const [adHtml, setAdHtml] = useState('');
  const [loading, setLoading] = useState(true);
  const { id } = useParams();

  useEffect(() => {
    const fetchAdHtml = async () => {
      try {
        const response = await api.get(`Advertisement/GetAdvertisementByAdId?adId=${id}`);
        setAdHtml(response.data.link);
        console.log(response.data.link);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching advertisement:', error);
        setLoading(false);
      }
    };

    fetchAdHtml();
  }, [id]);

  return (
    <>
      <HeaderTemplate />
      <div 
     
      className="ad-detail-container">
        {loading ? (
          <Spin size="large" />
        ) : (
          <>
            <div dangerouslySetInnerHTML={{ __html: adHtml }} />
           
          </>
        )}
      </div>
      <FooterTemplate />
    </>
  );
}

export default AdvertisementDetail;