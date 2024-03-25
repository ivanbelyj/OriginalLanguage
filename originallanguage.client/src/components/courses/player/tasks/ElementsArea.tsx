// import React from "react";
// import { Droppable } from "react-beautiful-dnd";
// import { List } from "antd";
// import SentenceElement from "./SentenceElement";
// import "./player.css";

// interface ElementAreaProps {
//   droppableId: string;
//   items: { id: string; content: string }[];
// }

// const ElementsArea: React.FC<ElementAreaProps> = ({ droppableId, items }) => {
//   return (
//     <Droppable droppableId={droppableId} direction="horizontal">
//       {(provided, snapshot) => (
//         <div
//           className="element-area"
//           {...provided.droppableProps}
//           ref={provided.innerRef}
//         >
//           {items.map((item, index) => (
//             <SentenceElement
//               key={item.id}
//               id={item.id}
//               content={item.content}
//               index={index}
//             />
//           ))}
//           {/* <List
//             bordered
//             itemLayout="vertical"
//             dataSource={items}
//             renderItem={(item, index) => (
//               <SentenceElement
//                 key={item.id}
//                 id={item.id}
//                 content={item.content}
//                 index={index}
//               />
//             )}
//           />*/}
//           {provided.placeholder}
//         </div>
//       )}
//     </Droppable>
//   );
// };

// export default ElementsArea;

import React from "react";
import { Droppable } from "react-beautiful-dnd";
import { List } from "antd";
import SentenceElement from "./SentenceElement";
import "./player.css";
import _ from "lodash";

interface ElementAreaProps {
  droppableId: string;
  items: { id: string; content: string }[];
}

export const getElementsAreaRowLength = () => 4;

const ElementsArea: React.FC<ElementAreaProps> = ({ droppableId, items }) => {
  const rows = _.chunk(items, getElementsAreaRowLength());

  return (
    <div>
      {rows.map((row, rowIndex) => (
        <Droppable
          droppableId={`${droppableId}-${rowIndex}`}
          direction="horizontal"
        >
          {(provided, snapshot) => (
            <div
              className="element-area"
              {...provided.droppableProps}
              ref={provided.innerRef}
            >
              {row.map((item, index) => (
                <SentenceElement
                  key={item.id}
                  id={item.id}
                  content={item.content}
                  index={index}
                />
              ))}
              {provided.placeholder}
            </div>
          )}
        </Droppable>
      ))}
    </div>
  );
};

export default ElementsArea;
