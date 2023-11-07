import api from './axios';

const authenticate = async (username, password) =>
  api.post(
    '/Account/authenticate',
    { username, password },
    {
      method: 'POST',
    }
  );

const getInfo = async () => api.get('/Account/get-info');

export { getInfo, authenticate };
