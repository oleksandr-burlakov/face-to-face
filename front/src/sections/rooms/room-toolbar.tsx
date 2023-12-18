import React, { useState } from "react";

import { Mic, ExitToApp, PhotoCamera } from "@mui/icons-material";
import { Box, Stack, Theme, Button, SxProps } from "@mui/material";
import { Link } from "react-router-dom";

const buttonCss: SxProps<Theme> = {
  padding: '22px',
  borderRadius: '100%'
};

export function RoomToolbar({micButtonCallback, cameraButtonCallback}: {micButtonCallback: () => void, cameraButtonCallback: () => void}) {
  const [isMuted, setIsMuted] = useState<boolean>(false);
  const [isCameraOff, setIsCameraOff] = useState<boolean>(false);

  const toggleMic = () => {
    setIsMuted(!isMuted);
    micButtonCallback();
  };

  const toggleVideo = () => {
    setIsCameraOff(!isCameraOff);
    cameraButtonCallback();
  };

  return (
    <Box sx={{flex: '0 1 10px',  padding: '10px', boxShadow: '1px 1px 1px 1px black'}}>
      <Stack flexDirection="row" height="100%" justifyContent='center' columnGap='10px'>
        <Button variant="contained" color={isMuted ? "error" : "primary"} sx={buttonCss} onClick={toggleMic}>
          <Mic/>
        </Button>
        <Button variant="contained" color={isCameraOff ? "error" : "primary"} sx={buttonCss} onClick={toggleVideo}>
          <PhotoCamera />
        </Button>
        <Link to="/meetings">
          <Button variant="contained" color="secondary" sx={buttonCss} onClick={toggleVideo}>
            <ExitToApp/>
          </Button>
        </Link>
      </Stack>
    </Box>
  );
}