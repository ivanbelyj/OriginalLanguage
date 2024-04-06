import React, { ReactNode, useState } from "react";
import { CloseOutlined } from "@ant-design/icons";
import { Modal } from "antd";

interface CloseButtonProps {
  onClose: () => void;
  closeModalTitle?: string;
  closeModalContent?: ReactNode;
}

export const CloseButton: React.FC<CloseButtonProps> = ({
  onClose,
  closeModalContent,
  closeModalTitle,
}) => {
  const [isModalVisible, setIsModalVisible] = useState(false);

  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleOk = () => {
    onClose();
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  return (
    <>
      <div
        onClick={showModal}
        style={{
          padding: "0.4em 0.6em",
          cursor: "pointer",
        }}
      >
        <CloseOutlined />
      </div>
      <Modal
        title={closeModalTitle ?? "Exit confirmation"}
        open={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        {closeModalContent ?? "Are you sure you want to exit?"}
      </Modal>
    </>
  );
};
