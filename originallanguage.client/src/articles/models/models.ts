export interface IArticle {
  id: string;
  authorId: string;
  title?: string;
  content?: string;
  dateTimeAdded: Date;
  shortDescription?: string;
}

export interface IArticleInfo {
  title?: string;
  shortDescription?: string;
}
