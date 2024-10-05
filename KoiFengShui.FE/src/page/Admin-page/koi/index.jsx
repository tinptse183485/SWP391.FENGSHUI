import React from 'react';
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';

const Koi = () => {
  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {
          <p>
            Koi
        </p>
        }
      </div>
    </div>
  );
};

export default Koi;
