import { useEffect, useState } from "react";
import { Form, DatePicker, Select, Button, Radio,Input  } from "antd";
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

  // useEffect(() => {
  //   if (colorFilter === "all") {
  //     setFilteredFishList(fishList); // Nếu không chọn màu, hiển thị toàn bộ danh sách cá
  //   } else {
  //     fetchFishByColor();
  //   }
  // }, [colorFilter, fishList]);

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

  // const fetchFishByColor = async () => {
  //   try {
  //     const response = await api.get(
  //       `KoiVariety/GetListKoiByColor?color=${colorFilter}`
  //     );
  //     setFilteredFishList(response.data);
  //   } catch (error) {
  //     toast.error("Error fetching fish by color");
  //   }
  // };

  const onFinish = async (values) => {
    try {
      console.log(values);
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
                  rules={[{ required: true, message: "Vui lòng chọn ngày sinh" }]}
                >
                  <DatePicker format="YYYY-MM-DD" style={{ width: "100%" }} />
                </Form.Item>
                <Form.Item
                  label="Gender"
                  name="Gender"
                  rules={[{ required: true, message: "Please select your gender!" }]}
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
                        <Option key={element.elementId} value={element.elementId}>
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
                        const filtered = fishList.filter(fish =>
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
                      className={`fish-card ${selectedFish === fish.koiType ? "selected" : ""}`}
                      onClick={() => {
                        if (selectedFish === fish.koiType) {
                          setSelectedFish(null);
                          form.setFieldsValue({ selectedFish: null });
                        } else {
                          setSelectedFish(fish.koiType);
                          form.setFieldsValue({ selectedFish: fish.koiType });
                        }
                      }}
                    >
                      <img src={`/koi_image/${fish.image}`} alt={fish.image} />
                      <p>{fish.koiType}</p>
                      <div className="fish-info-box">
                        <img src={`/koi_image/${fish.image}`} alt={fish.image} />
                      </div>
                    </div>
                  ))}
                </div>
              </Form.Item>
              {/* Add the pond shape selection here */}
              <Form.Item
                name="selectedPondShape"
                label="Hình dáng hồ"
                rules={[{ required: true, message: "Vui lòng chọn hình dáng hồ" }]}
              >
                <div className="pond-shape-list">
                  {pondShapes.map((shape) => (
                    <div
                      key={shape.shapeId}
                      className={`pond-shape-card ${selectedPondShape === shape.shapeId ? "selected" : ""}`}
                      onClick={() => {
                        if (selectedPondShape === shape.shapeId) {
                          setSelectedPondShape(null);
                          form.setFieldsValue({ selectedPondShape: null });
                        } else {
                          setSelectedPondShape(shape.shapeId);
                          form.setFieldsValue({ selectedPondShape: shape.shapeId });
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
                rules={[{ required: true, message: "Vui lòng chọn hướng đặt hồ" }]}
              >
                <Select placeholder="Vui lòng chọn hướng đặt hồ">
                  <Option value="">Chọn hướng đặt hồ</Option>
                  {pondDirections.map((direction) => (
                    <Option key={direction.directionId} value={direction.directionId}>
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
                <p className="result-score">Độ tương hợp: {compatibilityResult.compatibilityScore}</p>
                <p className="result-comment">Nhận xét: {compatibilityResult.comment}</p>
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