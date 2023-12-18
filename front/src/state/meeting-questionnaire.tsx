import { atom } from "recoil";
import { QuestionModelType } from "src/models/question";

export const meetingQuestionnaireAtom = atom<QuestionModelType[]>({
  key: 'MeetingQuestionnaire',
  default: []
});