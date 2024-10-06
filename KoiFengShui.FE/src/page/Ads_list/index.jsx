import React, { useEffect, useState } from 'react'; // Thêm useEffect và useState
import './index.css';
import api from "../../config/axios";
import { useNavigate } from 'react-router-dom';

const Ads_list = () => {
  const [data, setData] = useState([]); 
  // Khởi tạo state để lưu trữ dữ liệu
  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, []); // Chạy một lần khi component mount
  const fetchData = async () => {
    try {
      const response = await api.get('Advertisement/GetAllAdvertisement'); // Thay thế 'API_URL' bằng URL thực tế
      console.log('Fetched data:', response.data); // Kiểm tra dữ liệu nhận được
      setData(response.data); // Lưu trữ dữ liệu vào state
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };
  const viewAd = (adId) => {
    navigate(`/advertisement-detail/${adId}`);
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-content">
        {data.length > 0 ? ( // Kiểm tra xem có dữ liệu không
          data.map(item => (
            <div onClick={() => viewAd(item.adId)} key={item.adId} className="koi-item">
            <img  src={item.image}/> {/* Hiển thị hình ảnh */}
              <h3>{item.heading}</h3> {/* Hiển thị koiType */}
              
            </div>
          ))
        ) : (
          <p>No data available</p> // Thông báo nếu không có dữ liệu
        )}
      </div>
    </div>
  );
};

export default Ads_list;
