import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../config/axios';
import { Result, Button, Card } from 'antd';
import './index.css';

const PaymentSuccess = () => {
  const [paymentStatus, setPaymentStatus] = useState(null);
  const [paymentInfo, setPaymentInfo] = useState(null);
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
        if (response.data.message === 'Thanh toán thành công') {
          setPaymentInfo({
            amount: urlParams.get('vnp_Amount'),
            bankCode: urlParams.get('vnp_BankCode'),
            orderInfo: urlParams.get('vnp_OrderInfo'),
            payDate: urlParams.get('vnp_PayDate'),
            transactionNo: urlParams.get('vnp_TransactionNo'),
          });
        }
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
    <div className='container'>
    <div className="payment-result-container">
      <div className='payment-result-content'>
      <Result
        status={paymentStatus}
        title={paymentStatus === 'success' ? 'Payment Successful!' : 'Payment Failed'}
        subTitle={
          paymentStatus === 'success'
            ? 'Your payment has been processed successfully.'
            : 'There was an issue processing your payment. Please try again.'
        }
        
      />
      </div>
      <div className='payment-result-info'>
      {paymentStatus === 'success' && paymentInfo && (
        <Card title="Payment Information" >
          <p><strong>Amount:</strong> {parseInt(paymentInfo.amount) / 100} VND</p>
          <p><strong>Bank:</strong> {paymentInfo.bankCode}</p>
          <p><strong>Order Info:</strong> {decodeURIComponent(paymentInfo.orderInfo)}</p>
          <p><strong>Payment Date:</strong> {paymentInfo.payDate}</p>
          <p><strong>Transaction No:</strong> {paymentInfo.transactionNo}</p>
        </Card>
      )}

      </div>
      </div>
      <Button 
      
            type="primary" 
            key="console" 
            onClick={() => navigate(paymentStatus === 'success' ? '/' : '/payment')}
          >
            {paymentStatus === 'success' ? 'Go to Home' : 'Try Again'}
          </Button>
    </div>
    
    
  );
};

export default PaymentSuccess;