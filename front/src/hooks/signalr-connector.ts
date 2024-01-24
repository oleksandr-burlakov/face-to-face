import * as signalR from "@microsoft/signalr";

import { API_CONSTANTS } from "src/utils/globals/api-constants";

const URL = API_CONSTANTS.hub;

export type userInfo = {
    userName: string,
    connectionId: string
};

export type ConnectorType = {
    onUserJoinedRoom: (data: userInfo) => void,
        onInformJoinedUser: (data: userInfo) => void,
        onSendSignal: (connectionId: string) => void,
        onUserDisconnect: (connectionId: string) => void
};

class Connector {

    private connected = false;

    private connection: signalR.HubConnection;

    static instance: Connector;

    constructor(params: ConnectorType) {
        this.isCalledDisconnect = false;
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(URL)
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(err => document.write(err)).then(() => {this.connected = true;});
        this.connection.on('onInformJoinedUser', (data: any) => {
            params.onInformJoinedUser(JSON.parse(data));
        });
        this.connection.on('onJoinRoom', (data: any) => {
            params.onUserJoinedRoom(JSON.parse(data))
        });
        this.connection.on('onSendSignal', (connectionId: string) => {
            params.onSendSignal(connectionId);
        })
        this.connection.on('onUserDisconnect', (connectionId: string) => {
            params.onUserDisconnect(connectionId);
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

    public joinRoom = (userName: string, meetingId: string) => {
        this.connection.send("joinRoom", userName, meetingId);
    }

    public informJoinedUser = (user: string) => {
        this.connection.send("informJoinedUser", user);
    }

    public sendSignal = (user: string) => {
        this.connection.send("sendSignal",  user);
    }

    public getConnectionId = () => this.connection.connectionId

    public static getInstance(params: ConnectorType): Connector {
        if (!Connector.instance || Connector.instance.isCalledDisconnect)
            Connector.instance = new Connector(params);
        return Connector.instance;
    }

    private isCalledDisconnect: boolean = false;

    public disconnect = async () => {
        try {
            await this.connection.stop();
            this.isCalledDisconnect = true;
        } catch (exception) {
            console.log(exception)
        }
    }
}
export default Connector.getInstance;