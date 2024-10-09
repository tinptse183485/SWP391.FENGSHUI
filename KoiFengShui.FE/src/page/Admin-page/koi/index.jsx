
import { useEffect, useState } from "react"; // Thêm useEffect và useState
// import "./index.css";
import api from "../../../config/axios";
import { Button, Form, Image, Input, Modal, Table, Upload } from "antd";
import { toast } from "react-toastify";
import { PlusOutlined } from "@ant-design/icons";
import { useForm } from "antd/es/form/Form";
import uploadFile from "../../../utils/file";

const Koi = () => {
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu
  const [openModal, setOpenModal] = useState(false); //mac dinh modal dong
  const [submitting, setSubmitting] = useState(false);
  const [form] = useForm();
  const [previewOpen, setPreviewOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState("");
  const [fileList, setFileList] = useState([]);


  useEffect(() => {
    fetchData();
  }, []); // Chạy một lần khi component mount

  const fetchData = async () => {
    try {
      const response = await api.get("KoiVariety/GetAllKoi"); // Thay thế 'API_URL' bằng URL thực tế
      console.log("Fetched data:", response.data); // Kiểm tra dữ liệu nhận được
      setData(response.data); // Lưu trữ dữ liệu vào state
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const columns = [
    {
      title: "Koi Type",
      dataIndex: "koiType",
    },
    {
      title: "Image",
      dataIndex: "image",
      render: (image) => {
        return <Image src={image} alt="" width={100}></Image>;
      },
    },
    {
      title: "Element",
      dataIndex: "element",
    },
    {
      title: "Description",
      dataIndex: "description",
    },
  ];

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const handleCreateKoi = async (koi) => {
    // xu ly lay thong tin student trong form
    //post xuong API
    // console.log(student);

    if (fileList.length > 0) {
      const file = fileList[0];
      console.log(file);
      const url = await uploadFile(file.originFileObj);
      koi.image = url;
    }
    //day data xuong cho BE
    try {
      setSubmitting(true); //bat dau load
      const response = await api.post(api, koi);
      // => thanh cong
      console.log(response.data);
      toast.success("Tạo mới thành công");
      setOpenModal(false);
      //clear du lieu cu
      form.resetFields();
      fetchData();
    } catch (err) {
      toast.error(err);
    } finally {
      setSubmitting(false);
    }
  };

  // const handleDeleteStudent = async (studentId) => {
  //   try {
  //     await axios.delete(`${api}/${studentId}`);
  //     toast.success("Delete successfully");
  //     fetchStudent();
  //   } catch (ex) {
  //     toast.error("Failed to delete student");
  //   }
  // };

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
  };
  const handleChange = ({ fileList: newFileList }) => setFileList(newFileList);
  const uploadButton = (
    <button
      style={{
        border: 0,
        background: "none",
      }}
      type="button"
    >
      <PlusOutlined />
      <div
        style={{
          marginTop: 8,
        }}
      >
        Upload
      </div>
    </button>
  );

  return (
    <div>
      {/* className="dashboard-container" */}
      <h1>Quản lý cá Koi</h1>
      <Button onClick={handleOpenModal}>Tạo mới loại cá Koi</Button>
      <Table dataSource={data} columns={columns} />
      <Modal
        confirmLoading={submitting}
        onOk={() => form.submit()}
        title="Tạo một giống cá Koi mới"
        open={openModal}
        onCancel={handleCloseModal}
      >
        <Form onFinish={handleCreateKoi} form={form}>
          <Form.Item
            label="Giống cá Koi"
            name="koiType"
            rules={[
              {
                required: true,
                message: "Hãy nhập loại cá Koi",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Mệnh của cá"
            name="element"
            rules={[
              {
                required: true,
                message: "Hãy nhập mệnh của cá Koi",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Thông tin giới thiệu"
            name="description"
            rules={[
              {
                required: true,
                message: "Hãy nhập mô tả của cá Koi",
              },
            ]}
          >
            <Input.TextArea />
          </Form.Item>
          <Form.Item label="image" name="image">
            <Upload
              action="https://660d2bd96ddfa2943b33731c.mockapi.io/api/upload"
              listType="picture-card"
              fileList={fileList}
              onPreview={handlePreview}
              onChange={handleChange}
            >
              {fileList.length >= 8 ? null : uploadButton}
            </Upload>
          </Form.Item>
        </Form>
      </Modal>

      {previewImage && (
        <Image
          wrapperStyle={{
            display: "none",
          }}
          preview={{
            visible: previewOpen,
            onVisibleChange: (visible) => setPreviewOpen(visible),
            afterOpenChange: (visible) => !visible && setPreviewImage(""),
          }}
          src={previewImage}
        />
      )}

    </div>
  );
};

export default Koi;
