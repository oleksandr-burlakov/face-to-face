import { lazy, Suspense } from 'react';
import { Outlet, Navigate, useRoutes } from 'react-router-dom';

import { useAuth } from 'src/hooks/use-auth';

import MeetingsPage from 'src/pages/meetings';
import DashboardLayout from 'src/layouts/dashboard';
import QuestionnairePage from 'src/pages/questionnaires';

export const IndexPage = lazy(() => import('src/pages/app'));
export const BlogPage = lazy(() => import('src/pages/blog'));
export const LoginPage = lazy(() => import('src/pages/login'));
export const ProductsPage = lazy(() => import('src/pages/products'));
export const Page404 = lazy(() => import('src/pages/page-not-found'));
export const QuestionsPage = lazy(() => import('src/pages/questions'))

// ----------------------------------------------------------------------

export default function Router() {
  const { token } = useAuth();

  const routes = useRoutes([
    {
      element: (
        token ? 
        (
        <DashboardLayout>
          <Suspense>
            <Outlet />
          </Suspense>
        </DashboardLayout>) :
        (<Navigate to="/login"/>)
      ),
      children: [
        { element: <IndexPage />, index: true },
        { path: 'products', element: <ProductsPage /> },
        { path: 'blog', element: <BlogPage /> },
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
        }
      ],
    },
    {
      path: 'login',
      element: <LoginPage />,
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
