import React from "react";

interface IElementProps {
  content: string;
  onClick: () => void;
}

export const SentenceElement: React.FC<IElementProps> = ({
  content,
  onClick,
}) => {
  return (
    <div className="sentence-element" onClick={onClick}>
      {content}
    </div>
  );
};
