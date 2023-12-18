
import { SendDataModel } from 'src/models/records/';

import api from './axios';

const baseUrl = 'Records';

const sendData = async (model: SendDataModel) =>
  api.post<void>(
    `/${baseUrl}/send-data`,
    model,
    {
      method: 'POST',
    }
  );


export {  sendData} ;
