import React, { useState } from "react";
import { Button, Popconfirm } from "antd";

interface IPopconfirmButtonProps {
  confirmTitle?: string;
  confirmDescription?: string;
  onConfirm: () => Promise<void>;
  onCancel?: () => void;
  buttonProps?: React.ComponentProps<typeof Button>;
  children?: React.ReactNode;
}

const defaultProps: IPopconfirmButtonProps = {
  confirmTitle: "Please Confirm",
  confirmDescription:
    "Are you sure you want to proceed? This action cannot be undone.",
  onConfirm: async () => {},
  onCancel: () => {},
  buttonProps: {},
  children: "Confirm Action",
};

const PopconfirmButton: React.FC<IPopconfirmButtonProps> = (props) => {
  const {
    confirmTitle,
    confirmDescription,
    onConfirm,
    onCancel,
    buttonProps,
    children,
  } = { ...defaultProps, ...props };

  const [open, setOpen] = useState(false);
  const [confirmLoading, setConfirmLoading] = useState(false);

  const showPopconfirm = () => {
    console.log("show popconfirm");
    setOpen(true);
  };

  const handleOk = async () => {
    setConfirmLoading(true);
    try {
      await onConfirm();
    } finally {
      setOpen(false);
      setConfirmLoading(false);
    }
  };

  const { onClick: externalOnClick, ...restButtonProps } = buttonProps || {};
  delete buttonProps?.onClick;

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    console.log("handle click");
    showPopconfirm();
    if (externalOnClick) {
      externalOnClick(event);
    }
  };

  const handleCancel = () => {
    console.log("Clicked cancel button");
    setOpen(false);
    if (onCancel) {
      onCancel();
    }
  };

  return (
    <Popconfirm
      title={confirmTitle}
      description={confirmDescription}
      open={open}
      onConfirm={handleOk}
      okButtonProps={{
        loading: confirmLoading,
      }}
      onCancel={handleCancel}
    >
      <Button onClick={handleClick} {...buttonProps}>
        {children}
      </Button>
    </Popconfirm>
  );
};

export default PopconfirmButton;
