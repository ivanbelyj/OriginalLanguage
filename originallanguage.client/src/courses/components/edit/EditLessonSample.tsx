import {
  Card,
  Form,
  Button,
  InputNumber,
  Collapse,
  CollapseProps,
  Input,
  Divider,
  Typography,
} from "antd";
import EditSentence from "./EditSentence";
import { DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import { IUpdateLessonSample } from "../../hooks/useLessonSamples";
import ILessonSample from "../../models/ILessonSample";
import { IUpdateSentence, useSentences } from "../../hooks/useSentences";
import PopconfirmButton from "../../../common/components/PopconfirmButton";

const { Title } = Typography;

export interface IEditLessonSampleProps {
  lessonSample: ILessonSample;
  lessonSampleNumber: number;
  handleLessonSampleChanged: (updateLessonSample: IUpdateLessonSample) => void;
  onDelete: (id: string) => Promise<void>;
}

const EditLessonSample = ({
  lessonSample,
  lessonSampleNumber,
  handleLessonSampleChanged,
  onDelete,
}: IEditLessonSampleProps) => {
  const {
    lessonSampleSentences,
    postSentence,
    updateSentence,
    deleteSentence,
  } = useSentences(lessonSample.id);

  const [form] = Form.useForm();

  const onDeleteSentenceClick = (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    event.stopPropagation();
    console.log("on click delete variant");
  };

  const items: CollapseProps["items"] = lessonSampleSentences.map(
    (sentence, index) => ({
      key: sentence.id,
      label: (
        <div>
          {sentence.text ?? `Variant ${index + 1}`}
          {/* <Button
            type="text"
            size="small"
            style={{ marginLeft: "1em" }}
            icon={<DeleteOutlined />}
            onClick={(e) => {
              e.stopPropagation();
              deleteSentence(sentence.id);
            }}
          ></Button> */}
          <PopconfirmButton
            onConfirm={() => deleteSentence(sentence.id)}
            buttonProps={{
              type: "text",
              size: "small",
              style: { marginLeft: "1em" },
              icon: <DeleteOutlined />,
              onClick: onDeleteSentenceClick,
            }}
          >
            {""}
          </PopconfirmButton>
        </div>
      ),
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
    return (
      lessonSample.mainText || lessonSample.mainTranslation || defaultTitle
    );
  };

  const rootItems: CollapseProps["items"] = [
    {
      key: "1",
      label: getTitle(),
      children: (
        <>
          <Title level={5}>Edit sample</Title>
          <Form form={form} initialValues={lessonSample}>
            <Form.Item
              name="minimalProgressLevel"
              label="Minimal Progress Level"
            >
              <InputNumber min={0} max={10} onBlur={handleBlur} />
            </Form.Item>
            <Divider />

            <Form.Item name="mainText" label="Main Text">
              <Input onBlur={handleBlur} />
            </Form.Item>
            <Form.Item name="mainTranslation" label="Main Translation">
              <Input onBlur={handleBlur} />
            </Form.Item>
            <Form.Item name="textHints" label="Text Hints">
              <Input onBlur={handleBlur} />
            </Form.Item>
            <Form.Item name="translationHints" label="Translation Hints">
              <Input onBlur={handleBlur} />
            </Form.Item>
            <Form.Item name="glosses" label="Glosses">
              <Input onBlur={handleBlur} />
            </Form.Item>
            <Form.Item name="transcription" label="Transcription">
              <Input onBlur={handleBlur} />
            </Form.Item>
          </Form>
          <div style={{ marginBottom: "1.5em" }}>
            <Collapse items={items} />
          </div>

          <Button type="text" onClick={handleAddSentence}>
            <PlusOutlined /> Add sentence variant
          </Button>
          <PopconfirmButton
            onConfirm={() => onDelete(lessonSample.id)}
            buttonProps={{
              danger: true,
              type: "text",
              style: { marginLeft: "1em" },
              icon: <DeleteOutlined />,
            }}
          >
            Delete sample
          </PopconfirmButton>
        </>
      ),
    },
  ];

  return <Collapse items={rootItems} />;
};

export default EditLessonSample;
