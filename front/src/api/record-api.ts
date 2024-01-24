
import { SendDataModel, EndRecordModel } from 'src/models/records/';

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

const endRecord = async (model: EndRecordModel) =>
  api.post<void>(
    `/${baseUrl}/end-record`,
    model,
    {
      method: 'POST',
    }
  );
  


export { sendData, endRecord } ;
