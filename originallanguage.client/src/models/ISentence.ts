export default interface ISentence {
  id: string;
  lessonSampleId: string;

  text?: string;
  translation?: string;
  literalTranslation?: string;
  glosses?: string;
  transcription?: string;
}
