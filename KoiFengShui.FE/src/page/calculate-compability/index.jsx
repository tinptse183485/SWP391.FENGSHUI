import { useEffect, useState } from "react";

import {
  Form,
  DatePicker,
  Select,
  Button,
  Radio,
  Input,
  Modal,
  Slider,
  InputNumber,
} from "antd";

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

  // Tạo state cho modal và thông tin về các màu của cá
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedFishDetail, setSelectedFishDetail] = useState(null);
  const [colorWeights, setColorWeights] = useState([]);

  const [allFishColorWeights, setAllFishColorWeights] = useState({});

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    const fetchFilteredFishList = async () => {
      let tempFilteredFishList = [];

      if (colorFilter === "all") {
        // Nếu không chọn màu, chỉ lọc theo element
        tempFilteredFishList = fishList.filter(
          (fish) => elementFilter === "all" || fish.element === elementFilter
        );
      } else {
        // Gọi API để lấy cá theo màu
        try {
          const response = await api.get(
            `KoiVariety/GetListKoiByColor?color=${colorFilter}`
          );
          const fishByColor = response.data;

          // Lọc theo bảng mệnh sau khi có danh sách cá theo màu
          tempFilteredFishList = fishByColor.filter(
            (fish) => elementFilter === "all" || fish.element === elementFilter
          );
        } catch (error) {
          toast.error("Error fetching fish by color");
          return; // Trả về để không cập nhật danh sách cá nếu có lỗi
        }
      }

      setFilteredFishList(tempFilteredFishList);
    };

    fetchFilteredFishList();
  }, [colorFilter, elementFilter, fishList]);

  useEffect(() => {
    if (selectedFishDetail && colorWeights) {
      form.setFieldsValue({
        selectedFish: {
          koiType: selectedFishDetail.koiType,
          colors: colorWeights
        }
      });
    }
  }, [selectedFishDetail, colorWeights]);

  const fetchData = async () => {
    try {
      const [
        fishResponse,
        shapeResponse,
        directionResponse,
        elementResponse,
        colorResponse,
      ] = await Promise.all([
        api.get("KoiVariety/GetAllKoi"),
        api.get("Shape/GetAllShape"),
        api.get("Direction/GetAllDirection"),
        api.get("Element/GetAllElement"),
        api.get("Color/GetAllColor"),
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

  const onFinish = async (values) => {
    try {
      const {
        birthdate,
        Gender,
        selectedFish,
        selectedPondShape,
        pondDirection,
      } = values;

      // Chuẩn bị payload
      const payload = [
        {
          koiType: selectedFish.koiType,
          colors: selectedFish.colors.map((color, index, array) => ({
            colorId: color.colorId,
            percentage: color.percentage,
          })).filter(color => color.percentage > 0)
        }
      ];

      console.log("Payload gửi đi:", payload);

      // Gọi API sử dụng query params thay vì body
      const response = await api.post(
        `Compatibility/GetTheCompatibilityOfUserByListFish?ShapeID=${selectedPondShape}&Direction=${pondDirection}&DOB=${birthdate.format("YYYY-MM-DD")}&Gender=${Gender}`,
        payload
      );

      console.log("API Response:", response.data);

      if (response.data) {
        toast.success("Calculation successful");
        setCompatibilityResult(response.data.compatibility);
      } else {
        toast.error("No data received from the server");
      }
    } catch (error) {
      console.error("API error:", error);
      toast.error(error.response?.data || "Error calculating compatibility");
      setCompatibilityResult(null);
    }
  };
  function getCompatibilityComment(compatibilityResult) {
    if (compatibilityResult < 25) {
      return "Độ tương thích quá thấp không phù hợp với gia chủ";
    } else if (compatibilityResult >= 25 && compatibilityResult <= 50) {
      return "Độ tương thích không quá tốt, gia chủ nên cân nhắc";
    } else if (compatibilityResult > 50 && compatibilityResult <= 75) {
      return "Độ tương thích tương đối thích hợp với gia chủ";
    } else if (compatibilityResult > 75 && compatibilityResult <= 100) {
      return "Độ tương thích rất tốt với gia chủ, sẽ mang lại may mắn và tài lộc cho gia chủ";
    } else {
      return "Không rõ kết quả tương thích";
    }
  }

  // Hàm hiển thị modal khi người dùng chọn cá
  const showFishDetails = async (fish) => {
    try {
      let fishColors;
      if (allFishColorWeights[fish.koiType]) {
        fishColors = allFishColorWeights[fish.koiType];
      } else {
        const response = await api.get(`TypeColor/GetAllTypeColor`);
        const allColors = response.data;
        fishColors = allColors.filter(color => color.koiType === fish.koiType);
      }
      
      setSelectedFishDetail(fish);
      setColorWeights(fishColors.map(color => ({
        colorId: color.colorId,
        percentage: color.percentage || 0
      })));
      setIsModalVisible(true);
    } catch (error) {
      console.error("Error fetching fish details:", error);
      toast.error("Error fetching fish details");
    }
  };

  // Hàm xử lý thay đổi tỉ trọng màu
  const handleColorWeightChange = (index, value) => {
    const updatedWeights = [...colorWeights];
    updatedWeights[index] = {
      ...updatedWeights[index],
      percentage: Math.max(0, Math.min(1, value)) // Ensure value is between 0 and 1
    };
    setColorWeights(updatedWeights);
  };

  // Hàm kiểm tra tổng tỉ trọng phải là 100%
  const validateColorWeights = () => {
    const total = colorWeights.reduce(
      (sum, color) => sum + color.percentage,
      0
    );
    return Math.abs(total - 1) < 0.001;
  };

  const handleModalOk = () => {
    if (validateColorWeights()) {
      const selectedFishData = {
        koiType: selectedFishDetail.koiType,
        colors: colorWeights
      };
      form.setFieldsValue({ selectedFish: selectedFishData });
      setSelectedFish(selectedFishData);
      setAllFishColorWeights(prev => ({
        ...prev,
        [selectedFishDetail.koiType]: colorWeights
      }));
      setIsModalVisible(false);
    } else {
      toast.error("Total percentage must be 100%");
    }
  };
  
  const handleDeselectFish = () => {
    form.setFieldsValue({ selectedFish: null });
    setSelectedFish(null);
    setSelectedFishDetail(null);
    setIsModalVisible(false);
  };

  return (
    <>
      <HeaderTemplate />
      <div className="compute-container">
        <h1 className="page-title">Tính toán độ tương hợp</h1>
        <div className="compute-content">
          <div className="form-compute">
            <Form
              form={form}
              onFinish={onFinish}
              layout="vertical"
              className="compatibility-form"
            >
              <div className="birthdate-gender-row">
                <Form.Item
                  name="birthdate"
                  label="Ngày tháng năm sinh"
                  rules={[
                    { required: true, message: "Vui lòng chọn ngày sinh" },
                  ]}
                >
                  <DatePicker format="YYYY-MM-DD" style={{ width: "100%" }} />
                </Form.Item>
                <Form.Item
                  label="Giới tính"
                  name="Gender"
                  rules={[
                    { required: true, message: "Hãy chọn giới tính của bạn!" },
                  ]}
                >
                  <Radio.Group>
                    <Radio value="male">Nam</Radio>
                    <Radio value="female">Nữ</Radio>
                  </Radio.Group>
                </Form.Item>
              </div>
              <Form.Item
                label="Chọn loại cá"
                name="selectedFish"
                rules={[{ required: true, message: "Vui lòng chọn loại cá" }]}
              >
                <div className="filter-section">
                  <Form layout="inline">
                    <Form.Item label="Bảng mệnh">
                      <Select value={elementFilter} onChange={setElementFilter}>
                        <Option value="all">Tất cả</Option>
                        {elements.map((element) => (
                          <Option
                            key={element.elementId}
                            value={element.elementId}
                          >
                            {element.elementId}
                          </Option>
                        ))}
                      </Select>
                    </Form.Item>
                    <Form.Item label="Màu sắc">
                      <Select value={colorFilter} onChange={setColorFilter}>
                        <Option value="all">Tất cả</Option>
                        {colors.map((color) => (
                          <Option key={color.colorId} value={color.colorId}>
                            {color.colorId}
                          </Option>
                        ))}
                      </Select>
                    </Form.Item>

                    <Form.Item label="Tên giống cá">
                      <Input
                        placeholder="Tìm kiếm theo tên giống cá"
                        onChange={(e) => {
                          const searchTerm = e.target.value.toLowerCase();
                          const filtered = fishList.filter((fish) =>
                            fish.koiType.toLowerCase().includes(searchTerm)
                          );
                          setFilteredFishList(filtered);
                        }}
                      />
                    </Form.Item>
                  </Form>
                </div>

                
                <div className="fish-list">
                  {filteredFishList.map((fish) => (
                    <div 
                      key={fish.koiType} 
                      className={`fish-card ${selectedFish?.koiType === fish.koiType ? 'selected' : ''}`}
                    >
                      <img src={`/koi_image/${fish.image}`} alt={fish.image} />
                      <p>{fish.koiType}</p>
                      <div className="fish-info-box">
                        <img src={`/koi_image/${fish.image}`} alt={fish.image} />
                      </div>
                      <Button
                        type="primary"
                        onClick={() => showFishDetails(fish)}
                      >
                        {selectedFish?.koiType === fish.koiType ? 'Đã chọn' : 'Xem chi tiết'}
                      </Button>
                    </div>
                  ))}
                </div>
              </Form.Item>

              {/* Hiển thị modal trong phần JSX */}
              <Modal
                title={`Chi tiết cá ${selectedFishDetail?.koiType}`}
                visible={isModalVisible}
                onOk={handleModalOk}
                onCancel={() => setIsModalVisible(false)}
                footer={[
                  <div key="button-container" style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Button key="deselect" type="primary" danger onClick={handleDeselectFish}>
                      Bỏ chọn cá
                    </Button>
                    <Button key="submit" type="primary" onClick={handleModalOk}>
                      Chọn cá
                    </Button>
                  </div>
                ]}
              >
                {selectedFishDetail && (
                  <div>
                    <img
                      src={`/koi_image/${selectedFishDetail.image}`}
                      alt={selectedFishDetail.koiType}
                    />
                    <p>
                      <strong>Bản mệnh:</strong> {selectedFishDetail.element}
                    </p>
                    <p>
                      <strong>Giới thiệu:</strong>{" "}
                      {selectedFishDetail.description}
                    </p>

                    <div>
                      <h3>Điều chỉnh tỉ trọng màu</h3>
                      {colorWeights.map((color, index) => (
                        <div
                          key={color.colorId}
                          style={{ marginBottom: "16px" }}
                        >
                          <span>{color.colorId}: </span>
                          <Slider
                            min={0}
                            max={1}
                            step={0.01}
                            value={color.percentage}
                            onChange={(value) =>
                              handleColorWeightChange(index, value)
                            }
                          />
                          <InputNumber
                            min={0}
                            max={1}
                            step={0.01}
                            value={color.percentage}
                            onChange={(value) =>
                              handleColorWeightChange(index, value)
                            }
                          />
                        </div>
                      ))}
                      {!validateColorWeights() && (
        <p style={{ color: 'red' }}>Total percentage must be 100%</p>
      )}
                    </div>
                  </div>
                )}
              </Modal>
              {/* Add the pond shape selection here */}
              <Form.Item
                name="selectedPondShape"
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
                        if (selectedPondShape === shape.shapeId) {
                          setSelectedPondShape(null);
                          form.setFieldsValue({ selectedPondShape: null });
                        } else {
                          setSelectedPondShape(shape.shapeId);

                          form.setFieldsValue({
                            selectedPondShape: shape.shapeId,
                          });
                        }
                      }}
                    >
                      <img src={`/pond/${shape.image}`} alt={shape.shapeId} />
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
                <Select placeholder="Vui lòng chọn hướng đặt hồ">
                  <Option value="">Chọn hướng đặt hồ</Option>
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
                <h2 className="result-title">Kết quả tính toán</h2>

                <p className="result-score">
                  Độ tương hợp: {compatibilityResult}%
                </p>
                <p className="result-comment">
                  Nhận xét: {getCompatibilityComment(compatibilityResult)}
                </p>
              </div>
            )}
          </div>
        </div>
      </div>
      <FooterPage />
    </>
  );
}

export default ComputeCompability;