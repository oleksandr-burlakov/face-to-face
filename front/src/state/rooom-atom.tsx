import { atom } from "recoil";
import { MediaConnection } from "peerjs";

import { RoomUser } from "src/models/room/room-user";

export const roomUserAtom = atom<RoomUser[]>({
  key: 'RoomUserList',
  default: []
});

export const callAtom = atom<MediaConnection[]>({
  key: 'CallList',
  default: []
});