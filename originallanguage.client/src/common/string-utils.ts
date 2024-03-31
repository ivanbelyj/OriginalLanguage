export default class StringUtils {
  static cropWithEllipsis(str: string, maxLength: number = 200) {
    return str.slice(0, maxLength) + (str.length - 3 > maxLength ? "..." : "");
  }
}
