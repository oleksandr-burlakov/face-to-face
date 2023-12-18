import React from 'react';
import { Helmet } from 'react-helmet-async';

import { RoomView } from 'src/sections/rooms/view/room-view';

// ----------------------------------------------------------------------

export default function RoomPage() {
  return (
    <>
      <Helmet>
        <title> Room | Minimal UI </title>
      </Helmet>

      <RoomView />
    </>
  );
}
