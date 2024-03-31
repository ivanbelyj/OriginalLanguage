import { Alert, Button, Typography } from "antd";
import { ICheckAnswerResult } from "../hooks/useLessonTasks";
const { Paragraph } = Typography;

interface IPlayerControlProps {
  checkAnswerResult: ICheckAnswerResult | null;
  onClick: () => void;
  isLastTaskCompleted: boolean;
  canCheck: boolean;
}

enum ButtonState {
  MoveNext,
  CheckAnswer,
  Finish,
}

export const PlayerControl: React.FC<IPlayerControlProps> = ({
  checkAnswerResult,
  onClick,
  isLastTaskCompleted,
  canCheck,
}) => {
  const getButtonState = () => {
    if (checkAnswerResult === null) return ButtonState.CheckAnswer;
    else if (isLastTaskCompleted) return ButtonState.Finish;
    else return ButtonState.MoveNext;
  };
  const getButtonText = () => {
    switch (getButtonState()) {
      case ButtonState.MoveNext:
        return "Next";
      case ButtonState.CheckAnswer:
        return "Check";
      case ButtonState.Finish:
        return "Finish";
    }
  };

  return (
    <div>
      <Paragraph>
        <Button
          type="primary"
          style={{ marginTop: 16 }}
          onClick={onClick}
          disabled={getButtonState() === ButtonState.CheckAnswer && !canCheck}
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
