import { Button } from "antd";
import {
  DragDropContext,
  Droppable,
  Draggable,
  DropResult,
  DraggableProvidedDragHandleProps,
} from "react-beautiful-dnd";
import { PlusOutlined } from "@ant-design/icons";
import EditLesson from "./EditLesson.tsx";
import { useLessons } from "../../hooks/useLessons.ts";
import ILesson from "../../models/ILesson.ts";

export interface EditLessonsProps {
  courseId: string;
}

const EditCourseLessons: React.FC<EditLessonsProps> = ({
  courseId,
}: EditLessonsProps) => {
  const { courseLessons, postLesson, updateLessonNumbers, deleteLesson } =
    useLessons(courseId!);

  const sortedLessons = courseLessons.sort((a, b) => a.number - b.number);

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

    updateLessonNumbers(
      items.map((item, index) => ({ id: item.id, number: index + 1 }))
    );
  };

  const ItemRenderer = ({
    item,
    dragHandleProps,
    index,
  }: {
    item: ILesson;
    index: number;
    dragHandleProps: DraggableProvidedDragHandleProps | null | undefined;
  }) => {
    return (
      <div style={{ paddingBottom: "1rem" }}>
        <EditLesson
          lesson={item}
          dragHandleProps={dragHandleProps}
          onDelete={(id: string) => deleteLesson(id)}
        />
      </div>
    );
  };

  return (
    <>
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
                      <div ref={provided.innerRef} {...provided.draggableProps}>
                        {ItemRenderer({
                          item,
                          index,
                          dragHandleProps: provided.dragHandleProps,
                        })}
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
    </>
  );
};

export default EditCourseLessons;
