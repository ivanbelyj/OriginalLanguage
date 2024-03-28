import React from "react";
import { Input, Typography } from "antd";
import { ITaskProps } from "../../types/ITaskProps";

const { Paragraph } = Typography;

const EnterTextTask: React.FC<ITaskProps> = ({
  task,
  currentAnswer,
  setCurrentAnswer: setAnswer,
}) => {
  return (
    <div>
      <Paragraph>
        <div>{task.question}</div>
      </Paragraph>
      <Paragraph>
        <Input.TextArea
          placeholder="Translate the text"
          value={currentAnswer}
          onChange={(e) => {
            setAnswer(e.target.value);
          }}
        />
      </Paragraph>
    </div>
  );
};

export default EnterTextTask;
