import { CloseOutlined } from "@ant-design/icons";
import { Progress, ProgressProps } from "antd";

interface ICompletionBarProps {
  tasksCount: number;
  currentTaskIndex: number;
}
export const CompletionBar: React.FC<ICompletionBarProps> = ({
  currentTaskIndex,
  tasksCount,
}) => {
  const twoColors: ProgressProps["strokeColor"] = {
    "0%": "#108ee9",
    "100%": "#87d068",
  };
  const calculatePercent = () => {
    return (currentTaskIndex / tasksCount) * 100;
  };

  const onClose = () => {
    console.log("closing lesson");
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
