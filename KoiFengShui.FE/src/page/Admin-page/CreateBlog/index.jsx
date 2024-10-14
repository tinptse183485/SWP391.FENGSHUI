import React, { useState, useRef, useEffect } from 'react';
import { Editor } from '@tinymce/tinymce-react';
import { Button, message } from 'antd';
import api from '../../../config/axios';
import { useNavigate, useLocation } from 'react-router-dom';
import './index.css';

function CreateBlog() {
  const location = useLocation();
  const navigate = useNavigate();

  const [blogData, setBlogData] = useState({
    blogId: '.',
    heading: '',
    image: '',
    link: '',
    status: 'Draft'
  });
  const editorRef = useRef(null);

  useEffect(() => {
    const { blog } = location.state || {};
    if (blog) {
      setBlogData(prevData => ({
        ...prevData,
        ...blog
      }));
    }
  }, [location.state]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setBlogData(prevData => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleEditorChange = (content, editor) => {
    setBlogData(prevData => ({
      ...prevData,
      link: content
    }));
  };

  const handleSave = async (status) => {
    const updatedBlogData = {
      ...blogData,
      status: status
    };

    try {
      let response;
      if (blogData.blogId === '.') {
        response = await api.post('Blog/AddBlog', updatedBlogData);
      } else {
        response = await api.put('Blog/UpdateBlog', updatedBlogData);
      }
      console.log('Response:', response.data);
      message.success(status === 'Draft' ? 'Bài viết đã được lưu thành công!' : 'Bài viết đã được đăng thành công!');
      navigate('/dashboard/blog');
    } catch (error) {
      console.error('Error:', error);
      message.error('Có lỗi xảy ra khi lưu bài viết. Vui lòng thử lại.');
    }
  };

  return (
    <div className="blog-container">
      <h1>{blogData.blogId !== '.' ? 'Chỉnh sửa bài viết' : 'Tạo bài viết mới'}</h1>

      <input
        type="text"
        name="heading"
        value={blogData.heading}
        onChange={handleInputChange}
        placeholder="Tiêu đề bài viết"
        required
      />
      <input
        type="text"
        name="image"
        value={blogData.image}
        onChange={handleInputChange}
        placeholder="URL hình ảnh"
        required
      />
      <Editor
        apiKey='48zvgxqbyrxhxjktp3nysk7hscrlqcz0143gyuhannv3rfv5'
        onInit={(evt, editor) => {
          editorRef.current = editor;
          if (blogData.link) {
            editor.setContent(blogData.link);
          }
        }}
        value={blogData.link || "<p>Viết nội dung bài viết của bạn ở đây.</p>"}
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
          directionality: 'ltr',
          language: 'vi_VN',
        }}
        onEditorChange={handleEditorChange}
      />

      <div className="button-container">
        <Button className="action-button save-button" onClick={() => handleSave('Draft')}>
          Lưu bản nháp
        </Button>
        <Button className="action-button publish-button" onClick={() => handleSave('Published')}>
          Đăng bài
        </Button>
      </div>
      
      {blogData.link && (
        <div className="blog-preview">
          <h2>Nội dung bài viết:</h2>
          <div dangerouslySetInnerHTML={{ __html: blogData.link }} />
        </div>
      )}
    </div>
  );
}

export default CreateBlog;
