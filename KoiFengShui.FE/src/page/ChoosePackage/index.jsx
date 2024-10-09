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
  const [invoiceInfo, setInvoiceInfo] = useState({
    adId: '',
    rank: '',
    startDate: null,
    expiredDate: null,
    total: 0
  });

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
        status: 'Pending',
        total: values.total
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
    const quantity = form.getFieldValue('quantity') || 1;
    const total = selected ? selected.price * quantity : 0;
    form.setFieldsValue({ total });
    
    setInvoiceInfo(prev => ({
      ...prev,
      rank: selected.rank,
      total
    }));
  };

  const handleQuantityChange = (value) => {
    const selected = packages.find(pkg => pkg.rank === form.getFieldValue('Rank'));
    if (selected) {
      const total = selected.price * value;
      form.setFieldsValue({ total });
      setInvoiceInfo(prev => ({ ...prev, total }));
    }
  };

  const handleDateChange = (date) => {
    const expiredDate = new Date(date);
    expiredDate.setMonth(expiredDate.getMonth() + 1);
    setInvoiceInfo(prev => ({
      ...prev,
      startDate: date,
      expiredDate
    }));
  };

  return (
    <div className="choose-package-container">
      <Header_page />
      <h1>Choose Advertisement Package</h1>
      <div className="package-layout">
        <div className="package-info-and-form">
          <div className="package-info">
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
          </div>
          <div className="update-form">
            <Form form={form} onFinish={onFinish} layout="vertical">
              <Form.Item name="Rank" label="Rank" rules={[{ required: true }]}>
                <Select onChange={handlePackageChange}>
                  {packages.map(pkg => (
                    <Option key={pkg.rank} value={pkg.rank}>{pkg.rank}</Option>
                  ))}
                </Select>
              </Form.Item>
              <Form.Item name="startDate" label="Start Date" rules={[{ required: true }]}>
                <DatePicker showTime onChange={handleDateChange} />
              </Form.Item>
              <Form.Item name="quantity" label="Quantity" rules={[{ required: true }]}>
                <InputNumber min={1} onChange={handleQuantityChange} />
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
          </div>
        </div>
        <div className="invoice-info">
          <Card title="Advertisement Invoice">
            <p><strong>Ad ID:</strong> {invoiceInfo.adId || 'Not assigned yet'}</p>
            <p><strong>Rank:</strong> {invoiceInfo.rank || 'Not selected'}</p>
            <p><strong>Start Date:</strong> {invoiceInfo.startDate ? invoiceInfo.startDate.format('YYYY-MM-DD HH:mm:ss') : 'Not set'}</p>
            <p><strong>Expired Date:</strong> {invoiceInfo.expiredDate ? invoiceInfo.expiredDate.format('YYYY-MM-DD HH:mm:ss') : 'Not set'}</p>
            <p><strong>Total:</strong> ${invoiceInfo.total.toFixed(2)}</p>
          </Card>
        </div>
      </div>
    </div>
  );
};

export default ChoosePackage;