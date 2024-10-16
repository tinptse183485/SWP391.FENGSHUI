import React, { useEffect, useState, useRef } from "react";
import { Bar , Line} from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
} from "chart.js";
import api from "../../../config/axios";
import "./index.css";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const AdminDashboard = () => {
  const [adData, setAdData] = useState([]);
  const [revenueData, setRevenueByPackageData] = useState({});
  const [ageGroupData, setAgeGroupData] = useState({});
  const [fishcountData, setFishcountData] = useState([]);

  useEffect(() => {
    fetchAdData();
    fetchRevenueByPackageData();
    fetchUsersByAgeGroupData();
    fetchfishData();
  }, []);

  const fetchfishData = async () => {
    try {
      const response = await api.get("KoiVariety/GetAllKoi");
      setFishcountData(response.data);
    } catch (error) {
      console.error("Error fetching fish data:", error);
    }
  };

  const fetchAdData = async () => {
    try {
      const response = await api.get("Advertisement/GetAllAdvertisement");
      setAdData(response.data);
    } catch (error) {
      console.error("Error fetching ad data:", error);
    }
  };

  const fetchRevenueByPackageData = async () => {
    try {
      const response = await api.get("Dashboard/RevenueByPackage");
      setRevenueByPackageData(response.data);
    } catch (error) {
      console.error("Error fetching revenue by package data:", error);
    }
  };

  const fetchUsersByAgeGroupData = async () => {
    try {
      const response = await api.get("Dashboard/UsersByAgeGroup");
      setAgeGroupData(response.data);
    } catch (error) {
      console.error("Error fetching users by age group data:", error);
    }
  };

  const countFishbyElement = () => {
    const fishCounts = {};
    if (Array.isArray(fishcountData)) {
      fishcountData.forEach((fish) => {
        if (fishCounts[fish.element]) {
          fishCounts[fish.element]++;
        } else {
          fishCounts[fish.element] = 1;
        }
      });
    }
    return fishCounts;
  };

  const countAdsByUserId = () => {
    const userCounts = {};
    adData.forEach((ad) => {
      if (userCounts[ad.userId]) {
        userCounts[ad.userId]++;
      } else {
        userCounts[ad.userId] = 1;
      }
    });
    return userCounts;
  };

  const fishBarChartData = {
    labels: Object.keys(countFishbyElement()),
    datasets: [
      {
        label: "Số lượng cá theo mệnh ",
        data: Object.values(countFishbyElement()),
        backgroundColor: "rgba(92, 245, 39, 0.8)",
        borderColor: "rgba(92, 245, 39, 0.8)",
        borderWidth: 1,
      },
    ],
  };
  const userChartData = {
    labels: Object.keys(countAdsByUserId()),
    datasets: [
      {
        label: "Số lượng quảng cáo",
        data: Object.values(countAdsByUserId()),
        backgroundColor: "rgba(255, 99, 132, 0.6)",
        borderColor: "rgba(255, 99, 132, 1)",
        borderWidth: 1,
      },
    ],
  };

  const revenueChartData = {
    labels: Object.keys(revenueData),
    datasets: [
      {
        label: "Doanh thu theo gói",
        data: Object.values(revenueData),
        backgroundColor: "rgba(75, 192, 192, 0.6)",
        borderColor: "rgba(75, 192, 192, 1)",
        borderWidth: 1,
      },
    ],
  };

  const ageGroupChartData = {
    labels: Object.keys(ageGroupData),
    datasets: [
      {
        label: "Số lượng người dùng theo nhóm tuổi",
        data: Object.values(ageGroupData),
        backgroundColor: "rgba(153, 102, 255, 0.6)",
        borderColor: "rgba(153, 102, 255, 1)",
        borderWidth: 1,
      },
    ],
  };

  const revenueOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: "Doanh thu theo gói",
      },
    },
  };

  const ageGroupOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: "Số lượng người dùng theo nhóm tuổi",
      },
    },
  };

  const userOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: "Số lượng quảng cáo theo người dùng",
      },
    },
  };
  const fishLineChartOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: "Số lượng cá theo mệnh",
      },
    },
  };
  return (
    <div className="dashboard-container">
      <h2>Dashboard</h2>
      <div className="dashboard-content">

        
        {/* Thẻ tổng quan */}
        <div className="overview-cards">
          <div className="overview-card">
            <h3>số lượng quảng cáo</h3>
            <p>{adData.length}</p>
          </div>

          <div className="overview-card" style={{ backgroundColor: '#ff9800' }}>
            <h3>Tổng doanh thu</h3>
            <p>{Object.values(revenueData).reduce((acc, curr) => acc + curr, 0)}</p>
          </div>
        </div>

        <div className="chart-container">
          {revenueData && Object.keys(revenueData).length > 0 ? (
            <Bar key="revenueChart" data={revenueChartData} options={revenueOptions} />
          ) : (
            <p>Loading revenue data...</p>
          )}
        </div>
        <div className="chart-container">
          {ageGroupData && Object.keys(ageGroupData).length > 0 ? (
            <Bar key="ageGroupChart" data={ageGroupChartData} options={ageGroupOptions} />
          ) : (
            <p>Loading age group data...</p>
          )}
        </div>
        <div className="chart-container">
          {adData && adData.length > 0 ? (
            <Bar key="userChart" data={userChartData} options={userOptions} />
          ) : (
            <p>Loading ad data...</p>
          )}
        </div>
        <div className="chart-container">
          {fishcountData && fishcountData.length > 0 ? (
            <Bar key="fishLineChart" data={fishBarChartData} options={fishLineChartOptions} />
          ) : (
            <p>Loading fish data...</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
