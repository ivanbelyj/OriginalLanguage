import { Alert, Button, Typography } from "antd";
import { ICheckAnswerResult } from "../hooks/useLessonTasks";
import { useEffect } from "react";
import { LessonPlayerState } from "../hooks/useTasksPlay";
const { Paragraph } = Typography;

interface IPlayerControlProps {
  checkAnswerResult: ICheckAnswerResult | null;
  onPerformAction: () => void;
  playerState: LessonPlayerState;
}

export const PlayerControl: React.FC<IPlayerControlProps> = ({
  checkAnswerResult,
  onPerformAction,
  playerState,
}) => {
  const getButtonText = (): string => {
    switch (playerState) {
      case LessonPlayerState.NoAnswer:
        return "Check";
      case LessonPlayerState.AnswerGiven:
        return "Check";
      case LessonPlayerState.AnswerChecked:
        return "Next";
      case LessonPlayerState.LessonFinished:
        return "Finish";
    }
  };

  const isActionAllowed = (): boolean => {
    return playerState !== LessonPlayerState.NoAnswer;
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
      {playerState === LessonPlayerState.AnswerChecked && (
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
