import React from 'react';
import { Card } from 'antd';
import './index.css';

const AdvertisementDisplay = ({ advertisements, userElement }) => {
  const filteredAds = advertisements.filter(ad => ad.elementId === userElement);

  return (
    <div className="advertisement-container">
      <h2>Advertisements </h2>
      <div className="advertisement-list">
        {filteredAds.map((ad) => (
          <Card key={ad.id} className="advertisement-card">
            <div className="image-container">
              <img src={ad.image} alt={ad.heading} />
              <div className="text-block">
                <h3>{ad.heading}</h3>
              </div>
            </div>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default AdvertisementDisplay;