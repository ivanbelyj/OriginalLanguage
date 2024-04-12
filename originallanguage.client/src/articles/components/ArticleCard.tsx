import React from "react";
import { Card, Typography } from "antd";
import { ContentCard } from "../../common/components/ContentCard";
import { IArticle } from "../models/models";

interface ArticleCardProps {
  article: IArticle;
}

const ArticleCard: React.FC<ArticleCardProps> = ({ article }) => {
  return <ContentCard />;
};

export default ArticleCard;
