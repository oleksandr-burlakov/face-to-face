/* eslint-disable perfectionist/sort-imports */
import 'src/global.css';

import { useScrollToTop } from 'src/hooks/use-scroll-to-top';

import Router from 'src/routes/sections';
import ThemeProvider from 'src/theme';
import { RecoilRoot } from 'recoil';
import { GoogleOAuthProvider } from '@react-oauth/google';
import AuthProvider from './hooks/use-auth';
import AxiosErrorHandler from './api/axios-error-handler';

// ----------------------------------------------------------------------

export default function App() {
  useScrollToTop();

  return (
    <ThemeProvider>
      <AuthProvider>
        <GoogleOAuthProvider clientId='173963338050-8th87u43rl55icf2ps57r4mas6uv816u.apps.googleusercontent.com'>
          <AxiosErrorHandler>
            <RecoilRoot>
              <Router />
            </RecoilRoot>
          </AxiosErrorHandler>
        </GoogleOAuthProvider>
      </AuthProvider>
    </ThemeProvider>
  );
}
