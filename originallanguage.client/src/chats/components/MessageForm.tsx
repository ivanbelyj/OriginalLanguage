import React, { useState } from "react";
import { Input, Button } from "antd";

interface MessageFormProps {
  onSend: (content: string) => void;
}

const MessageForm: React.FC<MessageFormProps> = ({ onSend }) => {
  const [content, setContent] = useState("");

  const handleSubmit = () => {
    onSend(content);

    setContent("");
  };

  return (
    <div>
      <Input.TextArea
        value={content}
        onChange={(e) => setContent(e.target.value)}
        placeholder="Enter your message..."
      />
      <Button onClick={handleSubmit} type="primary">
        Send
      </Button>
    </div>
  );
};

export default MessageForm;
