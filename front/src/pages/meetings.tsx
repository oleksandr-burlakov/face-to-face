import React from 'react';
import { Helmet } from 'react-helmet-async';

import { MeetingView } from 'src/sections/meetings/';

// ----------------------------------------------------------------------

export default function MeetingsPage() {
  return (
    <>
      <Helmet>
        <title> Meetings | Minimal UI </title>
      </Helmet>

      <MeetingView />
    </>
  );
}
