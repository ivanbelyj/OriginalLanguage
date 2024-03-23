export class ChatGroupUtils {
  static getLanguageGroupId(languageId: string) {
    return this.getGroupId("l", languageId);
  }
  static getCourseGroupId(courseId: string) {
    return this.getGroupId("c", courseId);
  }
  private static getGroupId(scope: string, uniqueInScopeValue: string) {
    return `${scope}:${uniqueInScopeValue}`;
  }
}
