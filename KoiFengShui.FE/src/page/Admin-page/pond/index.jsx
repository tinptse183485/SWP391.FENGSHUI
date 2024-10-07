import React, { useState, useEffect } from "react"; // Added useState and useEffect
import Sidebar from "../../../components/sidebar/Sidebar";
import "./index.css";
import api from "../../../config/axios";

const Pond = () => {
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu

  useEffect(() => {
    fetchData();
  }, []); // Chạy một lần khi component mount
  const fetchData = async () => {
    try {
      const response = await api.get("Shape/GetAllShape"); // Thay thế 'API_URL' bằng URL thực tế
      console.log("Fetched data:", response.data); // Kiểm tra dữ liệu nhận được
      setData(response.data); // Lưu trữ dữ liệu vào state
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-content">
        {data.length > 0 ? ( // Kiểm tra xem có dữ liệu không
          data.map((item) => (
            <div key={item.shapeId} className="pond-item">
              <h3>{item.shapeId}</h3>
              <img src={item.image} alt={item.shapeId} />{" "}
              {/* Hiển thị hình ảnh */}
            </div>
          ))
        ) : (
          <p>No data available</p> // Thông báo nếu không có dữ liệu
        )}
      </div>
    </div>
  );
};

export default Pond;
