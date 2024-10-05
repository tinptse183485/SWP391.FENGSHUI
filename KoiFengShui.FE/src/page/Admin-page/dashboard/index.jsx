import React from 'react';
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';

const Dashboard = () => {
  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {
          <p>
            Dashboard
        </p>
        }
      </div>
    </div>
  );
};

export default Dashboard;
