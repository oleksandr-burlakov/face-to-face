export type AuthenticateModelType = {
    username: string,
    password: string
};

export type AuthenticateResponseType = {
    email: string,
    token: string,
    username: string
};