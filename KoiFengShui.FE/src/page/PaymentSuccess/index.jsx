import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { message } from 'antd';
import api from '../../config/axios';

const PaymentSuccess = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [isProcessing, setIsProcessing] = useState(false);

  useEffect(() => {
    const processPayment = async () => {
      if (isProcessing) return;
      setIsProcessing(true);

      const queryParams = new URLSearchParams(location.search);
      const vnp_ResponseCode = queryParams.get('vnp_ResponseCode');
      const packageInfo = JSON.parse(localStorage.getItem('pendingAdPackage'));
      const adData = JSON.parse(localStorage.getItem('adData'));

      if (vnp_ResponseCode === '00') {
        try {
          const queryParams = new URLSearchParams({
            Rank: packageInfo.rank,
            Status: 'Pending',
            startDate: packageInfo.startDate,
            quantity: packageInfo.quantity,
            total: packageInfo.total
          }).toString();

          const response = await api.get('Advertisement/CheckAdIdExist', {
            params: { adId: adData.adId }
          });

          if (response.data === "False") {
            const response1 =  await api.post('Advertisement/AddAdvertisementDraft', {
              adId: ".",
              heading: adData.heading,
              image: adData.image,
              link: adData.link,
              userId: adData.userId,
              elementId: adData.elementId
            });
            adData.adId = response1.data.adId;
          }

          await api.put(`Advertisement/UpdateAdvertisement?${queryParams}`, {
            adId: adData.adId,
            heading: adData.heading,
            image: adData.image,
            link: adData.link,
            userId: adData.userId,
            elementId: adData.elementId
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
      localStorage.removeItem('adData');
      setIsProcessing(false);
    };

    processPayment();
  }, [location, navigate]);

  return (
    <div>
      <h1>Processing payment result...</h1>
    </div>
  );
};

export default PaymentSuccess;