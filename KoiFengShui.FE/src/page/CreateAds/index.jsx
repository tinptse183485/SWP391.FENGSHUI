import React, { useState, useRef, useEffect } from 'react';
import { Editor } from '@tinymce/tinymce-react';
import { Button, message, Radio } from 'antd';
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

  const [elements, setElements] = useState([]);

  useEffect(() => {
    const { advertisement } = location.state || {};
    if (advertisement) {
      setAdData(prevData => ({
        ...prevData,
        ...advertisement
      }));
    }
    fetchElements();
  }, [location.state]);

  useEffect(() => {
    fetchElements();
  }, []);

  const fetchElements = async () => {
    try {
      const response = await api.get('Element/GetAllElement');
      setElements(response.data);
    } catch (error) {
      console.error('Error fetching elements:', error);
      message.error('Failed to fetch elements. Please try again.');
    }
  };
  const handleElementChange = (e) => {
    setAdData(prevData => ({
      ...prevData,
      elementId: e.target.value
    }));
  };
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
  //   if (adData.adId !== '.') {
  //     try {
  //       const response = await api.put('Advertisement/UpdateDaftAdvertisement',adData);
  //       console.log('Response:', response.data);
  //       message.success('Quảng cáo đã được lưu thành công!');
  //       navigate('/user-ads');
  //   } catch (error) {
  //     console.error('Error:', error);
  //     message.error('Có lỗi xảy ra khi lưu quảng cáo. Vui lòng thử lại.');
  //   }
  // } else 
  try {
    const response = await api.post('Advertisement/SaveAdvertisementDraft', adData);
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
       <div className="element-selection">
      <h3>Chọn phần tử cho quảng cáo:</h3>
      <Radio.Group onChange={handleElementChange} value={adData.elementId}>
        {elements.map(element => (
          <Radio key={element.elementId} value={element.elementId}>
            {element.elementId}
          </Radio>
        ))}
      </Radio.Group>
    </div>
      
      <Editor
        apiKey='48zvgxqbyrxhxjktp3nysk7hscrlqcz0143gyuhannv3rfv5'

        onInit={(evt, editor) => {
          editorRef.current = editor;
          if (adData.link) {
            editor.setContent(adData.link);
          }
        }}
        value={adData.link || "<p>Viết nội dung quảng cáo của bạn ở đây.</p>"}

        init={{
          height: 500,
          plugins: [
            'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
            'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
            'insertdatetime', 'media', 'table', 'help', 'wordcount',

            'directionality', 'emoticons', 'template', 'paste', 'textcolor', 'colorpicker'

          ],
          toolbar: 'undo redo | blocks | ' +
            'bold italic backcolor | alignleft aligncenter ' +
            'alignright alignjustify | bullist numlist outdent indent | ' +

            'removeformat | link image | help | emoticons | template | ltr rtl',
          content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
          directionality: 'ltr', // Đặt hướng văn bản mặc định là từ trái sang phải
          language: 'vi_VN', // Đặt ngôn ngữ là tiếng Việt

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
