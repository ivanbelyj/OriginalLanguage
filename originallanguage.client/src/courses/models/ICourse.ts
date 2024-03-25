export default interface ICourse {
  id: string;
  authorId: string;
  languageId?: number;
  title?: string;
  dateTimeAdded: Date;
}
