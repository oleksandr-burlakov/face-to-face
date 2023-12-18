import { atom } from "recoil";

import { RoomUser } from "src/models/room/room-user";

export const roomUserAtom = atom<RoomUser[]>({
  key: 'RoomUserList',
  default: []
});
