export default interface ILessonSample {
  id: string;
  minimalProgressLevel: number;
  lessonId: string;
  mainText?: string;
  mainTranslation?: string;
  textHints?: string;
  translationHints?: string;
  glosses?: string;
  transcription?: string;
}
