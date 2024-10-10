import { Button, DatePicker, Form, Input } from "antd";
import AuthenTemplate from "../../components/authen-templates";
import { Link, useNavigate } from "react-router-dom";
import api from "../../config/axios";
import { toast } from "react-toastify";
import "./index.css";
function Register() {
  const navigate = useNavigate();
  const [form] = Form.useForm();

  const handleRegister = async (values) => {
    //lấy cái values và submit nó xún backend
    try {
      values.role = "Member";
      values.status = "Active";
      const response = await api.post("Account/register", values);
      toast.success("Successfully register new account!");
      navigate("/login");
    } catch (error) {
      toast.error(error.response.data); //nơi mà backend sẽ trả về lỗi khi mà register fail
    }
  };
  // Custom validator for confirming password
  const validatePasswordConfirmation = ({ getFieldValue }) => ({
    validator(_, value) {
      if (!value || getFieldValue("Password") === value) {
        return Promise.resolve();
      }
      return Promise.reject(new Error("The two passwords do not match!"));
    },
  });
  return (
    <AuthenTemplate>
      <div className="login-header">
        <h1>Welcome to Koi Hên Xui !</h1>
        <p>Hãy nhập thông tin để tạo tài khoản</p>
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
          label="UserID"
          name="UserID"
          rules={[
            { required: true, message: "Xin hãy nhập User ID" },
            { min: 4, message: "User ID must be at least 4 characters!" },
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Password"
          name="Password"
          rules={[
            { required: true, message: "Xin hãy nhập password!" },
            { min: 6, message: "Password must be at least 6 characters!" },
          ]}
          hasFeedback
        >
          <Input.Password />
        </Form.Item>

        <Form.Item
          label="Re-enter Password"
          name="confirmPassword"
          dependencies={["Password"]} // Ensure the password field updates the validation for this field
          hasFeedback
          rules={[
            { required: true, message: "Please confirm your password!" },
            validatePasswordConfirmation(form),
          ]}
        >
          <Input.Password />
        </Form.Item>

        <Form.Item
          label="Họ và tên"
          name="Name"
          rules={[{ required: true, message: "Xin hãy nhập họ và tên!" }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Email"
          name="Email"
          rules={[
            { required: true, message: "Please input your email!" },
            { type: "email", message: "Please input a valid email!" },
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Birthday"
          name="Birthday"
          rules={[{ required: true, message: "Please select your birthday!" }]}
        >
          <DatePicker format="YYYY-MM-DD" className="datePicker" />
        </Form.Item>
        {/* <Form.Item
          label="Birthday"
          name="Birthday"
          rules={[
            { required: true, message: "Please input your birthday!" },
            {
              pattern: /^\d{4}-\d{2}-\d{2}$/,
              message: "Birthday must be in the format YYYY-MM-DD!",
            },
          ]}
        >
          <Input />
        </Form.Item> */}

        <Form.Item>
          <Button type="primary" htmlType="submit">
            Register
          </Button>
        </Form.Item>
        <Link to="/login">Already have account ? Login now</Link>
      </Form>
    </AuthenTemplate>
  );
}

export default Register;
