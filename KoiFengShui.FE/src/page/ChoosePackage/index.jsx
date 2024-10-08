import React, { useState, useEffect } from 'react';
import { Form, Input, Button, DatePicker, InputNumber, Select, message, Row, Col, Card } from 'antd';
import { useNavigate } from 'react-router-dom';
import api from '../../config/axios';
import Header_page from '../../components/header-page';
import './index.css';

const { Option } = Select;

const ChoosePackage = () => {
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const [packages, setPackages] = useState([]);
  const [selectedPackage, setSelectedPackage] = useState(null);

  useEffect(() => {
    fetchPackages();
  }, []);

  const fetchPackages = async () => {
    try {
      const response = await api.get('Package/GetAllPackage');
      setPackages(response.data);
    } catch (error) {
      console.error('Error fetching packages:', error);
      message.error('Failed to fetch packages');
    }
  };

  const onFinish = async (values) => {
    try {
      const response = await api.put('api/Advertisement/UpdateAdvertisement', {
        ...values,
        startDate: values.startDate.toISOString(),
      });
      message.success('Advertisement updated successfully');
      navigate('/user-ads');
    } catch (error) {
      console.error('Error updating advertisement:', error);
      message.error('Failed to update advertisement');
    }
  };

  const handlePackageChange = (value) => {
    const selected = packages.find(pkg => pkg.rank === value);
    setSelectedPackage(selected);
    form.setFieldsValue({ total: selected ? selected.price : 0 });
  };

  return (
    <div className="choose-package-container">
      <Header_page />
      <h1>Choose Advertisement Package</h1>
      <Row gutter={24}>
        <Col span={12}>
          <Card title="Package Information">
            {selectedPackage ? (
              <>
                <p><strong>Rank:</strong> {selectedPackage.rank}</p>
                <p><strong>Price:</strong> ${selectedPackage.price}</p>
                <p><strong>Description:</strong> {selectedPackage.description}</p>
              </>
            ) : (
              <p>Select a package to view details</p>
            )}
          </Card>
        </Col>
        <Col span={12}>
          <Form form={form} onFinish={onFinish} layout="vertical">
            <Form.Item name="Rank" label="Rank" rules={[{ required: true }]}>
              <Select onChange={handlePackageChange}>
                {packages.map(pkg => (
                  <Option key={pkg.rank} value={pkg.rank}>{pkg.rank}</Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item name="Status" label="Status" rules={[{ required: true }]}>
              <Input />
            </Form.Item>
            <Form.Item name="startDate" label="Start Date" rules={[{ required: true }]}>
              <DatePicker showTime />
            </Form.Item>
            <Form.Item name="quantity" label="Quantity" rules={[{ required: true }]}>
              <InputNumber min={1} />
            </Form.Item>
            <Form.Item name="total" label="Total" rules={[{ required: true }]}>
              <InputNumber min={0} step={0.01} readOnly />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit">
                Update Advertisement
              </Button>
            </Form.Item>
          </Form>
        </Col>
      </Row>
    </div>
  );
};

export default ChoosePackage;