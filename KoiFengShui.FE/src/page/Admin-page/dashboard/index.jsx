import React, { useEffect, useState } from "react";
import { Bar } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
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

const Dashboard = () => {
  const [adData, setAdData] = useState([]);

  useEffect(() => {
    fetchAdData();
  }, []);

  const fetchAdData = async () => {
    try {
      const response = await api.get("Advertisement/GetAllAdvertisement");
      setAdData(response.data);
    } catch (error) {
      console.error("Error fetching ad data:", error);
    }
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

  const countAdsByRank = () => {
    const rankCounts = {};
    adData.forEach((ad) => {
      if (rankCounts[ad.rank]) {
        rankCounts[ad.rank]++;
      } else {
        rankCounts[ad.rank] = 1;
      }
    });
    return rankCounts;
  };

  const rankChartData = {
    labels: Object.keys(countAdsByRank()),
    datasets: [
      {
        label: "Number of Ads",
        data: Object.values(countAdsByRank()),
        backgroundColor: "rgba(32, 119, 105, 0.8)",
        borderColor: "rgba(32, 119, 105, 0.8)",
        borderWidth: 1,
      },
    ],
  };

  const userChartData = {
    labels: Object.keys(countAdsByUserId()),
    datasets: [
      {
        label: "Number of Ads",
        data: Object.values(countAdsByUserId()),
        backgroundColor: "rgba(255, 99, 132, 0.6)",
        borderColor: "rgba(255, 99, 132, 1)",
        borderWidth: 1,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
    },
  };

  const rankOptions = {
    ...options,
    plugins: {
      ...options.plugins,
      title: {
        display: true,
        text: "Ads Count by Rank",
      },
    },
  };

  const userOptions = {
    ...options,
    plugins: {
      ...options.plugins,
      title: {
        display: true,
        text: "Ads Count by User ID",
      },
    },
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-content">
        <h2>Dashboard</h2>
        <div className="chart-container">
          <Bar data={rankChartData} options={rankOptions} />
        </div>
        <div className="chart-container">
          <Bar data={userChartData} options={userOptions} />
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
