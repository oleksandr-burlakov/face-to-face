import { atom } from "recoil";

import { QuestionModelType } from "src/models/question";

export const questionsAtom = atom<QuestionModelType[]>({
  key: 'QuestionsList',
  default: []
});