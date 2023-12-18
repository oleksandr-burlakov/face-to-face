import { MediaConnection } from "peerjs";

export type RoomUser = {
  stream: MediaStream,
  call: MediaConnection | null,
  id: string
};