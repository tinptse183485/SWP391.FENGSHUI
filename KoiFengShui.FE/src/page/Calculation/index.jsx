import "./index.css";

import { Button, DatePicker, Form, Radio } from "antd";
import AuthenTemplate from "../../components/authen-templates";
import { useNavigate } from "react-router-dom";
import api from "../../config/axios";
import { toast } from "react-toastify";
import { useState } from "react"; // Import useState

const Calculation = () => {
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const [koiData, setKoiData] = useState([]);
  const [koiQuantity, setKoiQuantity] = useState([]);
  const [pondShape, setPondShape] = useState([]);
  const [pondDirection, setPondDirection] = useState([]);

  const handleCalculate = async (values) => {
    try {
      // Gọi Api để lấy danh sách cá
      const response = await api.get("KoiVariety/GetListKoiByDOB", {
        params: { dob: values.YOB.format("YYYY-MM-DD") },
      });
      toast.success("Successfully fetched Koi list");
      console.log(response.data);

      // Gọi Api để lấy số lượng cá phù hợp
      const reponse2 = await api.get("KoiVariety/GetQuantityByDOB", {
        params: { dob: values.YOB.format("YYYY-MM-DD") },
      });
      toast.success("Successfully fetched quantity of Koi");
      console.log(reponse2.data);

      // Gọi Api để lấy hình dạng hồ phù hợp
      const reponse3 = await api.get("Pond/GetGoodShapeByDOB", {
        params: { dob: values.YOB.format("YYYY-MM-DD") },
      });
      toast.success("Successfully fetched shape of Pond");
      console.log(reponse3.data);

      // Gọi Api để lấy hướng hồ phù hợp
      const reponse4 = await api.get("Pond/GetGoodDirectionByDOB", {
        params: { dob: values.YOB.format("YYYY-MM-DD"), gender: values.Gender },
      });
      toast.success("Successfully fetched direction of Pond");
      console.log(reponse4.data);

      



      // Navigate to consulting page with Koi data,Koi quantity, pond shape, and direction
      navigate("/consulting", {
        state: {
          koiData: response.data,
          koiQuantity: reponse2.data,
          pondShape: reponse3.data,
          pondDirection: reponse4.data,
        },
      });
    } catch (error) {
      toast.error(error.response?.data || "Error fetching data"); // Xử lý lỗi
    }
  };

  return (
    <AuthenTemplate>
      <div className="Header-Title">
        <h1>Tư vấn cá và hồ theo tuổi của bạn</h1>
        <p>
          Hãy để chúng tôi giúp bạn cho loại cá và hỗ trợ theo năm sinh của bạn.
        </p>
      </div>
      <Form
        form={form}
        labelCol={{ span: 24 }}
        name="userForm"
        initialValues={{ remember: true }}
        onFinish={handleCalculate}
      >
        <Form.Item
          label="Ngày tháng năm sinh"
          name="YOB"
          rules={[{ required: true, message: "Hãy chọn ngày sinh của bạn!" }]}
        >
          <DatePicker format="YYYY-MM-DD" className="datePicker" />
        </Form.Item>

        <Form.Item
          label="Giới tính"
          name="Gender"
          rules={[{ required: true, message: "Hãy chọn giới tính của bạn!" }]}
        >
          <Form.Item name="Gender" noStyle>
            <Radio.Group>
              <Radio value="male">Nam</Radio>
              <Radio value="female">Nữ</Radio>
            </Radio.Group>
          </Form.Item>
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit">
            See now
          </Button>
        </Form.Item>
      </Form>
    </AuthenTemplate>

  );
};

export default Calculation;
