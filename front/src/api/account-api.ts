import { GenericResponse } from 'src/models/generic-response';
import { GetInfoResponseType } from 'src/models/account/get-info-model';

import api from './axios';
import { AuthenticateModelType, AuthenticateResponseType } from '../models/account/authenticate-model';


const authenticate = async (model: AuthenticateModelType) =>
  api.post<GenericResponse<AuthenticateResponseType>>(
    '/Account/authenticate',
    model,
    {
      method: 'POST',
    }
  );

const getInfo = async () => api.get<GenericResponse<GetInfoResponseType>>('/Account/get-info');

export { getInfo, authenticate };
