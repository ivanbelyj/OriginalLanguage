import React, { useState } from "react";
import { Input, Button } from "antd";

interface MessageFormProps {
  onSend: (content: string) => void;
}

const MessageForm: React.FC<MessageFormProps> = ({ onSend }) => {
  const [content, setContent] = useState("");

  const handlePressEnter = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
    if (!e.shiftKey) {
      e.preventDefault();
      handleSubmit();
    }
  };

  const handleSubmit = () => {
    if (content.trim()) {
      onSend(content);
      setContent("");
    }
  };

  return (
    <div className="message-form">
      <Input.TextArea
        className="message-form__textarea"
        value={content}
        onChange={(e) => setContent(e.target.value)}
        onPressEnter={handlePressEnter}
        placeholder="Enter your message..."
      />
      <Button
        className="message-form__send-button"
        onClick={handleSubmit}
        type="primary"
      >
        Send
      </Button>
    </div>
  );
};

export default MessageForm;
