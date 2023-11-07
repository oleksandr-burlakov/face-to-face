import { useEffect } from 'react';

import { useAuth } from 'src/hooks/use-auth';

import api from './axios';

const AxiosErrorHandler = ({ children }) => {
  const { setToken } = useAuth();

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
            setToken(null);
          }
        }

        return Promise.reject(error);
      }
    );

    return () => {
      api.interceptors.response.eject(responseInterceptor);
    };
  }, [setToken]);

  return children;
};

export default AxiosErrorHandler;
