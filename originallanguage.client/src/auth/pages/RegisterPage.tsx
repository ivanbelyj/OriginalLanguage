import React from "react";
import { Form, Input, Button, Card, Select } from "antd";
import { Link } from "react-router-dom";
import Center from "../../common/components/Center";
import axios from "axios";
import IUser from "../../user/models/IUser";
const { Option } = Select;

const RegisterPage: React.FC = () => {
  // const [name, setName] = useState<string>("");
  // const [email, setEmail] = useState<string>("");
  // const [password, setPassword] = useState<string>("");
  // const [confirmPassword, setConfirmPassword] = useState<string>("");
  // const [gender, setGender] = useState<string>("not-specified");
  const [form] = Form.useForm();

  const handleFinish = async (values: any) => {
    console.log("Handle register. values", values);
    if (values.password !== values.confirmPassword) {
      alert("Passwords do not match"); // Todo: validation
      return;
    }

    const response = await axios.post<IUser>(
      import.meta.env.VITE_API_URL + "accounts",
      {
        ...form.getFieldsValue(),
        // name,
        // email,
        // password,
      }
    );
    console.log("response: ");

    console.log(response);
  };

  return (
    <Center>
      <Card title="Register" style={{ width: "300px", margin: "0 auto" }}>
        <Form
          form={form}
          onFinish={handleFinish}
          initialValues={{ gender: "notSpecified" }}
        >
          <Form.Item
            label="Username"
            name="name"
            rules={[
              {
                required: true,
                message: "Please input your username!",
              },
            ]}
          >
            <Input type="text" />
          </Form.Item>
          <Form.Item
            label="Email"
            name="email"
            rules={[
              {
                type: "email",
                message: "The input is not valid E-mail!",
              },
              {
                required: true,
                message: "Please enter your E-mail!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Password"
            name="password"
            rules={[
              {
                required: true,
                message: "Please input your password!",
              },
            ]}
          >
            <Input.Password />
          </Form.Item>
          <Form.Item
            label="Confirm Password"
            name="confirmPassword"
            rules={[
              {
                required: true,
                message: "Please confirm your password!",
              },
            ]}
          >
            <Input.Password />
          </Form.Item>

          <Form.Item name="gender" label="Gender">
            <Select>
              <Option value="notSpecified">Not specified</Option>
              <Option value="male">Male</Option>
              <Option value="female">Female</Option>
            </Select>
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" style={{ width: "100%" }}>
              Register
            </Button>
          </Form.Item>

          <Link to="/login">Sign In</Link>
        </Form>
      </Card>
    </Center>
  );
};

export default RegisterPage;
