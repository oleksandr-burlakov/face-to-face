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

const googleLogin = async () =>
  api.post(
    `/Account/external-login?provider=Google`,
    {
      method: 'POST',
    },
    {
      headers: {"Access-Control-Allow-Origin": "http://localhost:3030", 'Access-Control-Allow-Credentials':true}
    }
  );

export { getInfo, googleLogin, authenticate };
