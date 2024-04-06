export default class SentenceUtils {
  static readonly explicitSeparator: string = "/";
  static splitSeparatedSentence(sentence: string): string[] {
    return sentence.split(SentenceUtils.explicitSeparator);
  }
}
