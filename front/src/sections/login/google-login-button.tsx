import React from "react";
import { useSearchParams } from "react-router-dom";

import { Button } from "@mui/material";
import { Google } from "@mui/icons-material";

import { useRouter } from "src/routes/hooks";

import { useAuth } from "src/hooks/use-auth";



export default function GoogleLoginButton() {
  const router = useRouter();
  const authContext = useAuth();
  const [query] = useSearchParams();

  const handleLogin = async () => {
    const containsQuestionMark = window.location.href.indexOf('?') !== -1;
    const currentUrl = encodeURIComponent( `${window.location.href}${containsQuestionMark ? '&' : '?'}successGoogle=true`);
    console.log(currentUrl);
    window.open(`https://localhost:7243/api/Account/external-login?provider=Google&returnUrl=${currentUrl}`, "_self")
  };

  return (
   <Button
      onClick={() => handleLogin()}
      fullWidth
      size="large"
      color="error"
      variant="outlined"
    >
      <Google/>
  </Button> 
  );
}