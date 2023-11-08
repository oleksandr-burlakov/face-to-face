import React, { useMemo, useState, useEffect, useContext } from 'react';


export interface AuthContextType {
  token: string | null,
  setToken: (newToken: string | null) => void
}

const AuthContext = React.createContext<AuthContextType | null>(null);

export type AuthProviderPropTypes = {
  children: any
};
// eslint-disable-next-line react/prop-types
const AuthProvider = ({ children }: AuthProviderPropTypes) => {
  const [token, setToken_] = useState(localStorage.getItem('token'));

  const setToken = (newToken: string | null) => {
    setToken_(newToken);
  };

  useEffect(() => {
    if (token) {
      localStorage.setItem('token', token);
    } else {
      localStorage.removeItem('token');
    }
  }, [token]);

  // Memoized value of the authentication context
  const contextValue = useMemo<AuthContextType>(
    () => ({
      token,
      setToken,
    }),
    [token]
  );

  return (<AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>);
};

export const useAuth = () => useContext(AuthContext);

export default AuthProvider;
