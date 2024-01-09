import { Card, Form, Input, Button, InputNumber, List, Collapse } from "antd";
import ILessonSample from "../../models/ILessonSample";
import EditSentence from "./EditSentence";
import { useSentences } from "../../hooks/useSentences";
import { PlusOutlined } from "@ant-design/icons";

const { Panel } = Collapse;

const EditLessonSample = ({
  lessonSample,
  title,
}: {
  lessonSample: ILessonSample;
  title: string;
}) => {
  const { lessonSampleSentences } = useSentences(lessonSample.id.toString());

  const [form] = Form.useForm();

  return (
    <Card title={title}>
      <Form form={form}>
        <Form.Item name="minimalProgressLevel" label="Minimal Progress Level">
          <InputNumber min={0} max={10} />
        </Form.Item>
        <Form.Item>
          <Collapse>
            {lessonSampleSentences.map((sentence, index) => (
              <Panel key={index} header={sentence.text}>
                <EditSentence sentence={sentence} />
              </Panel>
            ))}
          </Collapse>
        </Form.Item>
        {/* <List
          itemLayout="vertical"
          dataSource={lessonSampleSentences}
          renderItem={(sentence) => (
            <List.Item>
              <EditSentence sentence={sentence} />
            </List.Item>
          )}
        /> */}

        <Button type="primary">
          <PlusOutlined /> Add sentence variant
        </Button>
      </Form>
    </Card>
  );
};

export default EditLessonSample;
