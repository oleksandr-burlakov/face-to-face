// eslint-disable-next-line import/no-extraneous-dependencies
import axios from 'axios';

import { API_CONSTANTS } from '../utils/globals/api-constants';

const api = axios.create({ baseURL: API_CONSTANTS.url, withCredentials: true });

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (localError) => Promise.reject(localError)
);

export default api;
