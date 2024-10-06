import React from 'react';
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';

const Ads = () => {
  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {
          <p>
            Ads
        </p>
        }
      </div>
    </div>
  );
};

export default Ads;
