import React, { useState, FormEvent } from "react";
import { Form, Input, Button, Card, Checkbox } from "antd";
import { Link } from "react-router-dom";
import Center from "../components/common/Center";
import { LockOutlined, UserOutlined } from "@ant-design/icons";
import axios from "axios";
import { useAuth } from "../auth/AuthProvider";

const LoginPage: React.FC = () => {
  const { setToken } = useAuth();
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const handleFinish = async (_: FormEvent) => {
    console.log(`Username: ${email}, Password: ${password}`);
    try {
      const response = await axios.post(
        import.meta.env.VITE_IDENTITY_URL + "connect/token",
        {
          client_id: "frontend",
          client_secret: "secret",
          grant_type: "password",
          username: email,
          password: password,
          // scope: "openid profile",
        },
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );
      console.log(response.data);
      setToken(response.data.access_token);
    } catch (error) {
      console.error("Failed to authenticate", error);
    }
  };

  return (
    <Center>
      <Card title="Login" style={{ width: "300px" }}>
        <Form onFinish={handleFinish}>
          <Form.Item
            label="Email"
            name="email"
            rules={[{ required: true, message: "Please enter your Username" }]}
          >
            <Input
              prefix={<UserOutlined className="site-form-item-icon" />}
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Email"
            />
          </Form.Item>
          <Form.Item
            label="Password"
            name="password"
            rules={[{ required: true, message: "Please enter your password" }]}
          >
            <Input.Password
              prefix={<LockOutlined className="site-form-item-icon" />}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Password"
            />
          </Form.Item>
          <Form.Item>
            <Form.Item name="remember" valuePropName="checked" noStyle>
              <Checkbox>Remember me</Checkbox>
            </Form.Item>

            <Link to="">Forgot password</Link>
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" style={{ width: "100%" }}>
              Log in
            </Button>
          </Form.Item>

          <Link to="/register">Sign up</Link>
        </Form>
      </Card>
    </Center>
  );
};

export default LoginPage;
