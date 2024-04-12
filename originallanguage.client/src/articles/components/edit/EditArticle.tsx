import { useCallback, useEffect, useState } from "react";
import type { RemirrorJSON } from "remirror";
import { ArticleEditor } from "../../editor/ArticleEditor";
import Loading from "../../../common/components/Loading";

interface IEditArticleProps {
  initialContent?: RemirrorJSON;
  isLoading: boolean;
  onContentChanged: (content: RemirrorJSON) => void;
}

const EditArticle = ({
  initialContent,
  isLoading,
  onContentChanged,
}: IEditArticleProps) => {
  const handleEditorChange = useCallback((json: RemirrorJSON) => {
    onContentChanged(json);
  }, []);

  return isLoading ? (
    <Loading />
  ) : (
    <ArticleEditor
      onChange={handleEditorChange}
      initialContent={initialContent}
    ></ArticleEditor>
  );
};
export default EditArticle;
