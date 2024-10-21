import React, { useState, useRef, useEffect } from "react";
import { Editor } from "@tinymce/tinymce-react";

import { Button, message, Radio, Upload, Modal, Image } from "antd";
import { PlusOutlined } from "@ant-design/icons";

import api from "../../config/axios";
import { useNavigate, useLocation } from "react-router-dom";
import "./index.css";
import uploadFile from "../../utils/file"; // Đảm bảo import đúng đường dẫn

function CreateAds() {
  const location = useLocation();
  const navigate = useNavigate();

  const [adData, setAdData] = useState({
    adId: ".",
    heading: "",
    image: "",
    link: "",
    userId: localStorage.getItem("userId"),
    elementId: "None",
    status: "Draft",
  });
  const editorRef = useRef(null);

  const [elements, setElements] = useState([]);

  const [fileList, setFileList] = useState([]);
  const [previewOpen, setPreviewOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState("");
  const [previewTitle, setPreviewTitle] = useState("");

  useEffect(() => {
    const { advertisement } = location.state || {};
    if (advertisement) {
      setAdData((prevData) => ({
        ...prevData,
        ...advertisement,
      }));
      if (advertisement.image) {
        setFileList([
          {
            uid: "-1",
            name: "image.png",
            status: "done",
            url: advertisement.image,
          },
        ]);
      }
    }

    fetchElements();
  }, [location.state]);

  useEffect(() => {
    fetchElements();
  }, []);

  const fetchElements = async () => {
    try {
      const response = await api.get("Element/GetAllElement");
      setElements(response.data);
    } catch (error) {
      console.error("Error fetching elements:", error);
      message.error("Failed to fetch elements. Please try again.");
    }
  };
  const handleElementChange = (e) => {
    setAdData((prevData) => ({
      ...prevData,
      elementId: e.target.value,
    }));
  };
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setAdData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleEditorChange = (content, editor) => {
    setAdData((prevData) => ({
      ...prevData,

      link: content,
    }));
  };

  const getBase64 = (file) =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });

  const handlePreview = async (file) => {
    if (!file.url && !file.preview) {
      file.preview = await getBase64(file.originFileObj);
    }
    setPreviewImage(file.url || file.preview);
    setPreviewOpen(true);
    setPreviewTitle(
      file.name || file.url.substring(file.url.lastIndexOf("/") + 1)
    );
  };

  const handleChange = ({ fileList: newFileList }) => {
    setFileList(newFileList);
    if (newFileList.length > 0) {
      setAdData((prevData) => ({
        ...prevData,
        image: newFileList[0].url || newFileList[0].thumbUrl,
      }));
    } else {
      setAdData((prevData) => ({
        ...prevData,
        image: "",
      }));
    }
  };

  const handleCancel = () => setPreviewOpen(false);

  const uploadButton = (
    <button
      style={{
        border: 0,
        background: "none",
      }}
      type="button"
    >
      <PlusOutlined />
      <div style={{ marginTop: 8 }}>Tải ảnh bìa lên</div>
    </button>
  );

  const handleSave = async () => {
    try {
      let imageUrl = adData.image;
      if (fileList.length > 0 && fileList[0].originFileObj) {
        const file = fileList[0].originFileObj;
        imageUrl = await uploadFile(file);
      }

      const adDataToSave = {
        ...adData,
        image: imageUrl,
      };

      const response = await api.post(
        "Advertisement/SaveAdvertisementDraft",
        adDataToSave
      );
      console.log("Response:", response.data);
      message.success("Quảng cáo đã được lưu thành công!");
      navigate("/user-ads");
    } catch (error) {
      console.error("Error:", error);
      message.error("Có lỗi xảy ra khi lưu quảng cáo. Vui lòng thử lại.");
    }
  };

  const handleChoosePackage = () => {
    const updatedAdData = {
      ...adData,

      heading: adData.heading || document.querySelector('input[name="heading"]').value,
      image: adData.image || (fileList.length > 0 ? fileList[0].url || fileList[0].thumbUrl : ''),

      link: adData.link || editorRef.current.getContent(),
      userId: localStorage.getItem("userId"),
      elementId:
        adData.elementId ||
        document.querySelector('select[name="elementId"]').value,
      status: "Draft",
    };


    if (updatedAdData.heading && updatedAdData.image && updatedAdData.link && updatedAdData.elementId !== 'None') {
      localStorage.setItem('adData', JSON.stringify(updatedAdData));
      navigate('/choose-package', { state: { adData: updatedAdData } });

    } else {
      message.error(
        "Vui lòng điền đầy đủ thông tin quảng cáo và chọn mệnh trước khi chọn gói."
      );
    }
  };

  return (
    <div className="ads-container">
      <h1>
        {adData.adId !== "." ? "Chỉnh sửa quảng cáo" : "Đăng quảng cáo mới"}
      </h1>
      <div className="input-container">
        <div className="input-column image-upload">
          <Upload
            listType="picture-card"
            fileList={fileList}
            onPreview={handlePreview}
            onChange={handleChange}
            beforeUpload={() => false}
           
          >

            {fileList.length >= 1 ? null : uploadButton}
          </Upload>
        </div>
        <div className="input-row">
          <div className="input-column">
            <input
              type="text"
              name="heading"
              value={adData.heading}
              onChange={handleInputChange}
              placeholder="Tiêu đề quảng cáo"
              required
            />
          </div>
          <div className="input-column">
            <div className="element-selection">
              <select
                name="elementId"
                value={adData.elementId }
                onChange={handleElementChange}
                required
              >
                <option value="">Chọn mệnh cho quảng cáo</option>
                {elements.map(element => (
                  <option key={element.elementId} value={element.elementId}>
                    {element.elementId}
                  </option>
                ))}
              </select>
            </div>
          </div>

        </div>
      </div>
      <Editor
        apiKey="7badstws748pqjv54m7auuzpobs4nozi2we7cz1vz5mh63lh"
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
            "advlist",
            "autolink",
            "lists",
            "link",
            "image",
            "charmap",
            "preview",
            "anchor",
            "searchreplace",
            "visualblocks",
            "code",
            "fullscreen",
            "insertdatetime",
            "media",
            "table",
            "help",
            "wordcount",

            "directionality",
            "emoticons",
            "template",
            "paste",
            "textcolor",
            "colorpicker",
          ],
          toolbar:
            "undo redo | blocks | " +
            "bold italic backcolor | alignleft aligncenter " +
            "alignright alignjustify | bullist numlist outdent indent | " +
            "removeformat | link image | help | emoticons | template | ltr rtl",
          content_style:
            "body { font-family:Helvetica,Arial,sans-serif; font-size:16px }",
          directionality: "ltr", // Đặt hướng văn bản mặc định là từ trái sang phải
          language: "vi_VN", // Đặt ngôn ngữ là tiếng Việt
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
      {previewImage && (
        <Image
          style={{
            display: "none",
          }}
          src={previewImage}
          preview={{
            visible: previewOpen,
            onVisibleChange: (visible) => setPreviewOpen(visible),
          }}
        />
      )}
    </div>
  );
}

export default CreateAds;
