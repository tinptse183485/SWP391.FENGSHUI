import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import api from "../../config/axios";
import HeaderTemplate from "../../components/header-page";
import { Spin, Input, Button, message, Rate } from 'antd';
import './index.css';
import FooterTemplate from '../../components/footer-page';



function AdvertisementDetail() {
  const [adHtml, setAdHtml] = useState('');
  const [loading, setLoading] = useState(true);
  const [feedback, setFeedback] = useState('');
  const [rating, setRating] = useState(0);
  const { id } = useParams();
  const [allFeedback, setAllFeedback] = useState([]);



  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const [adResponse, feedbackResponse] = await Promise.all([
          retryFetch(() => api.get(`Advertisement/GetAdvertisementByAdId?adId=${id}`)),
          retryFetch(() => api.get(`Feedback/GetFeedBackByAdId?adId=${id}`))
        ]);

        setAdHtml(adResponse.data.link);
        setAllFeedback(feedbackResponse.data);
      } catch (error) {
        console.error('Error fetching data:', error);
        message.error('Failed to load some data. Please try refreshing the page.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  const handleFeedbackChange = (e) => {
    setFeedback(e.target.value);
  };

  const handleRatingChange = (value) => {
    setRating(value);
    console.log(rating);
  };

  const submitFeedback = async () => {
    if (rating === 0) {
      message.warning('Please provide a rating');
      return;
    }

    try {
      const user = localStorage.getItem('userId');
      const newFeedback = {
        fbId: ".",
        description: feedback,
        adId: id,
        userId: user,
        rate: rating
      };
      await retryFetch(() => api.post('Feedback/AddFeedback', newFeedback));
      message.success('Feedback submitted successfully');
      
      // Fetch updated feedback
      const updatedFeedback = await retryFetch(() => api.get(`Feedback/GetFeedBackByAdId?adId=${id}`));
      setAllFeedback(updatedFeedback.data);
      
      setFeedback('');
      setRating(0);
    } catch (error) {
      console.error('Error submitting feedback:', error);
      message.error('Failed to submit feedback. Please try again.');
    }
  };

  const retryFetch = async (fetchFunction, retries = 3, delay = 1000) => {
    for (let i = 0; i < retries; i++) {
      try {
        return await fetchFunction();
      } catch (error) {
        if (i === retries - 1) throw error;
        await new Promise(resolve => setTimeout(resolve, delay));
      }
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
        <h2>Đánh giá và Phản hồi</h2>
        <div className="rating-container">
          <span>Đánh giá bài đăng:</span>
          <Rate 
            value={rating} 
            onChange={handleRatingChange}
            className="feedback-rate"
          />
        </div>
        <Input.TextArea
          rows={4}
          value={feedback}
          onChange={handleFeedbackChange}
          placeholder="Chia sẻ ý kiến của bạn về bài đăng này..."
        />
        <Button type="primary" onClick={submitFeedback}>
          Gửi đánh giá
        </Button>
        
        <div className="all-feedback-section">
          <h2>Phản hồi từ cộng đồng</h2>
          {allFeedback.length > 0 ? (
            allFeedback.map((feedback, index) => (
              <div key={index} className="feedback-item">
                <Rate disabled value={feedback.rate} />
                <p>{feedback.description}</p>
              </div>
            ))
          ) : (
            <p>Chưa có đánh giá nào. Hãy là người đầu tiên chia sẻ ý kiến!</p>
          )}
        </div>
      </div>
      
      <FooterTemplate />
    </>
  );
}

export default AdvertisementDetail;
