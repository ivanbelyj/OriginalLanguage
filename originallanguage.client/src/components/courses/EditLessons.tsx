import { Button, Card, Modal } from "antd";
import {
  DragDropContext,
  Droppable,
  Draggable,
  DropResult,
} from "react-beautiful-dnd";
import { PlusOutlined } from "@ant-design/icons";
import ILesson from "../../models/ILesson";

export interface EditLessonsProps {
  lessons: ILesson[];
  setLessons: (lessons: ILesson[]) => void;
  onAddLesson: () => void;
}

const EditLessons: React.FC<EditLessonsProps> = ({
  lessons,
  setLessons,
  onAddLesson,
}: EditLessonsProps) => {
  //   const { lessons, updateLesson, postLesson, deleteLesson } = useLessons();

  //   const [lessons, setLessons] = useState(initialLessons);

  const sortedLessons = lessons.sort((a, b) => a.number - b.number);

  const handleDragEnd = (result: DropResult) => {
    if (!result.destination) return;

    const items = Array.from(sortedLessons);
    const [reorderedItem] = items.splice(result.source.index, 1);
    items.splice(result.destination.index, 0, reorderedItem);

    const newOrder = items.map((item, index) => {
      return { ...item, number: index + 1 };
    });

    // setLessons(newOrder);
    setLessons(newOrder);
  };

  const ItemRenderer = ({ item, index }: { item: ILesson; index: number }) => {
    return (
      <div style={{ paddingBottom: "1rem", cursor: "move" }}>
        <Card title={item.id}>This is lesson card. {item.number}</Card>
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
                console.log(item);
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
      <Button type="primary" onClick={onAddLesson}>
        <PlusOutlined /> Add lesson
      </Button>
    </div>
  );
};

export default EditLessons;
