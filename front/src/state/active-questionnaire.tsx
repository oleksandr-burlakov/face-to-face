import { atom } from "recoil";

import { GetMyQuestionnaireModelType } from "src/models/questionnaire";

export const activeQuestionnaireAtom = atom<GetMyQuestionnaireModelType | null>({
  key: 'ActiveQuestionnaire',
  default: null
});