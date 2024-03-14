import { Button, Collapse, List } from "antd";
import ILesson from "../../models/ILesson";
import EditLessonSample from "./EditLessonSample";
import { PlusOutlined } from "@ant-design/icons";
import { useSamplesOfLesson } from "../../hooks/useSamplesOfLesson";

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

  return (
    <div>
      <Collapse>
        <Panel header={"Lesson " + lesson.number} key={1}>
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
        </Panel>
      </Collapse>
    </div>
  );
};

export default EditLesson;
