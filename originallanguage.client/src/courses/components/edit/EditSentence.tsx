import { Form, FormInstance, Input } from "antd";
import { IUpdateSentence } from "../../hooks/useSentences";
import ISentence from "../../models/ISentence";

interface IEditSentenceProps {
  sentence: ISentence;
  handleSentenceChanged: (sentence: IUpdateSentence) => void;
}

const EditSentence = ({
  sentence,
  handleSentenceChanged,
}: IEditSentenceProps) => {
  const [form] = Form.useForm<FormInstance>();

  const handleBlur = () => {
    const updateSentence: IUpdateSentence = {
      ...form.getFieldsValue(),
      lessonSampleId: sentence.lessonSampleId,
    };
    handleSentenceChanged(updateSentence);
  };

  return (
    <Form form={form} initialValues={sentence}>
      <Form.Item name="text" label="Text">
        <Input onBlur={handleBlur} />
      </Form.Item>
      <Form.Item name="translation" label="Translation">
        <Input onBlur={handleBlur} />
      </Form.Item>
      <Form.Item name="literalTranslation" label="Literal Translation">
        <Input onBlur={handleBlur} />
      </Form.Item>
      <Form.Item name="glosses" label="Glosses">
        <Input onBlur={handleBlur} />
      </Form.Item>
      <Form.Item name="transcription" label="Transcription">
        <Input onBlur={handleBlur} />
      </Form.Item>
    </Form>
  );
};

export default EditSentence;
