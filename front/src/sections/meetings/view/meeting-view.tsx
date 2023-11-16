import Peer from "peerjs";
import React, { useRef, useState, useEffect } from "react"

import { Add } from "@mui/icons-material";
import { Box, Tab, Card, Tabs, Stack, Button, Container, TextField, Typography } from "@mui/material"

import Connector, { userInfo } from 'src/hooks/signalr-connector';



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
          <Typography>{children}</Typography>
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

type PeerUser = {
  peer: Peer.Instance,
  id: string
};

export function MeetingView() {
  
  const remoteVideo = useRef<HTMLVideoElement>();
  const [username, setUsername] = useState('');
  const [peer, setPeer] = useState<Peer>();
  const userVideo = useRef<HTMLVideoElement>(null);

  const joinRoomFunc = (data: userInfo) => {
    navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
      informJoinedUser(username, data.connectionId);
    });
  };
  
  const informUser = (data: userInfo) => {
    navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
      const localPeer = addPeer(stream);
      setPeer(localPeer);
      sendSignal("", data.connectionId, true);
    });
  };
  
  const sendSignalFunc = (connectionId: string, incomingSignal: string, isReturning: boolean) => {
    navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
      const localPeer = createPeer(connectionId, stream);
      setPeer(localPeer);
    });
  };

  const { joinRoom, sendSignal, informJoinedUser, waitForHubConnection, getConnectionId} = Connector({
    onUserJoinedRoom: joinRoomFunc,
    onInformJoinedUser: informUser,
    onSendSignal: sendSignalFunc,
    onUserDisconnect(connectionId) {
      console.log(connectionId);
    },
  });

  useEffect(() => {
    navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true}).then((stream) => {
      userVideo.current.srcObject = stream;
      waitForHubConnection().then(() => {
        joinRoom(` `);
      });
    });
    }, []);

    function createPeer(userToSignal: string, stream: MediaStream | null) {
      const id = getConnectionId() ?? '';
      const localPeer = new Peer(id, {
        host: "localhost",
        port: 9000,
        path: "/myapp",
      });

      if (stream)
      {
        const call = localPeer.call(userToSignal, stream);
        call.on('stream', (localRemoteStream) => {
          console.log("STREAMING")
          remoteVideo.current.srcObject = localRemoteStream;
        })
      }

      return localPeer;
    }

    function addPeer(stream: MediaStream) {
        const id = getConnectionId() ?? '';
        const localPeer = new Peer(id, {
          host: "localhost",
          port: 9000,
          path: "/myapp",
        });

        localPeer.on('call', (call) => {
            call.answer(stream); // Answer the call with an A/V stream.
            call.on('stream', (localRemoteStream) =>  {
              remoteVideo.current.srcObject = localRemoteStream;
            });
        });

        return localPeer;
    }

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
          <CustomTabPanel value={value} index={0}>
            {getConnectionId()}
            {/* <ActiveList /> */}
            <video ref={userVideo} autoPlay muted />
            {remoteVideo && <video ref={remoteVideo} autoPlay muted /> }
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

// const Video = ({peer} : {peer: Peer.Instance}) => {
//   const ref = useRef();

//   useEffect(() => {
//       peer.on("stream", stream => {
//           ref.current.srcObject = stream;
//       })
//   }, []);

//     return (
//       <>
//         Item
//         <video playsInline autoPlay ref={ref} />
//       </>
//     );
// }

