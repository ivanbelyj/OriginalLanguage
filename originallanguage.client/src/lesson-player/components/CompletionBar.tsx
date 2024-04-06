import { CloseOutlined } from "@ant-design/icons";
import { Progress, ProgressProps } from "antd";
import { useNavigate } from "react-router-dom";

interface ICompletionBarProps {
  tasksCount: number;
  completedTasksCount: number;
}
export const CompletionBar: React.FC<ICompletionBarProps> = ({
  tasksCount,
  completedTasksCount,
}) => {
  const navigate = useNavigate();

  const twoColors: ProgressProps["strokeColor"] = {
    "0%": "#108ee9",
    "100%": "#87d068",
  };
  const calculatePercent = () => {
    return Math.round((completedTasksCount / tasksCount) * 100);
  };

  const onClose = () => {
    navigate(-1);
  };

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
