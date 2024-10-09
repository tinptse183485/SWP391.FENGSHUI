import React, { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { message } from 'antd';
import api from '../../config/axios';

const PaymentSuccess = () => {
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const queryParams = new URLSearchParams(location.search);
    const vnp_ResponseCode = queryParams.get('vnp_ResponseCode');
    const packageInfo = JSON.parse(localStorage.getItem('pendingAdPackage'));

    const handlePaymentResult = async () => {
      if (vnp_ResponseCode === '00') {
        try {
          await api.put('Advertisement/UpdateAdvertisement', {
            adId: packageInfo.adId,
            Rank: packageInfo.rank,
            Status: packageInfo.status,
            startDate: packageInfo.startDate,
            quantity: packageInfo.quantity,
            total: packageInfo.total
          });
          message.success('Payment successful and advertisement updated');
          navigate('/user-ads');
        } catch (error) {
          console.error('Error updating advertisement:', error);
          message.error('Payment successful but failed to update advertisement');
        }
      } else {
        message.error('Payment failed');
        navigate('/choose-package');
      }
      localStorage.removeItem('pendingAdPackage');
    };

    if (packageInfo) {
      handlePaymentResult();
    } else {
      navigate('/choose-package');
    }
  }, [location, navigate]);

  return (
    <div>
      <h1>Processing payment result...</h1>
    </div>
  );
};

export default PaymentSuccess;