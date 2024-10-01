import React, { useEffect, useState } from "react";
import { Form, DatePicker, Select, Button } from "antd";
import api from "../../config/axios";
import { toast } from "react-toastify";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
import "./index.css";

const { Option } = Select;

function ComputeCompability() {
  const [form] = Form.useForm();
  const [fishList, setFishList] = useState([]);
  const [pondShapes, setPondShapes] = useState([]);
  const [pondDirections, setPondDirections] = useState([]);
  const [compatibilityResult, setCompatibilityResult] = useState(null);
  const [selectedFish, setSelectedFish] = useState(null);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const [fishResponse, shapeResponse, directionResponse] =
        await Promise.all([
          api.get("getKoiList"),
          api.get("getPondShapes"),
          api.get("getPondDirections"),
        ]);
      setFishList(fishResponse.data);
      setPondShapes(shapeResponse.data);
      setPondDirections(directionResponse.data);
    } catch (error) {
      toast.error("Error fetching data");
    }
  };

  const onFinish = async (values) => {
    try {
      const response = await api.post("calculateCompatibility", values);
      toast.success("Calculation successful");
      setCompatibilityResult(response.data); // Lưu kết quả vào state
    } catch (error) {
      toast.error("Error calculating compatibility");
      setCompatibilityResult(null); // Reset kết quả nếu có lỗi
      console.error("API error:", error);
    }
  };

  return (
    <>
      <HeaderTemplate></HeaderTemplate>
      <div className="compute-container">
        <h1>Tính toán độ tương thích</h1>
        <div className="compute-content">
          <div className="filter">test</div>
          <div className="form-compute">
            <Form
              form={form}
              onFinish={onFinish}
              layout="vertical"
              className="compatibility-form"
            >
              <Form.Item
                name="birthdate"
                label="Ngày tháng năm sinh"
                rules={[{ required: true, message: "Vui lòng chọn ngày sinh" }]}
              >
                <DatePicker style={{ width: "100%" }} />
              </Form.Item>

              <Form.Item
                label="Chọn loại cá"
                name="selectedFish"
                rules={[{ required: true, message: "Vui lòng chọn loại cá" }]}
              >
                <div className="fish-list">
                  {fishList.map((fish) => (
                    <div
                      key={fish.id}
                      className={`fish-card ${
                        selectedFish === fish.id ? "selected" : ""
                      }`}
                      onClick={() => {
                        setSelectedFish(fish.id);
                        form.setFieldsValue({ selectedFish: fish.id });
                      }}
                    >
                      <img src={fish.imageUrl} alt={fish.name} />
                      <p>{fish.name}</p>
                    </div>
                  ))}
                </div>
              </Form.Item>

              <Form.Item
                name="pondShape"
                label="Hình dáng hồ"
                rules={[
                  { required: true, message: "Vui lòng chọn hình dáng hồ" },
                ]}
              >
                <Select placeholder="Chọn hình dáng hồ">
                  {pondShapes.map((shape) => (
                    <Option key={shape.id} value={shape.id}>
                      {shape.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>

              <Form.Item
                name="pondDirection"
                label="Hướng đặt hồ"
                rules={[
                  { required: true, message: "Vui lòng chọn hướng đặt hồ" },
                ]}
              >
                <Select placeholder="Chọn hướng đặt hồ">
                  {pondDirections.map((direction) => (
                    <Option key={direction.id} value={direction.id}>
                      {direction.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>

              <Form.Item>
                <Button type="primary" htmlType="submit">
                  Tính toán
                </Button>
              </Form.Item>
            </Form>

            {compatibilityResult && (
              <div className="result-section">
                <h2>Kết quả tính toán</h2>
                <p>Độ tương thích: {compatibilityResult.compatibilityScore}</p>
                <p>Nhận xét: {compatibilityResult.comment}</p>
                {/* Thêm các thông tin khác tùy theo dữ liệu API trả về */}
              </div>
            )}
          </div>
        </div>
      </div>
      <FooterPage></FooterPage>
    </>
  );
}

export default ComputeCompability;
