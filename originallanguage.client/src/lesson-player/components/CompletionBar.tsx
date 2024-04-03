import { CloseOutlined } from "@ant-design/icons";
import { Progress, ProgressProps } from "antd";
import { useNavigate } from "react-router-dom";

interface ICompletionBarProps {
  tasksCount: number;
  currentTaskIndex: number;
  isCurrentTaskCompleted: boolean;
}
export const CompletionBar: React.FC<ICompletionBarProps> = ({
  currentTaskIndex,
  tasksCount,
  isCurrentTaskCompleted,
}) => {
  const navigate = useNavigate();

  const twoColors: ProgressProps["strokeColor"] = {
    "0%": "#108ee9",
    "100%": "#87d068",
  };
  const calculatePercent = () => {
    return Math.round(
      ((currentTaskIndex + (isCurrentTaskCompleted ? 1 : 0)) / tasksCount) * 100
    );
  };

  const onClose = () => {
    navigate(-1);
  };

  console.log(calculatePercent());
  return (
    <div
      style={{
        display: "flex",
      }}
    >
      <div
        onClick={onClose}
        style={{
          padding: "0.4em 0.6em",
          cursor: "pointer",
        }}
      >
        <CloseOutlined />
      </div>

      <Progress
        percent={calculatePercent()}
        strokeColor={twoColors}
        style={{ margin: 0 }}
      />
    </div>
  );
};
