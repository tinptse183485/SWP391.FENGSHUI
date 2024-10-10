import React, { useEffect, useState } from "react";
import { Table, Space, Button, Input, Select } from "antd"; // Added Select import
import api from "../../../config/axios";
import "./index.css";

const { Option } = Select; // Destructure Option from Select

const User = () => {
  const [userData, setUserData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingUser, setEditingUser] = useState(null); // New state for editing

  useEffect(() => {   
    fetchUserData();
  }, []);

  const fetchUserData = async () => {
    try {
      setLoading(true);
      const response = await api.get("Account/GetAllAccountMemberInfo");
      setUserData(response.data);
    } catch (error) {
      console.error("Error fetching user data:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleEdit = (user) => {
    setEditingUser(user); // Set the user to be edited
  };

  const handleSave = async (userID) => {
    try {
      await api.put("Account/UpdateAccountAdmin", {
        userID: userID,
        password: editingUser.password,
        status: editingUser.status,
      });
      setEditingUser(null); // Clear editing state after saving
      fetchUserData(); // Refresh user data
    } catch (error) {
      console.error("Error updating user data:", error);
    }
  };

  const columns = [
    {
      title: "User ID",
      dataIndex: "userID",
      key: "userID",
    },
    {
      title: "Password",
      dataIndex: "password",
      key: "password",
      render: (text, record) => (
        editingUser?.userID === record.userID ? (
          <Input
            value={editingUser.password}
            onChange={(e) => setEditingUser({ ...editingUser, password: e.target.value })}
          />
        ) : text
      ),
    },
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
    },
    {
      title: "Role",
      dataIndex: "role",
      key: "role",
    },
    {
      title: "Status",
      dataIndex: "status",
      key: "status",
      render: (text, record) => (
        editingUser?.userID === record.userID ? (
          <Select
            value={editingUser.status}
            onChange={(value) => setEditingUser({ ...editingUser, status: value })}
          >
            <Option value="Active">Active</Option>
            <Option value="Block">Block</Option>
          </Select>
        ) : text
      ),
    },
    {
      title: "Birthday",
      dataIndex: "birthday",
      key: "birthday",
      render: (birthday) => new Date(birthday).toLocaleDateString(),
    },
    {
      title: "Action",
      key: "action",
      render: (text, record) => (
        <Space direction="horizontal">
          {editingUser?.userID === record.userID ? (
            <Button type="primary" onClick={() => handleSave(record.userID)}>
              Save
            </Button>
          ) : (
            <Button type="primary" onClick={() => handleEdit(record)}>
              Update
            </Button>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div className="dashboard-container">
      <div className="dashboard-content">
        <h2>User Management</h2>
        <div className="user-management-table">
          <Table
            columns={columns}
            dataSource={userData}
            rowKey="userID"
            loading={loading}
            scroll={{ x: true }}
          />
        </div>
      </div>
    </div>
  );
};

export default User;
