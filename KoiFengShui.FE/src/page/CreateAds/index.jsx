import React, { useState, useRef } from 'react';
import { Editor } from '@tinymce/tinymce-react';
import { Button, message } from 'antd';
import api from '../../config/axios';
import { useNavigate } from 'react-router-dom';
import './index.css';

function CreateAds() {
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
    try {
      const response = await api.post('Advertisement/AddAdvertisementDraft', adData);
      console.log('Response:', response.data);
      message.success('Quảng cáo đã được lưu thành công!');
    } catch (error) {
      console.error('Lỗi khi đăng quảng cáo:', error);
      message.error('Có lỗi xảy ra khi đăng quảng cáo. Vui lòng thử lại.');
    }
  };

  const handleChoosePackage = () => {
    navigate('/choose-package');
  };

  return (
    <div className="ads-container">
      <h1>Đăng quảng cáo mới</h1>
      <input
        type="text"
        name="heading"
        value={adData.heading}
        onChange={handleInputChange}
        placeholder="Tiêu đề quảng cáo"
      />
      <input
        type="text"
        name="image"
        value={adData.image}
        onChange={handleInputChange}
        placeholder="URL hình ảnh"
      />
      <Editor
        apiKey='48zvgxqbyrxhxjktp3nysk7hscrlqcz0143gyuhannv3rfv5'
        onInit={(evt, editor) => editorRef.current = editor}
        initialValue="<p>Viết nội dung quảng cáo của bạn ở đây.</p>"
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
        <Button onClick={handleSave} type="primary" className="action-button save-button">Lưu bản nháp</Button>
        <Button onClick={handleChoosePackage} type="primary" className="action-button choose-package-button">Chọn gói quảng cáo</Button>
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
