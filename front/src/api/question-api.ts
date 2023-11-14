import { GenericResponse } from 'src/models/generic-response';
import { QuestionModelType, AddQuestionModelType } from 'src/models/question';

import api from './axios';

const updateQuestion = async (model: QuestionModelType) =>
  api.put<void>(
    '/Question/update',
    model,
    {
      method: 'PUT',
    }
  );

const addQuestion = async (model: AddQuestionModelType) =>
  api.post<GenericResponse<string>>(
    '/Question/insert',
    model,
    {
      method: 'POST',
    }
  );

const getByQuestionnaire = async (questionnaireId: string) => 
  api.get<GenericResponse<QuestionModelType[]>>(`/Question/get-by-questionnaire/${questionnaireId}`);

const deleteQuestion = async (id: string) => api.delete(`/Question/delete/${id}`);

export { addQuestion, deleteQuestion, updateQuestion, getByQuestionnaire};
