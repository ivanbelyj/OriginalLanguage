import { Button, Form, Input } from "antd";
import Title from "antd/es/typography/Title";
import { IUpdateArticle } from "../../hooks/useArticles";

import Loading from "../../../common/components/Loading";
import React, { useEffect } from "react";
import { IArticle, IArticleInfo } from "../../models/models";

interface IEditArticleInfoProps {
  initialArticle?: IArticle;
  isLoading: boolean;
  onArticleChanged: (articleInfo: IArticleInfo) => void;
  onSave: () => void;
}
export const EditArticleInfo: React.FC<IEditArticleInfoProps> = ({
  initialArticle,
  isLoading,
  onArticleChanged,
  onSave,
}) => {
  const [form] = Form.useForm();

  useEffect(() => {
    if (initialArticle) form.setFieldsValue(initialArticle);
  }, [initialArticle]);

  const handleBlur = async () => {
    handleArticleChanged();
  };

  const handleArticleChanged = async () => {
    const article: Partial<IUpdateArticle> = {
      ...form.getFieldsValue(),
    };
    await onArticleChanged(article);
  };

  return (
    <div>
      <Title level={3}>Edit article</Title>
      {isLoading ? (
        <Loading />
      ) : (
        <Form form={form}>
          <Form.Item
            name="title"
            label="Title"
            rules={[
              { required: true, message: "Please input the article title!" },
            ]}
          >
            <Input
              type="text"
              placeholder="Article title"
              onBlur={handleBlur}
            />
          </Form.Item>
          <Form.Item
            name="shortDescription"
            label="Short description"
            rules={[
              {
                max: 255,
                message: "Description must be less than 255 characters!",
              },
            ]}
          >
            <Input.TextArea
              placeholder="Short description"
              onBlur={handleBlur}
            />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" onClick={onSave}>
              Save
            </Button>
          </Form.Item>
        </Form>
      )}
    </div>
  );
};
