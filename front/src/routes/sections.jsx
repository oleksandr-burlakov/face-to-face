import { lazy, Suspense } from 'react';
import { Outlet, Navigate, useRoutes, useLocation  } from 'react-router-dom';

import { useAuth } from 'src/hooks/use-auth';

import MeetingsPage from 'src/pages/meetings';
import DashboardLayout from 'src/layouts/dashboard';
import QuestionnairePage from 'src/pages/questionnaires';

export const IndexPage = lazy(() => import('src/pages/app'));
export const LoginPage = lazy(() => import('src/pages/login'));
export const Page404 = lazy(() => import('src/pages/page-not-found'));
export const QuestionsPage = lazy(() => import('src/pages/questions'))
export const RoomPage = lazy(() => import('src/pages/room'))

// ----------------------------------------------------------------------

export default function Router() {
  const { token, setToken, cookieToken,setAccountInfo, accountInfo } = useAuth();
  const location = useLocation();
  const locationPath = location.pathname;
  const authenticationCookie = cookieToken();

  


  const routes = useRoutes([
    {
      element: (
        token != null  ? 
        (
        <DashboardLayout>
          <Suspense>
            <Outlet />
          </Suspense>
        </DashboardLayout>) :
        ( <Navigate to={`/login?redirectTo=${locationPath}`}/>)
      ),
      children: [
        { element: <IndexPage />, index: true },
        { 
          path: 'questionnaires',
          children: [
            {
              path: '',
              element: <QuestionnairePage />
            },
            {
              path: ':id',
              element: <QuestionsPage/>
            }
          ] 
        },
        {
          path: 'meetings',
          children: [
            {
              path: '',
              element: <MeetingsPage/>
            }
          ]
        },
      ],
    },
    {
      path: 'room',
      element: (
        token != null  ? 
        <Outlet /> :
        ( <Navigate to={`/login?redirectTo=${locationPath}`}/>)
        ),
      children: [
        {
          path: ':id',
          element: <RoomPage />
        }
      ]
    },
    {
      path: 'login',
      element: ( 
        token != null  ?
        ( <Navigate to="/"/>) :
        <LoginPage />
      ),
    },
    {
      path: '404',
      element: <Page404 />,
    },
    {
      path: '*',
      element: <Navigate to="/404" replace />,
    },
  ]);

  return routes;
}
