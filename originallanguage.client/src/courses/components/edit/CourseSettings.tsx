import React from "react";
import {
  Typography,
  Popconfirm,
  Button,
  Descriptions,
  DescriptionsProps,
  List,
  Row,
  Col,
  Card,
} from "antd";
import { DeleteOutlined } from "@ant-design/icons";
import PopconfirmButton from "../../../common/components/PopconfirmButton";
import { useCourses } from "../../hooks/useCourses";
import { useNavigate } from "react-router-dom";
import "../../styles/course-settings.css";

const { Title } = Typography;

interface ICourseSettingsProps {
  courseId: string;
}

const CourseSettings: React.FC<ICourseSettingsProps> = ({ courseId }) => {
  const { deleteCourse } = useCourses();
  const navigate = useNavigate();

  const onDelete = async () => {
    await deleteCourse(courseId);
    navigate("/", { replace: true });
  };

  const getRow = (description: string, children: any) => {
    return (
      <div className="course-settings-box__row">
        <div className="course-settings-box__description">{description}</div>
        <div
          className="course-settings-box__content"
          style={{ display: "flex", justifyContent: "flex-end" }}
        >
          {children}
        </div>
      </div>
    );
    // return (
    //   <Row
    //     align={"middle"}
    //     style={{ paddingTop: "0.6em", paddingBottom: "0.6em" }}
    //   >
    //     <Col span={18} push={6}>
    //       {children}
    //     </Col>
    //     <Col span={6} pull={18}>
    //       {description}
    //     </Col>
    //   </Row>
    // );
  };

  return (
    <>
      <Title level={3}>Course settings</Title>
      <div className="course-settings-box">
        {getRow(
          "The course will be available to all users",
          <Button>Publish</Button>
        )}
        {getRow(
          "Delete the course forever",
          <div>
            <PopconfirmButton
              onConfirm={() => onDelete()}
              buttonProps={{
                danger: true,
                type: "default",
                icon: <DeleteOutlined />,
              }}
            >
              Delete course
            </PopconfirmButton>
          </div>
        )}
      </div>
    </>
  );
};

export default CourseSettings;
