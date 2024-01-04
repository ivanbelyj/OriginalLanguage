import { useEffect } from "react";
import { Form, Input, Checkbox, Button, Typography } from "antd";
import { useLanguages } from "../../hooks/languages";
import { useParams } from "react-router-dom";

const { Title, Text } = Typography;

export function EditLanguage() {
  // const [name, setName] = useState("");
  // const [nativeName, setNativeName] = useState("");
  // const [isConlang, setIsConlang] = useState(true);
  const { updateLanguage, getLanguage } = useLanguages();

  const { id: languageId } = useParams();

  const [form] = Form.useForm();

  // const initialLanguage = languageId ? await getLanguage(languageId) : null;

  // const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
  //   setName(event.target.value);
  // };
  // const handleNativeNameChange = (
  //   event: React.ChangeEvent<HTMLInputElement>
  // ) => {
  //   setNativeName(event.target.value);
  // };
  // const handleIsConlangChange = (event: CheckboxChangeEvent) => {
  //   setIsConlang(event.target.checked);
  // };

  const handleFinish = async (_: React.FormEvent) => {
    if (!languageId) return;

    const lang = await updateLanguage(
      languageId,
      {
        ...form.getFieldsValue(),
        authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
      }
      //   {
      //   authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
      //   name,
      //   nativeName,
      //   isConlang,
      // }
    );
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
    </Form>
  );
}
