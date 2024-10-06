import React, { useEffect, useState } from 'react'; // Thêm useEffect và useState
import Sidebar from '../../../components/sidebar/Sidebar';
import './index.css';
import api from '../../../config/axios';

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

  return (
    <div className="dashboard-container">
      <Sidebar />
      <div className="dashboard-content">
        {data.length > 0 ? ( // Kiểm tra xem có dữ liệu không
          data.map(item => (
            <div key={item.koiType} className="koi-item">
              <h3>{item.koiType}</h3> {/* Hiển thị koiType */}
              <img  src={`/koi_image/${item.image}`} alt={item.koiType} /> {/* Hiển thị hình ảnh */}
              <p>Element: {item.element}</p> {/* Hiển thị element */}
              <p>Description: {item.description}</p> {/* Hiển thị description */}
            </div>
          ))
        ) : (
          <p>No data available</p> // Thông báo nếu không có dữ liệu
        )}
      </div>
    </div>
  );
};

export default Koi;
