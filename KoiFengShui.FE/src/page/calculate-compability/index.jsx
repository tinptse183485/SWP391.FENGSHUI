import { useEffect, useState } from "react";
import { Form, DatePicker, Select, Button, Radio } from "antd";
import api from "../../config/axios";
import { toast } from "react-toastify";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
import "./index.css";

const { Option } = Select;

function ComputeCompability() {
  const [form] = Form.useForm();

  const [filteredFishList, setFilteredFishList] = useState([]);

  const [fishList, setFishList] = useState([]);
  const [selectedFish, setSelectedFish] = useState(null);

  const [pondShapes, setPondShapes] = useState([]);
  const [selectedPondShape, setSelectedPondShape] = useState(null);

  const [pondDirections, setPondDirections] = useState([]);

  const [compatibilityResult, setCompatibilityResult] = useState(null);

  const [elements, setElements] = useState([]);
  const [elementFilter, setElementFilter] = useState("all");

  const [colors, setColors] = useState([]);
  const [colorFilter, setColorFilter] = useState("all");

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    if (colorFilter === "all") {
      setFilteredFishList(fishList); // Nếu không chọn màu, hiển thị toàn bộ danh sách cá
    } else {
      fetchFishByColor();
    }
  }, [colorFilter, fishList]);

  const fetchData = async () => {
    try {
      const [
        fishResponse,
        shapeResponse,
        directionResponse,
        elementResponse,
        colorResponse,
      ] = await Promise.all([
        api.get("getKoiList"),
        api.get("getPondShapes"),
        api.get("getPondDirections"),
        api.get("getElements"),
        api.get("getColors"),
      ]);
      setFishList(fishResponse.data);
      setFilteredFishList(fishResponse.data); // Đặt danh sách cá ban đầu
      setPondShapes(shapeResponse.data);
      setPondDirections(directionResponse.data);
      setElements(elementResponse.data);
      setColors(colorResponse.data);
    } catch (error) {
      toast.error("Error fetching data");
    }
  };

  const fetchFishByColor = async () => {
    try {
      const response = await api.get(`getKoiByColor/${colorFilter}`);
      setFilteredFishList(response.data);
    } catch (error) {
      toast.error("Error fetching fish by color");
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

  // const filteredFishList = fishList.filter((fish) => {
  //   return (
  //     (elementFilter === "all" || fish.element === elementFilter) &&
  //     (colorFilter === "all" || fish.color === colorFilter)
  //   );
  // });

  return (
    <>
      <HeaderTemplate></HeaderTemplate>
      <div className="compute-container">
        <h1>Tính toán độ thích hợp</h1>
        <div className="compute-content">
          <div className="filter">
            <h3>Bộ lọc</h3>
            <Form layout="vertical">
              <Form.Item label="Bảng mệnh">
                <Select value={elementFilter} onChange={setElementFilter}>
                  <Option value="all">Tất cả</Option>
                  {elements.map((element) => (
                    <Option key={element.id} value={element.id}>
                      {element.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>

              <Form.Item label="Màu sắc">
                <Select value={colorFilter} onChange={setColorFilter}>
                  <Option value="all">Tất cả</Option>
                  {colors.map((color) => (
                    <Option key={color.id} value={color.id}>
                      {color.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>
            </Form>
          </div>

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
                <DatePicker format="YYYY-MM-DD" style={{ width: "100%" }} />
              </Form.Item>

              <Form.Item
                label="Gender"
                name="Gender"
                rules={[
                  { required: true, message: "Please select your gender!" },
                ]}
              >
                <Form.Item name="Gender" noStyle>
                  <Radio.Group>
                    <Radio value="male">Nam</Radio>
                    <Radio value="female">Nữ</Radio>
                  </Radio.Group>
                </Form.Item>
              </Form.Item>

              <Form.Item
                label="Chọn loại cá"
                name="selectedFish"
                rules={[{ required: true, message: "Vui lòng chọn loại cá" }]}
              >
                <div className="fish-list">
                  {filteredFishList.map((fish) => (
                    <div
                      key={fish.koiType}
                      className={`fish-card ${
                        selectedFish === fish.koiType ? "selected" : ""
                      }`}
                      onClick={() => {
                        setSelectedFish(fish.koiType);
                        form.setFieldsValue({ selectedFish: fish.koiType });
                      }}
                    >
                      <img src={fish.image} alt={fish.koiType} />
                      <p>{fish.koiType}</p>
                      <p>{fish.description}</p>
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
                <div className="pond-shape-list">
                  {pondShapes.map((shape) => (
                    <div
                      key={shape.shapeId}
                      className={`pond-shape-card ${
                        selectedPondShape === shape.shapeId ? "selected" : ""
                      }`}
                      onClick={() => {
                        setSelectedPondShape(shape.shapeId);
                        form.setFieldsValue({
                          selectedPondShape: shape.shapeId,
                        });
                      }}
                    >
                      <img src={shape.image} alt={shape.shapeId} />
                      <p>{shape.shapeId}</p>
                    </div>
                  ))}
                </div>
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
                    <Option
                      key={direction.directionId}
                      value={direction.directionId}
                    >
                      {direction.directionId}
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
