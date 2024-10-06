import React, { useEffect, useState } from 'react'; // Thêm useEffect và useState
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';
import api from '../../../config/axios';
import { Table } from 'antd';

const Koi = () => {
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu

  useEffect(() => {
    fetchData();
  }, []); // Chạy một lần khi component mount

  
  const fetchData = async () => {
    try {
      const response = await api.get('KoiVariety/GetAllKoi'); // Thay thế 'API_URL' bằng URL thực tế
      console.log('Fetched data:', response.data); // Kiểm tra dữ liệu nhận được
      setData(response.data); // Lưu trữ dữ liệu vào state
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const columns = [
    {
      title: 'Koi Type',
      dataIndex: 'koiType',
    },
    {
      title: 'Image',
      dataIndex: 'image',
    },
    {
      title: 'Element',
      dataIndex: 'element',
    },
    {
      title: 'Description',
      dataIndex: 'description',
    },
  ];
  return (
    <div className="dashboard-container">
     <Table dataSource={data} columns={columns} />
    </div>
  );
};

export default Koi;

