export interface ConlangData {
  type: string;
  origin: string;
  articulation: string;
  subjectiveComplexity: string;
  developmentStatus: string;
  settingOrigin: string;
  notHumanoidSpeakers: boolean;
  furrySpeakers: boolean;
}

export default interface ILanguage {
  id: string;
  authorId: string;
  name: string;
  dateTimeCreated: Date;
  dateTimeUpdated: Date;

  nativeName?: string;
  about?: string;
  aboutNativeSpeakers?: string;
  links?: string;
  flagUrl?: string;
  conlangData?: ConlangData;
}
