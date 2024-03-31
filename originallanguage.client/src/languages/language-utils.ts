import { ICreateLanguage } from "./hooks/useLanguages";

export default class LanguageUtils {
  static defaultCreateLanguageModel(userId: string): ICreateLanguage {
    return {
      authorId: userId,
      name: "New Language",
      conlangData: {
        type: "notSpecified",
        origin: "notSpecified",
        articulation: "common",
        subjectiveComplexity: "notSpecified",
        developmentStatus: "notSpecified",
        settingOrigin: "notSpecified",
        notHumanoidSpeakers: false,
        furrySpeakers: false,
      },
    };
  }
}
