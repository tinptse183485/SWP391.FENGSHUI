import React, { useState, useRef } from 'react';
import { Editor } from '@tinymce/tinymce-react';
import api from '../../config/axios';

function ADS() {
  const [adData, setAdData] = useState({
    adId: 'AD001',
    heading: 'Quảng cáo 1',
    image: 'https://example.com/image.jpg',
    link: '',
    userId: 'Huy', // Bạn cần cung cấp userId từ hệ thống xác thực của bạn
    rank: 'Diamond',
    elementId: 'None',
    status: 'Pending'
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
      link: content // Lưu nội dung HTML vào trường link
    }));
  };

  const handleSubmit = async () => {
    try {
      const response = await api.post('Advertisement/AddAdvertisement', adData);
      console.log('Response:', response.data);
      // Lưu nội dung HTML vào trường link
      setAdData(prevData => ({
        ...prevData,
        link: adData.link // Giữ nguyên nội dung HTML đã nhập
      }));
      alert('Quảng cáo đã được đăng thành công! Đường dẫn: ' + location.href);
    } catch (error) {
      console.error('Lỗi khi đăng quảng cáo:', error);
      alert('Có lỗi xảy ra khi đăng quảng cáo. Vui lòng thử lại.');
    }
  };

  return (
    <div>
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
          menubar: false,
          plugins: [
            'advlist autolink lists link image charmap print preview anchor',
            'searchreplace visualblocks code fullscreen',
            'insertdatetime media table paste code help wordcount'
          ],
          toolbar: 'undo redo | formatselect | ' +
            'bold italic backcolor | alignleft aligncenter ' +
            'alignright alignjustify | bullist numlist outdent indent | ' +
            'removeformat | help',
          content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }'
        }}
        onEditorChange={handleEditorChange}
      />
      <button onClick={handleSubmit}>Đăng quảng cáo</button>
      
      {/* Hiển thị nội dung quảng cáo */}
      {adData.link && (
        <div>
          <h2>Nội dung quảng cáo:</h2>
          <div dangerouslySetInnerHTML={{ __html: adData.link }} />
        </div>
      )}
    </div>
  );
}

export default ADS;
