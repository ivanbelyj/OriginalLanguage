import { RemirrorJSON } from "@remirror/core";
import { RemirrorRenderer } from "@remirror/react";
import React from "react";
import Loading from "../../common/components/Loading";

export interface IArticleProps {
  isLoading: boolean;
  content?: RemirrorJSON;
}

export const Article: React.FC<IArticleProps> = ({ isLoading, content }) => {
  if (isLoading) return <Loading />;
  else if (content) return <RemirrorRenderer json={content} />;
  else return <div>No content yet</div>;
};
