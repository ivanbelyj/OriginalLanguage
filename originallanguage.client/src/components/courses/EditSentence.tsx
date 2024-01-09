import { Button, Card, Collapse, Form, Input } from "antd";
import ISentence from "../../models/ISentence";

const { Panel } = Collapse;

const EditSentence = ({ sentence }: { sentence: ISentence }) => {
  const [form] = Form.useForm();

  return (
    // <Collapse defaultActiveKey={["1"]}>
    //   <Panel header={sentence.text} key="1">
    <>
      <Form.Item name="text" label="Text">
        <Input />
      </Form.Item>
      <Form.Item name="translation" label="Translation">
        <Input />
      </Form.Item>
      <Form.Item name="literalTranslation" label="Literal Translation">
        <Input />
      </Form.Item>
      <Form.Item name="glosses" label="Glosses">
        <Input />
      </Form.Item>
      <Form.Item name="transcription" label="Transcription">
        <Input />
      </Form.Item>
    </>
    //   </Panel>
    // </Collapse>
  );
};

export default EditSentence;
