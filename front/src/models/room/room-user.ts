import Peer from "peerjs";

export type RoomUser = {
  stream: MediaStream,
  peer: Peer|null,
  id: string
};