import { GenericResponse } from 'src/models/generic-response';
import { MeetingModel, AddMeetingModel, EditMeetingModel } from 'src/models/meeting';

import api from './axios';

const baseUrl = '/Meeting';

const updateMeeting = async (model: EditMeetingModel) =>
  api.put<void>(
    `${baseUrl}/update`,
    model,
    {
      method: 'PUT',
    }
  );

const addMeeting = async (model: AddMeetingModel) =>
  api.post<GenericResponse<string>>(
    `${baseUrl}/insert`,
    model,
    {
      method: 'POST',
    }
  );

  const getMeeting = async (id: string) => 
  api.get<GenericResponse<MeetingModel>>(
    `${baseUrl}/get?id=${id}`,
    {
      method: 'GET'
    }
  );

const getMyMeetings = async () => 
  api.get<GenericResponse<MeetingModel[]>>(`${baseUrl}/get-my`);


export { addMeeting, getMyMeetings, updateMeeting, getMeeting };
