import {
  Button,
  Collapse,
  CollapseProps,
  Form,
  Input,
  List,
  Typography,
} from "antd";
import EditLessonSample from "./EditLessonSample";
import { DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import { DraggableProvidedDragHandleProps } from "react-beautiful-dnd";
import ILesson from "../../models/ILesson";
import {
  IUpdateLessonSample,
  useLessonSamples,
} from "../../hooks/useLessonSamples";
import PopconfirmButton from "../../../common/components/PopconfirmButton";
import { IUpdateLesson } from "../../hooks/useLessons";
import CourseUtils from "../../course-utils";

const { Title } = Typography;

export interface IEditLessonProps {
  lesson: ILesson;
  dragHandleProps: DraggableProvidedDragHandleProps | null | undefined;
  handleLessonChanged: (id: string, lesson: IUpdateLesson) => Promise<void>;
  onDelete: (id: string) => Promise<void>;
}

const EditLesson = ({
  lesson,
  dragHandleProps,
  handleLessonChanged,
  onDelete,
}: IEditLessonProps) => {
  const {
    samplesOfLesson,
    postLessonSample,
    updateLessonSample,
    deleteLessonSample,
  } = useLessonSamples(lesson.id);

  const [form] = Form.useForm();

  const onAddLessonSampleClicked = async () => {
    await postLessonSample({
      lessonId: lesson.id,
      minimalProgressLevel: 0,
    });
  };

  const handleBlur = () => {
    const updateLesson: IUpdateLesson = {
      ...lesson,
      ...form.getFieldsValue(),
    };
    handleLessonChanged(lesson.id, updateLesson);
  };

  const items: CollapseProps["items"] = [
    {
      key: "1",
      label: (
        <div {...dragHandleProps}>{CourseUtils.getLessonTitle(lesson)}</div>
      ),
      children: (
        <>
          <Title level={4}>
            {"Edit lesson" + (lesson.title ? ` "${lesson.title}"` : "")}
          </Title>
          <Form form={form} initialValues={lesson}>
            <Form.Item name="title" label="Lesson Title">
              <Input onBlur={handleBlur} />
            </Form.Item>
            <Form.Item name="description" label="Description">
              <Input.TextArea onBlur={handleBlur} />
            </Form.Item>
          </Form>
          <List
            itemLayout="vertical"
            dataSource={samplesOfLesson}
            renderItem={(lessonSample, index) => (
              <List.Item>
                <EditLessonSample
                  lessonSample={lessonSample}
                  lessonSampleNumber={index + 1}
                  handleLessonSampleChanged={(
                    lessonSampleChanged: IUpdateLessonSample
                  ) => {
                    updateLessonSample(lessonSample.id, lessonSampleChanged);
                  }}
                  onDelete={(id: string) => deleteLessonSample(id)}
                />
              </List.Item>
            )}
          />
          <Button type="default" onClick={onAddLessonSampleClicked}>
            <PlusOutlined /> Add sample
          </Button>
          <PopconfirmButton
            onConfirm={() => onDelete(lesson.id)}
            buttonProps={{
              danger: true,
              type: "text",
              style: { marginLeft: "1em" },
              icon: <DeleteOutlined />,
            }}
          >
            Delete lesson
          </PopconfirmButton>
        </>
      ),
    },
  ];

  return (
    <div>
      <Collapse items={items}></Collapse>
    </div>
  );
};

export default EditLesson;
