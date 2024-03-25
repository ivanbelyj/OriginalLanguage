import React from "react";
import { Draggable } from "react-beautiful-dnd";

interface ElementProps {
  id: string;
  content: string;
  index: number;
}

const SentenceElement: React.FC<ElementProps> = ({ id, content, index }) => {
  return (
    <Draggable draggableId={id} index={index}>
      {(provided) => (
        <div
          className="sentence-element"
          ref={provided.innerRef}
          {...provided.draggableProps}
          {...provided.dragHandleProps}
        >
          {content}
        </div>
      )}
    </Draggable>
  );
};

export default SentenceElement;
