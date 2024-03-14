import { Button } from "antd";
import {
  DragDropContext,
  Droppable,
  Draggable,
  DropResult,
} from "react-beautiful-dnd";
import { PlusOutlined } from "@ant-design/icons";
import ILesson from "../../models/ILesson";
import EditLesson from "./EditLesson";
import ILessonSample from "../../models/ILessonSample";
import ISentence from "../../models/ISentence";

export interface EditLessonsProps {
  lessons: ILesson[];
  getLessonSampleById: (id: string) => ILessonSample;
  getSentenceById: (id: string) => ISentence;

  setLessons: (lessons: ILesson[]) => void;
  setLessonSampleById: (id: string, lessonSample: ILessonSample) => void;
  setSentenceById: (id: string, sentence: ISentence) => void;

  handleAddLesson: () => void;
  handleAddLessonSample: (lessonId: string) => void;
  handleAddSentence: (lessonSampleId: string) => void;
}

const EditCourseLessons: React.FC<EditLessonsProps> = ({
  lessons,
  getLessonSampleById,
  getSentenceById,
  setLessons,
  setLessonSampleById,
  setSentenceById,
  handleAddLesson,
  handleAddLessonSample,
  handleAddSentence,
}: EditLessonsProps) => {
  const sortedLessons = lessons.sort((a, b) => a.number - b.number);

  const handleDragEnd = (result: DropResult) => {
    if (!result.destination) return;

    const items = Array.from(sortedLessons);
    const [reorderedItem] = items.splice(result.source.index, 1);
    items.splice(result.destination.index, 0, reorderedItem);

    const newOrder = items.map((item, index) => {
      return { ...item, number: index + 1 };
    });

    setLessons(newOrder);
  };

  const ItemRenderer = ({ item, index }: { item: ILesson; index: number }) => {
    return (
      <div style={{ paddingBottom: "1rem", cursor: "move" }}>
        <EditLesson
          lesson={item}
          handleAddLessonSample={() => handleAddLessonSample(item.id)}
          handleAddSentence={handleAddSentence}
        />
      </div>
    );
  };

  return (
    <div>
      <DragDropContext onDragEnd={handleDragEnd}>
        <Droppable droppableId="droppable">
          {(provided) => (
            <div ref={provided.innerRef} {...provided.droppableProps}>
              {sortedLessons.map((item, index) => {
                return (
                  <Draggable
                    key={item.id}
                    draggableId={item.id.toString()}
                    index={index}
                  >
                    {(provided) => (
                      <div
                        ref={provided.innerRef}
                        {...provided.draggableProps}
                        {...provided.dragHandleProps}
                      >
                        {ItemRenderer({ item, index })}
                      </div>
                    )}
                  </Draggable>
                );
              })}
              {provided.placeholder}
            </div>
          )}
        </Droppable>
      </DragDropContext>
      <Button type="primary" onClick={handleAddLesson}>
        <PlusOutlined /> Add lesson
      </Button>
    </div>
  );
};

export default EditCourseLessons;
