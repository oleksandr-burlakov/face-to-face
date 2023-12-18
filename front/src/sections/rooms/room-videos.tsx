/* eslint-disable jsx-a11y/media-has-caption */
import React, { useRef, useEffect } from "react";

import { Box, Grid, Stack } from "@mui/material";

import { RoomUser } from "src/models/room";

import { RoomToolbar } from "./room-toolbar";

export function RoomVideos({remoteVideos, toggleCam, toggleMic, stream}: {remoteVideos: RoomUser[], toggleCam: () => void, toggleMic: () => void, stream : MediaStream | null}) {
  return (
    <Box flexBasis="100%" sx={{display: 'flex', flexDirection: 'column'}}>
        <Stack justifyContent='center' alignItems='center' sx={{flex: '1 1 auto'}}>
          <Grid sx={{display: 'grid', columnGap: '10px',  padding:'10px', gridTemplateColumns: remoteVideos?.slice(0,4)?.map(() => '1fr').join(' ') ?? '1fr' }} gridTemplateColumns={[50, 50]}>
            {remoteVideos && remoteVideos.map((v, key) => <Video connectionId={v.id} key={key} content={v.stream}/>)}
          </Grid>
        </Stack>
        {stream && <RoomToolbar cameraButtonCallback={toggleCam} micButtonCallback={toggleMic} />}
      </Box>
  );
}

const Video = ({content, connectionId} : {content: MediaStream, connectionId: string}) => {
  const ref = useRef<HTMLVideoElement>(null);
  useEffect(() => {
    if (ref && ref.current)
      ref.current.srcObject = content;
  });

  return (
    <div>
      {connectionId}
      <video style={{width:'100%', height: 'auto', borderRadius: '10px'}} muted={false} playsInline autoPlay ref={ref} />
    </div>
  );
}