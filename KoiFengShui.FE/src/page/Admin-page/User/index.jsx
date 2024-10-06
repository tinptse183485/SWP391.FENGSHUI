import React from 'react';
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';

const User = () => {
  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {
          <p>
            User
        </p>
        }
      </div>
    </div>
  );
};

export default User;
