import { useEffect } from "react";
import { Form, Input, Checkbox, Button, Typography, Tooltip } from "antd";
import { useLanguages } from "../../hooks/languages";
import { useParams } from "react-router-dom";
import React from "react";

const { Title, Text } = Typography;

export function EditLanguage() {
  // const [name, setName] = useState("");
  // const [nativeName, setNativeName] = useState("");
  // const [isConlang, setIsConlang] = useState(true);
  const { updateLanguage, getLanguage } = useLanguages();

  const { id: languageId } = useParams();

  const [form] = Form.useForm();

  const handleFinish = async (_: React.FormEvent) => {
    if (!languageId) return;

    const lang = await updateLanguage(languageId, {
      ...form.getFieldsValue(),
      authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
    });
    console.log("language updated", lang);
  };

  useEffect(() => {
    if (languageId) {
      getLanguage(languageId).then((language) => {
        console.log("language (use effect)", language);

        form.setFieldsValue({
          name: language.name,
          nativeName: language.nativeName,
          isConlang: language.isConlang,
        });
      });
    }
  }, [languageId]);

  return (
    <Form form={form} onFinish={handleFinish} style={{ maxWidth: "300px" }}>
      <Title level={3}>{form.getFieldValue("name")}</Title>

      <Form.Item
        label="Language name"
        name="name"
        rules={[{ required: true, message: "Please enter the language name" }]}
      >
        <Input type="text" />
      </Form.Item>
      <Form.Item
        label="Language native name"
        name="nativeName"
        rules={[
          { required: true, message: "Please enter the language native name" },
        ]}
      >
        <Input type="text" />
      </Form.Item>
      <Form.Item label="Is conlang" name="isConlang" valuePropName="checked">
        <Checkbox />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          Save
        </Button>
      </Form.Item>
      <Form.Item>
        <Title level={5}>Native speakers</Title>
        <Checkbox.Group>
          <Checkbox value="A">Not humanoid</Checkbox>
          <Tooltip title="Check if there are any sounds that are not naturally produced by humans">
            <Checkbox value="B">Inhuman sounds</Checkbox>
          </Tooltip>
          <Tooltip title="Check if the spoken language has a completely alien articulation compared to humans">
            <Checkbox value="C">Alien articulation</Checkbox>
          </Tooltip>

          <Tooltip
            title={
              "Check if native speakers combine anthropomorphic and some " +
              "animal traits (or they can be considered as furry, if you want)"
            }
          >
            <Checkbox value="D">Furry</Checkbox>
          </Tooltip>
          <Checkbox value="E">D</Checkbox>
          <Checkbox value="F">E</Checkbox>
        </Checkbox.Group>
      </Form.Item>
    </Form>
  );
}
