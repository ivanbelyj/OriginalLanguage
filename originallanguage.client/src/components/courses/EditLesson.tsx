import { Button, Card, Collapse, List } from "antd";
import ILesson from "../../models/ILesson";
import EditLessonSample from "./EditLessonSample";
import { useLessonSamples } from "../../hooks/useLessonSamples";
import { useParams } from "react-router-dom";
import { PlusOutlined } from "@ant-design/icons";
import ILessonSample from "../../models/ILessonSample";

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
  const { samplesOfLesson } = useLessonSamples(lesson.id);

  const onAddLessonSampleClicked = () => {
    handleAddLessonSample();
  };

  return (
    <div>
      {/* <Collapse defaultActiveKey={["1"]}>
        <Panel header={"Lesson " + lesson.number} key={"1"}>
          <Collapse
            defaultActiveKey={samplesOfLesson.map((sample, index) =>
              index.toString()
            )}
          >
            {samplesOfLesson.map((lessonSample, index) => (
              <Panel header={"Sample " + (index + 1)} key={index.toString()}>
                <EditLessonSample lessonSample={lessonSample} />
              </Panel>
            ))}
          </Collapse>
        </Panel>
      </Collapse> */}

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
