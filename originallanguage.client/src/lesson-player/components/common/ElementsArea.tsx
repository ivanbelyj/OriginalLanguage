import React from "react";
import { SentenceElement } from "./SentenceElement";

interface ElementAreaProps {
  items: string[];
  onElementClick: (index: number) => void;
}

export const ElementsArea: React.FC<ElementAreaProps> = ({
  items,
  onElementClick,
}) => {
  return (
    <div className="elements-area">
      <div className="elements-area__elements">
        {items.map((item, index) => (
          <div key={index} className="elements-area__element">
            <SentenceElement
              key={index}
              content={item}
              onClick={() => {
                onElementClick(index);
              }}
            />
          </div>
        ))}
      </div>
    </div>
  );
};
