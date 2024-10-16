import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import api from "../../config/axios";
import HeaderTemplate from "../../components/header-page";
import { Spin, Input, Button, message } from 'antd';
import './index.css';
import FooterTemplate from '../../components/footer-page';


function AdvertisementDetail() {
  const [adHtml, setAdHtml] = useState('');
  const [loading, setLoading] = useState(true);
  const [feedback, setFeedback] = useState('');
  const { id } = useParams();

  useEffect(() => {
    const fetchAdHtml = async () => {
      try {
        const response = await api.get(`Advertisement/GetAdvertisementByAdId?adId=${id}`);
        setAdHtml(response.data.link);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching advertisement:', error);
        setLoading(false);
      }
    };

    fetchAdHtml();
  }, [id]);

  const handleFeedbackChange = (e) => {
    setFeedback(e.target.value);
  };

  const submitFeedback = async () => {
    try {
      await api.post('Feedback/SubmitFeedback', {
        adId: id,
        feedbackText: feedback
      });
      message.success('Feedback submitted successfully');
      setFeedback('');
    } catch (error) {
      console.error('Error submitting feedback:', error);
      message.error('Failed to submit feedback');
    }
  };

  return (
    <>
      <HeaderTemplate />
      <div className="ad-detail-container">
        {loading ? (
          <Spin size="large" />
        ) : (
          <>
            <div dangerouslySetInnerHTML={{ __html: adHtml }} />
          </>
        )}
      </div>
      <div className="feedback-section">
              <h2>Leave Your Feedback</h2>
              <Input.TextArea
                rows={4}
                value={feedback}
                onChange={handleFeedbackChange}
                placeholder="Enter your feedback here..."
              />
              <Button type="primary" onClick={submitFeedback}>
                Submit Feedback
              </Button>
            </div>
      <FooterTemplate />
    </>
  );
}

export default AdvertisementDetail;
