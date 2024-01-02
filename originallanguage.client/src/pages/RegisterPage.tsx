import React, { useState, FormEvent } from "react";
import { Form, Input, Button, Card, Select } from "antd";
import { Link } from "react-router-dom";
import Center from "../components/common/Center";
import axios from "axios";
import IUser from "../models/IUser";
const { Option } = Select;

const RegisterPage: React.FC = () => {
  const [name, setName] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [gender, setGender] = useState<string>("not-specified");

  const handleFinish = async (_: FormEvent) => {
    if (password !== confirmPassword) {
      alert("Passwords do not match"); // Todo: validation
      return;
    }

    console.log(import.meta.env.VITE_API_URL + "accounts");

    const response = await axios.post<IUser>(
      import.meta.env.VITE_API_URL + "accounts",
      {
        name,
        email,
        password,
      }
    );
    console.log("response: ");

    console.log(response);
  };

  return (
    <Center>
      <Card title="Register" style={{ width: "300px", margin: "0 auto" }}>
        <Form onFinish={handleFinish} initialValues={{ gender }}>
          <Form.Item
            label="Username"
            name="username"
            rules={[
              {
                required: true,
                message: "Please input your username!",
              },
            ]}
          >
            <Input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
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
            <Input value={email} onChange={(e) => setEmail(e.target.value)} />
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
            <Input.Password
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </Form.Item>
          <Form.Item
            label="Confirm Password"
            name="confirm-password"
            rules={[
              {
                required: true,
                message: "Please confirm your password!",
              },
            ]}
          >
            <Input.Password
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </Form.Item>

          <Form.Item name="gender" label="Gender">
            <Select
              placeholder="Select your gender"
              onChange={(value) => setGender(value)}
            >
              <Option value="not-specified">Not specified</Option>
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
