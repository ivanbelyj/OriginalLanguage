class RouteUtils {
  static main() {
    return "/";
  }
  static about() {
    return "/about";
  }
  static contact() {
    return "/contact";
  }
  static register() {
    return "/register";
  }
  static login() {
    return "/login";
  }
  static profile() {
    return "/profile";
  }
  static logout() {
    return "/logout";
  }
  static courses() {
    return "/courses";
  }
  static course(id?: string) {
    return `/courses/${id ?? ":id"}`;
  }
  static languages() {
    return "/languages";
  }
  static language(id?: string) {
    return `/languages/${id ?? ":id"}`;
  }
  static users() {
    return "/users";
  }
  static userArticles(id?: string) {
    return `/users/${id ?? ":id"}/articles`;
  }
  static manageCourseDefault(id?: string) {
    return `/courses/${id ?? ":id"}/manage`;
  }
  static manageCourse(id?: string, activeTab?: string) {
    return `/courses/${id ?? ":id"}/${activeTab ?? ":activeTab"}`;
  }
  static editLanguage(id?: string) {
    return `/languages/${id ?? ":id"}/edit`;
  }
  static lessonPlayer(id?: string) {
    return `/lessons/${id ?? ":id"}/player`;
  }
  static courseLessons(id?: string) {
    return `/courses/${id ?? ":id"}/lessons`;
  }
  static manageArticle(id?: string) {
    return `/articles/${id ?? ":id"}/manage`;
  }
}

export default RouteUtils;
