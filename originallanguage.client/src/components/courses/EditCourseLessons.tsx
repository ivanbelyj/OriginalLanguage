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
import { useLessons } from "../../hooks/useLessons.ts";

export interface EditLessonsProps {
  courseId: string;
}

const EditCourseLessons: React.FC<EditLessonsProps> = ({
  courseId,
}: EditLessonsProps) => {
  const { courseLessons, postLesson, updateLessonNumbers } = useLessons(
    courseId!
  );
  // const [editedLessons, setEditedLessons] = useState<ILesson[]>(initialLessons);

  const sortedLessons = courseLessons.sort((a, b) => a.number - b.number);

  // useEffect(() => {
  //   setEditedLessons(initialLessons);
  // }, [initialLessons]);

  const handleAddLesson = async () => {
    if (!courseId) return;

    console.log("Add lesson");
    await postLesson({
      courseId: courseId,
      number:
        courseLessons.reduce(
          (acc, lesson) => (lesson.number > acc ? lesson.number : acc),
          0
        ) + 1,
    });
  };

  const handleDragEnd = (result: DropResult) => {
    if (!result.destination) return;

    const items = Array.from(sortedLessons);
    const [reorderedItem] = items.splice(result.source.index, 1);
    items.splice(result.destination.index, 0, reorderedItem);

    // const newOrder = items.map((item, index) => {
    //   return { ...item, number: index + 1 };
    // });

    updateLessonNumbers(
      items.map((item, index) => ({ id: item.id, number: index + 1 }))
    );
    // setEditedLessons(newOrder);
  };

  const ItemRenderer = ({ item, index }: { item: ILesson; index: number }) => {
    return (
      <div style={{ paddingBottom: "1rem", cursor: "move" }}>
        <EditLesson lesson={item} />
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
