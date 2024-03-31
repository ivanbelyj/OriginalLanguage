import { Alert, Button, Typography } from "antd";
import { ICheckAnswerResult } from "../hooks/useLessonTasks";
const { Paragraph } = Typography;

interface IPlayerControlProps {
  checkAnswerResult: ICheckAnswerResult | null;
  onClick: () => void;
}

export const PlayerControl: React.FC<IPlayerControlProps> = ({
  checkAnswerResult,
  onClick,
}) => {
  return (
    <div>
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
      <Paragraph>
        <Button type="primary" style={{ marginTop: 16 }} onClick={onClick}>
          {checkAnswerResult === null ? "Check" : "Next"}
        </Button>
      </Paragraph>
    </div>
  );
};
