import React from 'react';
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';

const Pond = () => {
  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {
          <p>
            Pond
        </p>
        }
      </div>
    </div>
  );
};

export default Pond;
