import { Card, Form, Button, InputNumber, Collapse, CollapseProps } from "antd";
import ILessonSample from "../../models/ILessonSample";
import EditSentence from "./EditSentence";
import { PlusOutlined } from "@ant-design/icons";
import { IUpdateSentence, useSentences } from "../../hooks/useSentences";
import "../../styles/common.css";
import { IUpdateLessonSample } from "../../hooks/useLessonSamples";

export interface IEditLessonSampleProps {
  lessonSample: ILessonSample;
  lessonSampleNumber: number;
  handleLessonSampleChanged: (updateLessonSample: IUpdateLessonSample) => void;
}

const EditLessonSample = ({
  lessonSample,
  lessonSampleNumber,
  handleLessonSampleChanged,
}: IEditLessonSampleProps) => {
  const { lessonSampleSentences, postSentence, updateSentence } = useSentences(
    lessonSample.id
  );

  const [form] = Form.useForm();

  const items: CollapseProps["items"] = lessonSampleSentences.map(
    (sentence, index) => ({
      key: sentence.id,
      label: sentence.text ?? `Variant ${index + 1}`,
      children: (
        <EditSentence
          sentence={sentence}
          handleSentenceChanged={(sentenceChanged: IUpdateSentence) =>
            updateSentence(sentence.id, sentenceChanged)
          }
        />
      ),
    })
  );

  const handleBlur = () => {
    const updateLessonSample: IUpdateLessonSample = form.getFieldsValue();
    updateLessonSample.lessonId = lessonSample.lessonId;
    handleLessonSampleChanged(updateLessonSample);
  };

  const handleAddSentence = async () => {
    await postSentence({
      lessonSampleId: lessonSample.id,
    });
  };

  const getTitle = () => {
    const defaultTitle = `Lesson sample ${lessonSampleNumber}`;
    // Todo: lesson sample title based on main sentence variant
    const firstSentenceText =
      lessonSampleSentences.length > 0 ? lessonSampleSentences[0].text : null;
    return firstSentenceText ?? defaultTitle;
  };

  return (
    <Card title={getTitle()}>
      <Form form={form} initialValues={lessonSample}>
        <Form.Item name="minimalProgressLevel" label="Minimal Progress Level">
          <InputNumber min={0} max={10} onBlur={handleBlur} />
        </Form.Item>
      </Form>
      <div className="item">
        <Collapse accordion items={items} />
      </div>

      <Button type="primary" onClick={handleAddSentence}>
        <PlusOutlined /> Add sentence variant
      </Button>
    </Card>
  );
};

export default EditLessonSample;
