import { RemirrorJSON } from "@remirror/core";
import { ICreateArticle } from "./hooks/useArticles";

export default class ArticleUtils {
  static defaultCreateArticleModel(userId: string): ICreateArticle {
    return {
      authorId: userId,
      title: "New article",
      content: "",
      shortDescription: "",
    };
  }

  public static parseContent(content: string): RemirrorJSON {
    return JSON.parse(content);
  }
  public static stringifyContent(content: RemirrorJSON) {
    return JSON.stringify(content);
  }

  public static getArticleContentLocal(articleId: string) {
    const content = ArticleUtils.getArticleContentLocalString(articleId);
    return content ? ArticleUtils.parseContent(content) : undefined;
  }
  public static getArticleContentLocalString(articleId: string) {
    const storageKey = ArticleUtils.getArticleStorageKey(articleId);
    return window.localStorage.getItem(storageKey);
  }

  public static defaultArticleContent(): RemirrorJSON {
    return {
      type: "doc",
      content: [],
    };
  }
  public static saveArticleContentLocally(
    articleId: string,
    json: RemirrorJSON
  ) {
    window.localStorage.setItem(
      ArticleUtils.getArticleStorageKey(articleId),
      ArticleUtils.stringifyContent(json)
    );
  }

  private static getArticleStorageKey(articleId: string) {
    return `article-${articleId}`;
  }
}
