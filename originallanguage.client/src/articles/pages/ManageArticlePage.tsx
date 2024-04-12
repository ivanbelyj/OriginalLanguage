import "../articles.css";

import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Tabs } from "antd";
import EditArticle from "../components/edit/EditArticle";
import { Article } from "../components/Article";
import { EditArticleInfo } from "../components/edit/EditArticleInfo";
import { IArticle, IArticleInfo } from "../models/models";
import { useArticles } from "../hooks/useArticles";
import ArticleUtils from "../article-utils";
import { useAuth } from "../../auth/AuthProvider";
import { RemirrorJSON } from "@remirror/core";
import useForceUpdate from "../hooks/useForceUpdate";

function getActualToolbarHeight() {
  return document.getElementById("edit-article-toolbar")?.offsetHeight;
}

class TabKeyConstants {
  static readonly Info = "Info";
  static readonly Content = "Content";
  static readonly Preview = "Preview";
}

const ManageArticlePage = () => {
  const { id: articleId } = useParams();
  const [activeTab, setActiveTab] = useState(TabKeyConstants.Content);
  const [toolbarHeight, setToolbarHeight] = useState(getActualToolbarHeight());
  const [isLoading, setIsLoading] = useState(true);

  const [initialArticle, setInitialArticle] = useState<IArticle>();
  const [articleInfo, setArticleInfo] = useState<IArticleInfo>();
  const [articleContent, setArticleContent] = useState<RemirrorJSON>();

  const { forceUpdate } = useForceUpdate();

  const { getArticle, updateArticle } = useArticles();

  const { getDecodedToken } = useAuth();
  const decodedToken = getDecodedToken();
  const userId = decodedToken?.sub;

  useEffect(() => {
    if (articleId) {
      setIsLoading(true);
      getArticle(articleId).then((article: IArticle) => {
        console.log("set article", article);
        setInitialArticle(article);
        setArticleInfo(article);

        console.log("set article content ", article.content);
        const content = article.content
          ? ArticleUtils.parseContent(article.content)
          : ArticleUtils.defaultArticleContent();

        setArticleContent(content);

        setIsLoading(false);
      });
    }
  }, [articleId]);

  useEffect(() => {
    const updateToolbarHeight = () => {
      const toolbarElementHeight = getActualToolbarHeight();
      if (toolbarElementHeight) {
        setToolbarHeight(toolbarElementHeight);
      }
    };

    updateToolbarHeight();

    window.addEventListener("resize", updateToolbarHeight);

    return () => {
      window.removeEventListener("resize", updateToolbarHeight);
    };
  }, [getActualToolbarHeight()]);

  useEffect(() => {
    // Todo: better fix of setting correct toolbar height
    forceUpdate();
  }, []);

  const saveArticle = () => {
    if (userId && initialArticle) {
      const articleToSave: IArticle = {
        ...initialArticle,
        ...articleInfo,
        ...(articleContent
          ? { content: ArticleUtils.stringifyContent(articleContent) }
          : {}),
      };
      updateArticle(articleToSave.id, articleToSave);
    }
  };

  const handleArticleInfoChanged = (newInfo: IArticleInfo) => {
    setArticleInfo(newInfo);
  };

  const handleArticleContentChanged = (content: RemirrorJSON) => {
    setArticleContent(content);
  };

  const tabItems = [
    {
      label: "Info",
      key: TabKeyConstants.Info,
      children: (
        <EditArticleInfo
          initialArticle={initialArticle}
          isLoading={isLoading}
          onArticleChanged={handleArticleInfoChanged}
          onSave={saveArticle}
        />
      ),
    },
    {
      label: "Content",
      key: TabKeyConstants.Content,
      children: (
        <EditArticle
          isLoading={isLoading}
          onContentChanged={handleArticleContentChanged}
          initialContent={articleContent}
        />
      ),
    },
    {
      label: "Preview",
      key: TabKeyConstants.Preview,
      children: <Article content={articleContent} isLoading={isLoading} />,
    },
  ];

  const handleTabChange = (key: string) => {
    setActiveTab(key);
  };

  console.log("toolbar height", toolbarHeight);
  const tabsStyle =
    activeTab === TabKeyConstants.Content
      ? {
          paddingTop: toolbarHeight,
        }
      : {};

  return (
    <>
      <Tabs
        className="manage-article__tabs"
        items={tabItems}
        activeKey={activeTab}
        onChange={handleTabChange}
        style={tabsStyle}
      ></Tabs>
    </>
  );
};

export default ManageArticlePage;
