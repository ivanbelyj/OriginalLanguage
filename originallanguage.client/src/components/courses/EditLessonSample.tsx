import {
  Card,
  Form,
  Input,
  Button,
  InputNumber,
  List,
  Collapse,
  CollapseProps,
} from "antd";
import ILessonSample from "../../models/ILessonSample";
import EditSentence from "./EditSentence";
import { PlusOutlined } from "@ant-design/icons";
import { useLessonSampleSentences } from "../../hooks/useLessonSampleSentences";

const { Panel } = Collapse;

export interface IEditLessonSampleProps {
  lessonSample: ILessonSample;
  title: string;
  handleAddSentence: () => void;
}

const EditLessonSample = ({
  lessonSample,
  title,
  handleAddSentence,
}: IEditLessonSampleProps) => {
  const { lessonSampleSentences } = useLessonSampleSentences(
    lessonSample.id.toString()
  );

  const [form] = Form.useForm();

  const items: CollapseProps["items"] = lessonSampleSentences.map(
    (sentence, index) => ({
      key: sentence.id,
      label: sentence.text ?? `Variant ${index + 1}`,
      children: <EditSentence sentence={sentence} />,
    })
  );

  return (
    <Card title={title}>
      <Form form={form}>
        <Form.Item name="minimalProgressLevel" label="Minimal Progress Level">
          <InputNumber min={0} max={10} />
        </Form.Item>
        <Form.Item>
          <Collapse items={items} />
        </Form.Item>

        <Button type="primary" onClick={handleAddSentence}>
          <PlusOutlined /> Add sentence variant
        </Button>
      </Form>
    </Card>
  );
};

export default EditLessonSample;
