import { Alert, Button, Typography } from "antd";
import { ICheckAnswerResult } from "../hooks/useLessonTasks";
import { useEffect, useRef } from "react";
const { Paragraph } = Typography;

interface IPlayerControlProps {
  checkAnswerResult: ICheckAnswerResult | null;
  onPerformAction: () => void;
  isLastTaskCompleted: boolean;
  canCheck: boolean;
}

enum ActionType {
  MoveNext,
  CheckAnswer,
  Finish,
}

export const PlayerControl: React.FC<IPlayerControlProps> = ({
  checkAnswerResult,
  onPerformAction,
  isLastTaskCompleted,
  canCheck,
}) => {
  const getCurrentAction = () => {
    if (checkAnswerResult === null) return ActionType.CheckAnswer;
    else if (isLastTaskCompleted) return ActionType.Finish;
    else return ActionType.MoveNext;
  };
  const getButtonText = () => {
    switch (getCurrentAction()) {
      case ActionType.MoveNext:
        return "Next";
      case ActionType.CheckAnswer:
        return "Check";
      case ActionType.Finish:
        return "Finish";
    }
  };

  const isActionAllowed = () => {
    return getCurrentAction() !== ActionType.CheckAnswer || canCheck;
  };

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.code === "Enter") {
        if (!event.shiftKey && isActionAllowed()) {
          event.preventDefault();
          onPerformAction();
        }
      }
    };
    document.addEventListener("keydown", handleKeyDown);

    return () => {
      document.removeEventListener("keydown", handleKeyDown);
    };
  }, [onPerformAction]);

  return (
    <div>
      <Paragraph>
        <Button
          type="primary"
          style={{ marginTop: 16 }}
          onClick={onPerformAction}
          disabled={!isActionAllowed()}
        >
          {getButtonText()}
        </Button>
      </Paragraph>
      {checkAnswerResult !== null && (
        <Paragraph>
          <Alert
            message={
              checkAnswerResult?.isCorrect
                ? "Correct!"
                : "Incorrect. Correct solution: " +
                  (checkAnswerResult?.correctAnswer.answer ?? "(empty answer)")
            }
            type={checkAnswerResult?.isCorrect ? "success" : "error"}
            showIcon
          />
        </Paragraph>
      )}
    </div>
  );
};
