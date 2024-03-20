import { Button, Collapse, CollapseProps, List } from "antd";
import ILesson from "../../models/ILesson";
import EditLessonSample from "./EditLessonSample";
import { PlusOutlined } from "@ant-design/icons";
import { useSamplesOfLesson } from "../../hooks/useSamplesOfLesson";
import { useEffect } from "react";

const { Panel } = Collapse;

export interface IEditLessonProps {
  lesson: ILesson;
  handleAddLessonSample: () => void;
  handleAddSentence: (lessonSampleId: string) => void;
}

const EditLesson = ({
  lesson,
  handleAddLessonSample,
  handleAddSentence,
}: IEditLessonProps) => {
  const { samplesOfLesson } = useSamplesOfLesson(lesson.id);

  const onAddLessonSampleClicked = () => {
    handleAddLessonSample();
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
                  handleAddSentence={() => handleAddSentence(lessonSample.id)}
                  lessonSample={lessonSample}
                  title={"Sample " + (index + 1)}
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
