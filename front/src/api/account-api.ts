import api from './axios';
import { AuthenticateModelType } from '../models/account/authenticate-model';


const authenticate = async (model: AuthenticateModelType) =>
  api.post(
    '/Account/authenticate',
    model,
    {
      method: 'POST',
    }
  );

const getInfo = async () => api.get('/Account/get-info');

export { getInfo, authenticate };
