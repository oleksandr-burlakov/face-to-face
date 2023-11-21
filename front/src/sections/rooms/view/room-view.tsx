import Peer from "peerjs";
import { useRecoilState } from "recoil";
import React, { useRef, useState, useEffect } from "react"

import { Add } from "@mui/icons-material";
import { Box, Tab, Card, Tabs, Grid, Stack, Button, Container, TextField, Typography } from "@mui/material"

import Connector, { userInfo } from 'src/hooks/signalr-connector';

import {  roomUserAtom } from "src/state/rooom-atom";

function CustomTabPanel(props : CustomTabPanelPropTypes) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          {children}
        </Box>
      )}
    </div>
  );
}

export type CustomTabPanelPropTypes = {
  children: any,
  index: number,
  value: number,
};

const videoConstraints = {
  height: window.innerHeight / 2,
  width: window.innerWidth / 2
};

// Keep list of calls. Close all calls on user leave 

export function RoomView() {
  const [remoteVideos, setRemoteVideos] = useRecoilState(roomUserAtom);
  const [username, setUsername] = useState('');
  const isUnmounting = useRef(false);

  useEffect(() => () => (isUnmounting.current = true), []);

  useEffect(() => () => {
    if (isUnmounting.current) {
      for (const remote of remoteVideos.filter(x => x.peer != null)) {
        if(!remote.peer?.disconnected) {
          remote.peer?.disconnect();
        }
      }
      setRemoteVideos([]);
    }
  });
  
  useEffect(() => {
    const joinRoomFunc = (data: userInfo) => {
      informJoinedUser(username, data.connectionId);
    };
    
    const informUser = (data: userInfo) => {
      navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
        const localPeer = addPeer(data.connectionId, stream);
        sendSignal("", data.connectionId, true);
      });
    };
    
    const sendSignalFunc = (connectionId: string, incomingSignal: string, isReturning: boolean) => {
      navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
        createPeer(connectionId, stream);
      });
    };
    
    const { joinRoom, sendSignal, informJoinedUser, waitForHubConnection, getConnectionId} = Connector({
      onUserJoinedRoom: joinRoomFunc,
      onInformJoinedUser: informUser,
      onSendSignal: sendSignalFunc,
      onUserDisconnect(connectionId) {
        console.log('disconnected');
        setRemoteVideos(v => [...v.filter(x => x.id !== connectionId)]);
      },
    });

    navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
      const id = getConnectionId();
      if (id)
        addStream(stream, id, null);
      waitForHubConnection().then(() => {
        joinRoom(` `);
      });
    });

    function createPeer(userToSignal: string, stream: MediaStream | null) {
      const id = getConnectionId() ?? '';
      const localPeer = new Peer(`${id}-${userToSignal}`, {
        host: "localhost",
        port: 9000,
        path: "/myapp",
      });

      if (stream)
      {
        const call = localPeer.call(`${userToSignal}-${id}`, stream);
        call.on('stream', (localRemoteStream) => {
          addStream(localRemoteStream, userToSignal, localPeer);
        })
      }

      return localPeer;
    }

    function addStream(stream: MediaStream, id: string, peer: Peer | null) {
      setRemoteVideos(v => [...v.filter(x => x.id !== id), {peer, stream, id}]);
    }

    function addPeer(userToSignal: string, stream: MediaStream) {
        const id = getConnectionId() ?? '';
        const localPeer = new Peer(`${id}-${userToSignal}`, {
          host: "localhost",
          port: 9000,
          path: "/myapp",
        });

        localPeer.on('call', (call) => {
            call.answer(stream); // Answer the call with an A/V stream.
            call.on('stream', (localRemoteStream) =>  {
              addStream(localRemoteStream, userToSignal, localPeer);
            });
        });

        return localPeer;
    }

    }, [setRemoteVideos]);


    const [value, setValue] = React.useState(0);
      
      const handleChange = (event: any, newValue: number) => {
        setValue(newValue);
      };

  const createNew = () => {
  };

  return (
    <Container>
      <Stack mb={5}>
        <Typography variant="h4">Meetings</Typography>
      </Stack>
      <Card>
        <Box padding={4}>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <Stack flexDirection='row'>
              <Tabs sx={{width:'100%'}} value={value} onChange={handleChange} centered textColor="secondary" indicatorColor="secondary">
                <Tab label="Active"/>
                <Tab label="Archive"/>
              </Tabs>
              <TextField onChange={(event) => setUsername(event.target.value)}/>
              <Button onClick={createNew} color="secondary" variant="outlined">
                <Add />
              </Button>
            </Stack>
          </Box>
          <CustomTabPanel value={value} index={0} >
            {/* <ActiveList /> */}
            <Grid gridTemplateColumns={[50, 50]}>
              {remoteVideos && remoteVideos.map((v, key) => <Video connectionId={v.id} key={key} content={v.stream}/>)}
            </Grid>
          </CustomTabPanel>
          <CustomTabPanel value={value} index={1}>
            List of participants:
            Item Two
          </CustomTabPanel>
        </Box>
      </Card>
    </Container>
  )
}

const Video = ({content, connectionId} : {content: MediaStream, connectionId: string}) => {
  const ref = useRef({});
  useEffect(() => {
    ref.current.srcObject = content;
  });

  return (
    <div>
      {connectionId}
      <video muted playsInline autoPlay ref={ref} />
    </div>
  );
}



