import React, { useState, useEffect } from 'react';
import { Layout, Menu, Table, Button, Popconfirm, message } from 'antd';
import { useNavigate } from 'react-router-dom';
import Header_page from '../../../components/header-page';
import api from '../../../config/axios';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import './index.css';
import { toast } from 'react-toastify';

const { Content } = Layout;

const Blog = () => {
  const navigate = useNavigate();
  const [blogs, setBlogs] = useState([]);
  const [filteredBlogs, setFilteredBlogs] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState('all');

  const menuItems = [
    { key: 'all', label: 'Tất cả' },
    { key: 'Draft', label: 'Bản nháp' },
    { key: 'Published', label: 'Đã đăng' },
  ];

  useEffect(() => {
    fetchBlogs();
  }, []);

  useEffect(() => {
    filterBlogs(selectedStatus);
  }, [selectedStatus, blogs]);

  const fetchBlogs = async () => {
    try {
      const response = await api.get("Blog/GetAllBlog");
      setBlogs(response.data);
    } catch (error) {
      console.error('Error fetching blogs:', error);
    }
  };

  const filterBlogs = (status) => {
    if (status === 'all') {
      setFilteredBlogs(blogs);
    } else {
      const filtered = blogs.filter((blog) => blog.status === status);
      setFilteredBlogs(filtered);
    }
  };

  const handleMenuClick = (e) => {
    setSelectedStatus(e.key);
  };

  const handleCreate = () => {
    navigate("/dashboard/create-blog", { state: { blog: null } });
  };

  const handleEdit = (blog) => {
    navigate("/dashboard/create-blog", { state: { blog } });
  };

  const handleDelete = async (blogId) => {
    try {
      const response = await api.delete(`Blog/DeleteBlog/${blogId}`);
      toast.success(response.data);
      fetchBlogs(); // Cập nhật lại danh sách blog sau khi xóa
    } catch (error) {
      toast.error(error.response.data);
    }
  };

  const columns = [
    {
      title: 'BlogId',
      dataIndex: 'blogId',
      key: 'blogId',
    },
    {
      title: 'Heading',
      dataIndex: 'heading',
      key: 'heading',
    },
    {
      title: 'Image',
      dataIndex: 'image',
      key: 'image',
      render: (_, record) => (
        <img src={record.image} alt="Blog Image"  
        style={{ width: '150px', height: '150px', objectFit: "cover", borderRadius: "4px" }} />
      ),
    },
    {
      title: "Nội dung",
      key: "content",
      render: (_, record) => (
        <Button
          type="link"
          onClick={() => navigate(`/blog-detail/${record.blogId}`)}
        >
          Xem chi tiết
        </Button>
      ),
    },
    {
      title: 'Status',
      dataIndex: 'status',
      key: 'status',
    },
    {
      title: 'Hành động',
      key: 'action',
      render: (_, record) => (
        <>
          <Button
            icon={<EditOutlined />}
            onClick={() => handleEdit(record)}
            style={{ marginRight: 8 }}
          >
            Chỉnh sửa
          </Button>
          <Popconfirm
            title="Bạn có chắc chắn muốn xóa bài viết này?"
            onConfirm={() => handleDelete(record.blogId)}
            okText="Có"
            cancelText="Không"
          >
            <Button icon={<DeleteOutlined />} danger>
              Xóa
            </Button>
          </Popconfirm>
        </>
      ),
    },
  ];

  return (
    <Layout style={{ minHeight: '100vh', backgroundColor: 'white' }}>
      <Layout>
        <Content style={{ margin: '16px' }}>
          <div className="container" style={{ padding: 24, background: '#fff', minHeight: 360 }}>
            <div className="Heading">
              <h1>Quản lý Blog</h1>
              <button onClick={handleCreate} className="btn-create">
                Tạo Blog
              </button>
            </div>
            <Menu
              className="menu"
              theme="light"
              mode="horizontal"
              selectedKeys={[selectedStatus]}
              onClick={handleMenuClick}
            >
              {menuItems.map((item) => (
                <Menu.Item key={item.key}>{item.label}</Menu.Item>
              ))}
            </Menu>
            <Table columns={columns} dataSource={filteredBlogs} rowKey="blogId" />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default Blog;
