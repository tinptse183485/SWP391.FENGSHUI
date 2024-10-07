import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../config/axios';
import { Result, Button } from 'antd';
import './index.css';

const PaymentSuccess = () => {
  const [paymentStatus, setPaymentStatus] = useState(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPaymentStatus = async () => {
      try {
        const urlParams = new URLSearchParams(window.location.search);
        const response = await api.get('VNPay/payment-callback', {
          params: Object.fromEntries(urlParams)
        });
        setPaymentStatus(response.data.message === 'Thanh toán thành công' ? 'success' : 'error');
      } catch (error) {
        console.error('Error fetching payment status:', error);
        setPaymentStatus('error');
      } finally {
        setLoading(false);
      }
    };

    fetchPaymentStatus();
  }, []);

  if (loading) {
    return <div className="loading">Loading...</div>;
  }

  return (
    <div className="payment-result">
      <Result
        status={paymentStatus}
        title={paymentStatus === 'success' ? 'Payment Successful!' : 'Payment Failed'}
        subTitle={
          paymentStatus === 'success'
            ? 'Your payment has been processed successfully.'
            : 'There was an issue processing your payment. Please try again.'
        }
        extra={[
          <Button 
            type="primary" 
            key="console" 
            onClick={() => navigate(paymentStatus === 'success' ? '/' : '/payment')}
          >
            {paymentStatus === 'success' ? 'Go to Home' : 'Try Again'}
          </Button>
        ]}
      />
    </div>
  );
};

export default PaymentSuccess;