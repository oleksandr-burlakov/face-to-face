import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import { timeErrorAlert } from 'src/utils/helpers/alert-helper';

import api from './axios';
import { useAuth } from '../hooks/use-auth';

const AxiosErrorHandler = (props: { children: any }) => {
  const auth = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    const responseInterceptor = api.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalRequest = error.config;

        // If the error status is 401 and there is no originalRequest._retry flag,
        // it means the token has expired and we need to refresh it
        if (error.response.status === 401 && !originalRequest._retry) {
          originalRequest._retry = true;

          try {
            // const refreshToken = localStorage.getItem('refreshToken');
            // const response = await axios.post('/api/refresh-token', { refreshToken });
            // const { token } = response.data;
            // localStorage.setItem('token', token);
            // Retry the original request with the new token
            // originalRequest.headers.Authorization = `Bearer ${token}`;
            // return axios(originalRequest);
            throw new Error('No refresh token yet');
          } catch (localError) {
            auth?.setToken(null);
            auth?.deleteAuthenticationCookie();
            navigate('/');
          }
        }

        timeErrorAlert(error.response.data.Errors[0]);

        return Promise.reject(error);
      }
    );

    return () => {
      api.interceptors.response.eject(responseInterceptor);
    };
  }, [auth]);

  return props.children;
};

export default AxiosErrorHandler;
