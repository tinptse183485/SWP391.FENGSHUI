import "./index.css";
import { Button, DatePicker, Form, Radio } from "antd";
import AuthenTemplate from "../../components/authen-templates";
import { Link, useNavigate } from "react-router-dom";
import api from "../../config/axios";
import { toast } from "react-toastify";

const Calculation = () => {
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const handleRegister = async (values) => {
    //lấy cái values và submit nó xún backend
    try {
      const response = await api.post("Account/register", values);
      toast.success("Successfully register new account!");
      navigate("/login");
    } catch (error) {
      toast.error(error.response.data); //nơi mà backend sẽ trả về lỗi khi mà register fail
    }
  };

  return (
    <AuthenTemplate>
      <div className="Header-Title">
        <h1>Feng Shui consulting according to your Age</h1>
        <p>
          Hãy để chúng tôi giúp bạn cho loại cá và hỗ trợ theo năm sinh của bạn.
        </p>
      </div>
      <Form
        form={form}
        labelCol={{
          span: 24,
        }}
        name="userForm"
        initialValues={{ remember: true }}
        onFinish={handleRegister}
      >
        <Form.Item
          label="Birthday"
          name="Birthday"
          rules={[{ required: true, message: "Please select your birthday!" }]}
        >
          <DatePicker format="YYYY-MM-DD" className="datePicker" />
        </Form.Item>

        <Form.Item
          label="Gender"
          name="Gender"
          rules={[{ required: true, message: "Please select your gender!" }]}
        >
          <Form.Item name="Gender" noStyle>
            <Radio.Group>
              <Radio value="male">Male</Radio>
              <Radio value="female">Female</Radio>
            </Radio.Group>
          </Form.Item>
        </Form.Item>
      </Form>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          See now
        </Button>
      </Form.Item>
    </AuthenTemplate>
  );
};

export default Calculation;
