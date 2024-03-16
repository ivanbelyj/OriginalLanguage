export class ChatGroupUtils {
  static getLanguageGroupId(languageId: string) {
    return this.getGroupId("l", languageId);
  }
  private static getGroupId(scope: string, uniqueInScopeValue: string) {
    return `${scope}:${uniqueInScopeValue}`;
  }
}
