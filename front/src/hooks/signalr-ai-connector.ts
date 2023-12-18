import * as signalR from "@microsoft/signalr";

import { API_CONSTANTS } from "src/utils/globals/api-constants";

const URL = API_CONSTANTS.aiHub;

export type ConnectorType = {
    onGenerateResponse: (data: string) => void,
};

class AIConnector {
    private connected = false;

    private connection: signalR.HubConnection;

    static instance: AIConnector;

    constructor(params: ConnectorType) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(URL)
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(err => document.write(err)).then(() => {this.connected = true;});
        this.connection.on('onGenerateResponse', (data: string) => {
            params.onGenerateResponse(data);
        });
    }

    public waitForHubConnection = async () => {
        // eslint-disable-next-line no-constant-condition
        while (true) {
            if (this.connected) {
                console.log("Connection verified");
                return;
            }
            // eslint-disable-next-line no-await-in-loop
            await new Promise(resolve => setTimeout(resolve, 200));
        }
    }

    public generateResposne = (request: string) => {
        this.connection.send("generateResponse", this.connection.connectionId, request );
    }

    public static getInstance(params: ConnectorType, isNew: boolean = false): AIConnector {
        if (!AIConnector.instance || isNew)
          AIConnector.instance = new AIConnector(params);
        return AIConnector.instance;
    }
}
export default AIConnector.getInstance;