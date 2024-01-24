const httpsPort = '7243';
// eslint-disable-next-line no-unused-vars, @typescript-eslint/no-unused-vars
const httpPort = '5151';
const base=  `https://localhost:${httpsPort}`;
export const API_CONSTANTS = {
  url: `${base}/api/`,
  hub:`${base}/hub/`,
  aiHub:`${base}/ai-hub/`,
  staticFilesUrl: `${base}/StaticFiles`,
  clientBaseUrl : `localhost:3030`
};
