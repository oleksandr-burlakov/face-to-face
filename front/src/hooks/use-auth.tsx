import React, { useMemo, useState, useEffect, useContext } from 'react';

import { GetInfoResponseType } from 'src/models/account/get-info-model';


export interface AuthContextType {
  token: string | null,
  setToken: (newToken: string | null) => void,
  accountInfo: GetInfoResponseType | null,
  setAccountInfo: (accountInfo: GetInfoResponseType | null) => void,
}

const AuthContext = React.createContext<AuthContextType | null>(null);

export type AuthProviderPropTypes = {
  children: any
};
// eslint-disable-next-line react/prop-types
const AuthProvider = ({ children }: AuthProviderPropTypes) => {
  const [token, setToken_] = useState(localStorage.getItem('token'));
  const accountInfoJson = localStorage.getItem('account-info');
  const [accountInfo, setAccountInfo_] = useState<GetInfoResponseType | null>(accountInfoJson ?  JSON.parse(accountInfoJson) : null);

  const setToken = (newToken: string | null) => {
    setToken_(newToken);
  };

  const setAccountInfo = (localInfo: GetInfoResponseType | null) => {
    setAccountInfo_(localInfo);
  }

  useEffect(() => {
    if (token) {
      localStorage.setItem('token', token);
    } else {
      localStorage.removeItem('token');
    }
  }, [token]);

  useEffect(() => {
    if (accountInfo) {
      localStorage.setItem('account-info', JSON.stringify(accountInfo));
    } else {
      localStorage.removeItem('account-info');
    }
  }, [accountInfo]);

  // Memoized value of the authentication context
  const contextValue = useMemo<AuthContextType>(
    () => ({
      token,
      setToken,
      accountInfo,
      setAccountInfo
    }),
    [token, accountInfo]
  );

  return (<AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>);
};

export const useAuth = () => useContext(AuthContext);

export default AuthProvider;
