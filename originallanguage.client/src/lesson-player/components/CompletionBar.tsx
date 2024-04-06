import { CloseOutlined } from "@ant-design/icons";
import { Progress, ProgressProps } from "antd";
import { useNavigate } from "react-router-dom";
import { CloseButton } from "../../common/components/CloseButton";

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
      <CloseButton
        onClose={onClose}
        closeModalTitle="Exit lesson"
        closeModalContent={
          "Are you sure you want to exit the lesson? " +
          "Current lesson passing data will be lost."
        }
      />

      <Progress
        percent={calculatePercent()}
        strokeColor={twoColors}
        style={{ margin: 0 }}
      />
    </div>
  );
};
