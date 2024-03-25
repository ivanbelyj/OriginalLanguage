import { Button, Collapse, CollapseProps, List } from "antd";
import EditLessonSample from "./EditLessonSample";
import { DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import { DraggableProvidedDragHandleProps } from "react-beautiful-dnd";
import ILesson from "../../models/ILesson";
import {
  IUpdateLessonSample,
  useLessonSamples,
} from "../../hooks/useLessonSamples";
import PopconfirmButton from "../../../common/components/PopconfirmButton";

export interface IEditLessonProps {
  lesson: ILesson;
  dragHandleProps: DraggableProvidedDragHandleProps | null | undefined;
  onDelete: (id: string) => Promise<void>;
}

const EditLesson = ({
  lesson,
  dragHandleProps,
  onDelete,
}: IEditLessonProps) => {
  const {
    samplesOfLesson,
    postLessonSample,
    updateLessonSample,
    deleteLessonSample,
  } = useLessonSamples(lesson.id);

  const onAddLessonSampleClicked = async () => {
    await postLessonSample({
      lessonId: lesson.id,
      minimalProgressLevel: 0,
    });
  };

  const items: CollapseProps["items"] = [
    {
      key: "1",
      label: <div {...dragHandleProps}>Lesson {lesson.number}</div>,
      children: (
        <>
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
          <Button type="primary" onClick={onAddLessonSampleClicked}>
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
