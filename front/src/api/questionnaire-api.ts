import { GenericResponse } from 'src/models/generic-response';
import { AddQuestionnareModelType } from 'src/models/questionnaire/add-questionnaire-model';
import { UpdateQuestionnaireModel } from 'src/models/questionnaire/update-questionnaire-model';
import { GetMyQuestionnaireModelType } from 'src/models/questionnaire/get-my-questionnaire-model';

import api from './axios';


const addQuestionnaire = async (model: AddQuestionnareModelType) =>
  api.post<GenericResponse<string>>(
    '/Questionnaire/add',
    model,
    {
      method: 'POST',
    }
  );

const updateQuestionnaire = async (model: UpdateQuestionnaireModel) =>
    api.put<GenericResponse<string>>(
      '/Questionnaire/update',
      model,
    );

const getMyQuestionnaires = async () => api.get<GenericResponse<GetMyQuestionnaireModelType[]>>('/Questionnaire/get-my');

const deleteQuestionnaire = async (id: string) => api.delete(`/Questionnaire/delete?id=${id}`);

export { addQuestionnaire, getMyQuestionnaires, updateQuestionnaire, deleteQuestionnaire};
