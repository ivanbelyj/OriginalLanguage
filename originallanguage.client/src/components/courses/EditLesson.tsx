import { Button, Collapse, CollapseProps, List } from "antd";
import ILesson from "../../models/ILesson";
import EditLessonSample from "./EditLessonSample";
import { PlusOutlined } from "@ant-design/icons";
import {
  IUpdateLessonSample,
  useLessonSamples,
} from "../../hooks/useLessonSamples";

export interface IEditLessonProps {
  lesson: ILesson;
}

const EditLesson = ({ lesson }: IEditLessonProps) => {
  const { samplesOfLesson, postLessonSample, updateLessonSample } =
    useLessonSamples(lesson.id);

  const onAddLessonSampleClicked = async () => {
    await postLessonSample({
      lessonId: lesson.id,
      minimalProgressLevel: 0,
    });
  };

  const items: CollapseProps["items"] = [
    {
      key: "1",
      label: "Lesson " + lesson.number,
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
                />
              </List.Item>
            )}
          />
          <Button type="primary" onClick={onAddLessonSampleClicked}>
            <PlusOutlined /> Add sample
          </Button>
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
