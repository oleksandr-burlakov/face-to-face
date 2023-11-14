/* eslint-disable perfectionist/sort-imports */
import 'src/global.css';

import { useScrollToTop } from 'src/hooks/use-scroll-to-top';

import Router from 'src/routes/sections';
import ThemeProvider from 'src/theme';
import { RecoilRoot } from 'recoil';
import AuthProvider from './hooks/use-auth';
import AxiosErrorHandler from './api/axios-error-handler';

// ----------------------------------------------------------------------

export default function App() {
  useScrollToTop();

  return (
    <ThemeProvider>
      <AuthProvider>
        <AxiosErrorHandler>
          <RecoilRoot>
            <Router />
          </RecoilRoot>
        </AxiosErrorHandler>
      </AuthProvider>
    </ThemeProvider>
  );
}
