import React, { useState, useRef, useEffect } from 'react';
import { Editor } from '@tinymce/tinymce-react';
import { Button, message } from 'antd';
import api from '../../config/axios';
import { useNavigate, useLocation } from 'react-router-dom';
import './index.css';

function CreateAds() {
  const location = useLocation();
  const navigate = useNavigate();
  const [adData, setAdData] = useState({
    adId: '.',
    heading: '',
    image: '',
    link: '',
    userId: localStorage.getItem("userId"),
    elementId: 'None',
    status: 'Draft'
  });
  const editorRef = useRef(null);

  useEffect(() => {
    const { advertisement } = location.state || {};
    if (advertisement) {
      setAdData(prevData => ({
        ...prevData,
        ...advertisement,
        adId: advertisement.adId || '.'
      }));
    }
  }, [location]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setAdData(prevData => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleEditorChange = (content, editor) => {
    setAdData(prevData => ({
      ...prevData,
      link: content
    }));
  };

  const handleSave = async () => {
    if (adData.adId !== '.') {
      try {
        const response = await api.put('Advertisement/UpdateDaftAdvertisement',adData);
        console.log('Response:', response.data);
        message.success('Quảng cáo đã được lưu thành công!');
        navigate('/user-ads');
    } catch (error) {
      console.error('Error:', error);
      message.error('Có lỗi xảy ra khi lưu quảng cáo. Vui lòng thử lại.');
    }
  } else 
  try {
    const response = await api.post('Advertisement/AddAdvertisementDraft', adData);
    console.log('Response:', response.data);
    message.success('Quảng cáo đã được lưu thành công!');
    navigate('/user-ads');
  } catch (error) {
    console.error('Error:', error);
    message.error('Có lỗi xảy ra khi lưu quảng cáo. Vui lòng thử lại.');
  }
};

const handleChoosePackage = () => {
  const updatedAdData = {
    ...adData,
    heading: adData.heading || document.querySelector('input[name="heading"]').value,
    image: adData.image || document.querySelector('input[name="image"]').value,
    link: adData.link || editorRef.current.getContent(),
    userId: localStorage.getItem("userId"),
    elementId: adData.elementId || 'None',
    status: 'Draft'
  };

  if (updatedAdData.heading && updatedAdData.image && updatedAdData.link) {
    localStorage.setItem('adData', JSON.stringify(updatedAdData));
    navigate('/choose-package', { state: { adData: updatedAdData } });
  } else {
    message.error('Vui lòng điền đầy đủ thông tin quảng cáo trước khi chọn gói.');
  }
};

  return (
    <div className="ads-container">
      <h1>{adData.adId !== '.' ? 'Chỉnh sửa quảng cáo' : 'Đăng quảng cáo mới'}</h1>
      <input
        type="text"
        name="heading"
        value={adData.heading}
        onChange={handleInputChange}
        placeholder="Tiêu đề quảng cáo"
        required
      />
      <input
        type="text"
        name="image"
        value={adData.image}
        onChange={handleInputChange}
        placeholder="URL hình ảnh"
        required
      />
      <Editor
        apiKey='48zvgxqbyrxhxjktp3nysk7hscrlqcz0143gyuhannv3rfv5'
        onInit={(evt, editor) => editorRef.current = editor}
        initialValue={adData.link || "<p>Viết nội dung quảng cáo của bạn ở đây.</p>"}
        init={{
          height: 500,
          plugins: [
            'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
            'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
            'insertdatetime', 'media', 'table', 'help', 'wordcount',
            'directionality emoticons template paste textcolor colorpicker'
          ],
          toolbar: 'undo redo | blocks | ' +
            'bold italic backcolor | alignleft aligncenter ' +
            'alignright alignjustify | bullist numlist outdent indent | ' +
            'removeformat | link image | help | emoticons | template',
          content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
        }}
        onEditorChange={handleEditorChange}
      />
      
      <div className="button-container">
        <Button className="action-button save-button" onClick={handleSave}>
          Lưu bản nháp
        </Button>
        <Button className="action-button" onClick={handleChoosePackage}>
          Chọn gói quảng cáo
        </Button>
      </div>
      
      {adData.link && (
        <div className="ad-preview">
          <h2>Nội dung quảng cáo:</h2>
          <div dangerouslySetInnerHTML={{ __html: adData.link }} />
        </div>
      )}
    </div>
  );
}

export default CreateAds;
