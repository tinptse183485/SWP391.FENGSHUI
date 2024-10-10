import React from 'react';
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';

const Blog = () => {
  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {
          <p>
            Blog
        </p>
        }
      </div>
    </div>
  );
};

export default Blog;
