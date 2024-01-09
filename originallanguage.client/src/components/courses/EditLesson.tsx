import { Button, Card, Collapse, List } from "antd";
import ILesson from "../../models/ILesson";
import EditLessonSample from "./EditLessonSample";
import { useLessonSamples } from "../../hooks/useLessonSamples";
import { useParams } from "react-router-dom";
import { PlusOutlined } from "@ant-design/icons";

const { Panel } = Collapse;

const EditLesson = ({ lesson }: { lesson: ILesson }) => {
  const { samplesOfLesson } = useLessonSamples(lesson.id);

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
                  lessonSample={lessonSample}
                  title={"Sample " + (index + 1)}
                />
              </List.Item>
            )}
          />
          <Button type="primary">
            <PlusOutlined /> Add sample
          </Button>
        </Panel>
      </Collapse>
    </div>
  );
};

export default EditLesson;
