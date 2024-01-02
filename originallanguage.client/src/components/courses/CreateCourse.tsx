import { useState } from "react";
import axios from "axios";
import ICourse from "../../models/ICourse";
import { Form, Input, Button } from "antd";

interface ICreateCourseProps {
  onCreate: (newCourse: ICourse) => void;
}

export function CreateCourse({ onCreate }: ICreateCourseProps) {
  const [title, setTitle] = useState("");

  const handleTitleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(event.target.value);
  };
  const handleFinish = async (_: React.FormEvent) => {
    const response = await axios.post<ICourse>(
      import.meta.env.VITE_API_URL + "courses",
      {
        authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
        title,
      }
    );

    console.log("Course created: ", response);
    // setTitle("");

    onCreate(response.data);
  };

  return (
    <Form onFinish={handleFinish} style={{ maxWidth: "300px" }}>
      <Form.Item
        label="Course title"
        name="title"
        rules={[{ required: true, message: "Please input the course title!" }]}
      >
        <Input
          type="text"
          value={title}
          onChange={handleTitleChange}
          placeholder="Course title"
        />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          Create
        </Button>
      </Form.Item>
    </Form>
  );
}
