import { Button } from "antd";
import { useNavigate } from "react-router-dom";

export const EmptyLesson: React.FC = () => {
  const navigate = useNavigate();

  return (
    <div>
      <div>The lesson is not filled with content yet (</div>
      <Button
        type="primary"
        style={{ marginTop: 16 }}
        onClick={() => navigate(-1)}
      >
        Go back
      </Button>
    </div>
  );
};
