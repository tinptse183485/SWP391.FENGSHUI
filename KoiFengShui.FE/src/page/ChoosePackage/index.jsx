import React, { useState, useEffect } from 'react';
import { Form, Input, Button, DatePicker, InputNumber, Select, message, Row, Col, Card } from 'antd';
import { useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import api from '../../config/axios';
import Header_page from '../../components/header-page';
import './index.css';
import { useLocation } from 'react-router-dom';

const { Option } = Select;

const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
};

const ChoosePackage = () => {
  const location = useLocation();
  const adData = location.state?.adData || {};
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const [packages, setPackages] = useState([]);
  const [selectedPackage, setSelectedPackage] = useState(null);
  const [invoiceInfo, setInvoiceInfo] = useState({
    adId: adData.adId || '',
    rank:  '',
    startDate:  null,
    expiredDate: null,
    total:  0
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
    const packageInfo = {
      adId: invoiceInfo.adId,
      rank: values.Rank,
      startDate: values.startDate.format('YYYY-MM-DD'),
      status: 'Pending',
      quantity: values.quantity,
      total: invoiceInfo.total
    };

    try {
      const response = await api.post('VNPay/create-payment', {
        amount: invoiceInfo.total,
        
      });

      if (response.data && response.data.paymentUrl) {
        localStorage.setItem('pendingAdPackage', JSON.stringify(packageInfo));
        window.location.href = response.data.paymentUrl;
      } else {
        message.error('Failed to create payment URL');
      }
    } catch (error) {
      console.error('Error creating payment:', error);
      message.error('Failed to initiate payment');
    }
  };

  const handlePackageChange = (value) => {
    const selected = packages.find(pkg => pkg.rank === value);
    setSelectedPackage(selected);
    const quantity = form.getFieldValue('quantity') || 1;
    const total = selected ? selected.price * quantity : 0;
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
      setInvoiceInfo(prev => ({ ...prev, total }));
    }
  };

  const handleDateChange = (date) => {
    if (date && selectedPackage) {
      const formattedStartDate = dayjs(date).format('DD-MM-YYYY');
      const expiredDate = dayjs(date).add(selectedPackage.duration, 'days');
      const formattedExpiredDate = expiredDate.format('DD-MM-YYYY');
      setInvoiceInfo(prev => ({
        ...prev,
        startDate: formattedStartDate,
        expiredDate: formattedExpiredDate
      }));
    }
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
                  <p><strong>Price:</strong> {formatCurrency(selectedPackage.price)}</p>
                  <p><strong>Description:</strong> {selectedPackage.description}</p>
                  <p><strong>Duration:</strong> {selectedPackage.duration} days</p>
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
                <DatePicker  onChange={handleDateChange} />
              </Form.Item>
              <Form.Item name="quantity" label="Quantity" rules={[{ required: true }]}>
                <InputNumber min={1} onChange={handleQuantityChange} />
              </Form.Item>
             
              <Form.Item>
                <Button type="primary" htmlType="submit">
                  Thanh Toán
                </Button>
              </Form.Item>
            </Form>
          </div>
        </div>
        <div className="invoice-info">
          <Card title="Advertisement Invoice">
            <p><strong>Ad ID:</strong> {invoiceInfo.adId || 'Not assigned yet'}</p>
            <p><strong>Rank:</strong> {invoiceInfo.rank || 'Not selected'}</p>
            <p><strong>Start Date:</strong> {invoiceInfo.startDate || 'Not set'}</p>
            <p><strong>Expired Date:</strong> {invoiceInfo.expiredDate || 'Not set'}</p>
            <p><strong>Total:</strong> {formatCurrency(invoiceInfo.total)}</p>
          </Card>
        </div>
      </div>
    </div>
  );
};

export default ChoosePackage;