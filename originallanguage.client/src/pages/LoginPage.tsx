import React, { useState, FormEvent } from "react";
import { Form, Input, Button, Card, Checkbox } from "antd";
import { Link } from "react-router-dom";
import Center from "../components/common/Center";
import { LockOutlined, UserOutlined } from "@ant-design/icons";

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const handleFinish = (_: FormEvent) => {
    console.log(`Username: ${email}, Password: ${password}`);
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
