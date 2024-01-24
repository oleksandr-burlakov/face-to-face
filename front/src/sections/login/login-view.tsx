import React, { useState } from 'react';
import { useSearchParams} from 'react-router-dom';

import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Divider from '@mui/material/Divider';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import LoadingButton from '@mui/lab/LoadingButton';
import { alpha, useTheme } from '@mui/material/styles';
import InputAdornment from '@mui/material/InputAdornment';

import { getInfo, authenticate } from 'src/api/account-api';

import Logo from '../../components/logo';
import { bgGradient } from '../../theme/css';
import { useAuth } from '../../hooks/use-auth';
import Iconify from '../../components/iconify';
import { useRouter } from '../../routes/hooks';
import GoogleLoginButton from './google-login-button';

// ----------------------------------------------------------------------

export default function LoginView() {
  const authContext = useAuth();
  const [query] = useSearchParams();

  const successGoogle = query.get('successGoogle');

  const loadInfo = async () => {
    const authenticateCookie = authContext.cookieToken();
    if (!authContext.token && authenticateCookie) {
      const group = JSON.parse(decodeURIComponent(authenticateCookie));
      const {Token} = JSON.parse(decodeURIComponent(authenticateCookie));
      localStorage.setItem('token', Token);
      authContext.setToken(Token);
    }
    if (!authContext.accountInfo) {
      const accountDataResponse = await getInfo();
      if (accountDataResponse.data.succeeded) {
        authContext.setAccountInfo(accountDataResponse.data.result);
      }
    }

  };

  if (successGoogle) {
    loadInfo();
  }
  
  const [formData, setFormData] = useState({username: "",password: ""});

  const handleChange = (event: any) => {
    const { name, value } = event.target;
    setFormData((prevFormData) => ({ ...prevFormData, [name]: value }));
  };

  const theme = useTheme();

  const router = useRouter();

  const [showPassword, setShowPassword] = useState(false);

  const handleClick = async () => {
    const result = await authenticate({username: formData.username, password: formData.password});
    if (result.data.succeeded && authContext) {
      localStorage.setItem('token', result.data.result.token);
      authContext.setToken(result.data.result.token);
      if (!authContext.accountInfo) {
        const accountDataResponse = await getInfo();
        if (accountDataResponse.data.succeeded) {
          authContext.setAccountInfo(accountDataResponse.data.result);
        }
      }
      const redirectTo = query.get('redirectTo');
      if (redirectTo) {
        router.push(redirectTo);
      } else {
        router.push('/');
      }
    }
  };

  const renderForm = (
    <>
      <Stack spacing={3}>
        <TextField name="username" label="Login" onChange={handleChange} />

        <TextField
          name="password"
          label="Password"
          onChange={handleChange}
          type={showPassword ? 'text' : 'password'}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton onClick={() => setShowPassword(!showPassword)} edge="end">
                  <Iconify icon={showPassword ? 'eva:eye-fill' : 'eva:eye-off-fill'} sx={undefined} width={0} />
                </IconButton>
              </InputAdornment>
            ),
          }}
        />
      </Stack>

      <Stack direction="row" alignItems="center" justifyContent="flex-end" sx={{ my: 3 }}>
        <Link variant="subtitle2" underline="hover">
          Forgot password?
        </Link>
      </Stack>

      <LoadingButton
        fullWidth
        size="large"
        type="submit"
        variant="contained"
        color="inherit"
        disabled={!formData.username || !formData.password}
        onClick={handleClick}
      >
        Login
      </LoadingButton>
    </>
  );

  return (
    <Box
      sx={{
        ...bgGradient({
          color: alpha(theme.palette.background.default, 0.9),
          imgUrl: '/assets/background/overlay_4.jpg',
        }),
        height: 1,
      }}
    >
      <Logo
        sx={{
          position: 'fixed',
          top: { xs: 16, md: 24 },
          left: { xs: 16, md: 24 },
        }} disabledLink={false}      />

      <Stack alignItems="center" justifyContent="center" sx={{ height: 1 }}>
        <Card
          sx={{
            p: 5,
            width: 1,
            maxWidth: 420,
          }}
        >
          <Typography variant="h4">Sign in to Minimal</Typography>

          <Typography variant="body2" sx={{ mt: 2, mb: 5 }}>
            Don’t have an account?
            <Link variant="subtitle2" sx={{ ml: 0.5 }}>
              Get started
            </Link>
          </Typography>

          <Stack direction="row" spacing={2}>
            <GoogleLoginButton />
          </Stack>

          <Divider sx={{ my: 3 }}>
            <Typography variant="body2" sx={{ color: 'text.secondary' }}>
              OR
            </Typography>
          </Divider>

          {renderForm}
        </Card>
      </Stack>
    </Box>
  );
}