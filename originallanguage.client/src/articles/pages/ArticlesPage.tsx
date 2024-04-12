import React from "react";
import { Avatar, Button, List, Typography } from "antd";
import { PlusOutlined } from "@ant-design/icons";
import ArticleCard from "../components/ArticleCard";
import { IArticle } from "../models/models";

const {} = Typography;

const generateFakeArticle = (): IArticle => ({
  id: Math.floor(Math.random() * 1000).toString(),
  authorId: Math.random().toString(36).substring(2, 15),
  title: `Article Title ${Math.floor(Math.random() * 100)}`,
  content: `This is a large content for the article. It contains ${Math.floor(
    Math.random() * 1000
  )} words.`,
  dateTimeAdded: new Date(Date.now() - Math.floor(Math.random() * 10000000000)),
  shortDescription: `Short description for article ${Math.floor(
    Math.random() * 100
  )}`,
});

const fakeArticles: IArticle[] = Array.from({ length: 5 }, generateFakeArticle);

const RenderItem = (article: IArticle, index: number) => {
  return (
    <List.Item>
      <ArticleCard key={article.id} article={article} />
    </List.Item>
  );
};

const ArticlesPage: React.FC = () => {
  return (
    <div>
      <List
        itemLayout="horizontal"
        dataSource={fakeArticles}
        renderItem={RenderItem}
      ></List>
      <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => console.log("Add new article")}
      >
        Add Article
      </Button>
    </div>
  );
};

export default ArticlesPage;
