import { useEffect, useState } from "react"; // Thêm useEffect và useState
// import "./index.css";
import api from "../../../config/axios";

import {
  Button,
  Form,
  Image,
  Input,
  Modal,
  Table,
  Upload,
  Space,
  Select,
  Popconfirm,
} from "antd";
import { toast } from "react-toastify";
import {
  PlusOutlined,
  MinusCircleOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";

import { useForm } from "antd/es/form/Form";
import uploadFile from "../../../utils/file";

const { Option } = Select;

const Koi = () => {
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu
  const [openModal, setOpenModal] = useState(false); //mac dinh modal dong
  const [submitting, setSubmitting] = useState(false);
  const [form] = useForm();
  const [previewOpen, setPreviewOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState("");
  const [fileList, setFileList] = useState([]);
  const [colors, setColors] = useState([]);

  const [colorModalVisible, setColorModalVisible] = useState(false);
  const [colorForm] = Form.useForm();

  useEffect(() => {
    fetchDataAndColors();
    fetchColors();
  }, []); // Chạy một lần khi component mount

  const fetchDataAndColors = async () => {
    try {
      const [koiResponse, colorResponse] = await Promise.all([
        api.get("KoiVariety/GetAllKoi"),
        api.get("TypeColor/GetAllTypeColor"),
      ]);

      const koiData = koiResponse.data;
      const colorData = colorResponse.data;

      const combinedData = koiData.map((koi) => {
        const koiColors = colorData.filter(
          (color) => color.koiType === koi.koiType
        );
        return {
          ...koi,
          colors: koiColors,
        };
      });

      setData(combinedData);
    } catch (error) {
      console.error("Error fetching data:", error);
      toast.error("Không thể tải dữ liệu");
    }
  };

  const fetchColors = async () => {
    try {
      const response = await api.get("Color/GetAllColor");
      // Chỉ lấy colorId từ dữ liệu API
      const colorIds = response.data.map((color) => color.colorId);
      setColors(colorIds);
    } catch (error) {
      console.error("Error fetching colors:", error);
      toast.error("Không thể tải danh sách màu");
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
    {
      title: "Colors",
      dataIndex: "colors",
      render: (colors) => (
        <ul>
          {colors.map((color, index) => (
            <li key={index}>
              {color.colorId}: {color.percentage.toFixed(2)}
            </li>
          ))}
        </ul>
      ),
    },
    {
      title: "Action",
      dataIndex: "koiType",
      key: "koiType",
      render: (koiType, category) => (
        <>
          {/* <Button
            type="primary"
            onClick={() => {
              setOpenModal(true);
              form.setFieldsValue(category);
            }}
          >
            Edit
          </Button> */}

          <Popconfirm
            title="Delete"
            description="Delete?"
            onConfirm={() => handleDelete(koiType)}
          >
            <Button type="primary" danger>
              Delete
            </Button>
          </Popconfirm>
        </>
      ),
    },
  ];

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const handleCreateKoi = async (values) => {
    // Kiểm tra tổng tỉ trọng

    const totalPercentage = values.colors.reduce(
      (sum, color) => sum + parseFloat(color.percentage || 0),
      0
    );

    if (Math.abs(totalPercentage - 1) > 0.001) {
      // Sử dụng sai số nhỏ để xử lý lỗi làm tròn số thập phân
      toast.error("Tổng tỉ trọng màu phải bằng 1");
      return;
    }

    try {
      setSubmitting(true);

      let imageUrl = "";
      if (fileList.length > 0) {
        const file = fileList[0].originFileObj;
        imageUrl = await uploadFile(file);
      }

      const koi = {
        ...values,
        image: imageUrl,
        colors: values.colors.map((color) => ({
          colorId: color.colorId.toString(),
          percentage: parseFloat(color.percentage),
        })),
      };
      const response = await api.post("KoiVariety/AddKoiAndTypeColor", koi);

      console.log(response.data);
      toast.success("Tạo mới thành công");
      setOpenModal(false);
      form.resetFields();
      fetchDataAndColors();
    } catch (err) {
      toast.error(err.response?.data?.message || "Có lỗi xảy ra");
    } finally {
      setSubmitting(false);
    }
  };

  const handleAddColor = async (values) => {
    const formattedValues = {
      colorID: values.colorID,
      elementPoint: Object.entries(values.elementPoints).map(
        ([elementID, point]) => ({
          elementID,
          point: parseFloat(point),
        })
      ),
    };

    try {
      await api.post("Color/AddColorAndElement", formattedValues);
      toast.success("Màu mới đã được thêm thành công");
      setColorModalVisible(false);
      colorForm.resetFields();
      fetchColors();
    } catch (error) {
      console.error("Error adding new color:", error);
      toast.error("Không thể thêm màu mới");
    }
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
      <Space>
        <Button onClick={handleOpenModal}>Tạo mới loại cá Koi</Button>
        <Button onClick={() => setColorModalVisible(true)}>
          Quản lý màu sắc
        </Button>
      </Space>
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
              listType="picture-card"
              fileList={fileList}
              onPreview={handlePreview}
              onChange={handleChange}
              beforeUpload={() => false} // Prevent auto upload
            >
              {fileList.length >= 1 ? null : uploadButton}
            </Upload>
          </Form.Item>
          <Form.List name="colors">
            {(fields, { add, remove }) => (
              <>
                {fields.map(({ key, name, ...restField }) => (
                  <Space
                    key={key}
                    style={{ display: "flex", marginBottom: 8 }}
                    align="baseline"
                  >
                    <Form.Item
                      {...restField}
                      name={[name, "colorId"]}
                      rules={[{ required: true, message: "Chọn màu" }]}
                    >
                      <Select style={{ width: 120 }} placeholder="Chọn màu">
                        {colors.map((colorId) => (
                          <Option key={colorId} value={colorId}>
                            {colorId}
                          </Option>
                        ))}
                      </Select>
                    </Form.Item>
                    <Form.Item
                      {...restField}
                      name={[name, "percentage"]}
                      rules={[
                        { required: true, message: "Nhập tỉ trọng" },
                        {
                          validator: async (_, value) => {
                            if (value && (value < 0 || value > 1)) {
                              throw new Error("Tỉ trọng phải từ 0 đến 1");
                            }
                          },
                        },
                      ]}
                    >
                      <Input
                        type="number"
                        step="0.01"
                        placeholder="Tỉ trọng (0-1)"
                      />
                    </Form.Item>
                    <MinusCircleOutlined onClick={() => remove(name)} />
                  </Space>
                ))}
                <Form.Item>
                  <Space>
                    <Button
                      type="dashed"
                      onClick={() => add()}
                      icon={<PlusOutlined />}
                    >
                      Thêm màu
                    </Button>
                  </Space>
                </Form.Item>
              </>
            )}
          </Form.List>
        </Form>
      </Modal>

      <Modal
        title="Quản lý màu sắc"
        open={colorModalVisible}
        onCancel={() => setColorModalVisible(false)}
        footer={null}
      >
        <Form form={colorForm} onFinish={handleAddColor}>
          <Form.Item
            name="colorID"
            label="Tên màu"
            rules={[{ required: true, message: "Vui lòng nhập tên màu" }]}
          >
            <Input />
          </Form.Item>
          {["Hỏa", "Thủy", "Mộc", "Kim", "Thổ"].map((element) => (
            <Form.Item
              key={element}
              name={["elementPoints", element]}
              label={element}
              rules={[{ required: true, message: "Vui lòng chọn điểm" }]}
            >
              <Select placeholder="Chọn điểm">
                <Option value="0.25">0.25</Option>
                <Option value="0.5">0.5</Option>
                <Option value="0.75">0.75</Option>
                <Option value="1">1</Option>
              </Select>
            </Form.Item>
          ))}
          <Form.Item>
            <Button type="primary" htmlType="submit">
              Thêm màu mới
            </Button>
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
