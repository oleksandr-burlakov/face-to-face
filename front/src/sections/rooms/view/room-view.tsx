import { useRecoilState } from "recoil";
import { useParams } from "react-router-dom";
import Peer, { MediaConnection } from "peerjs";
import React, { useRef, useState, useEffect } from "react"

import { Stack } from "@mui/material"

import { useAuth } from "src/hooks/use-auth";
import Connector, { userInfo } from 'src/hooks/signalr-connector';

import { getMeeting } from "src/api/meeting-api";
import {  roomUserAtom } from "src/state/rooom-atom";
import { QuestionModelType } from "src/models/question";
import { getByQuestionnaire } from "src/api/question-api";
import { getQuestionnaire } from "src/api/questionnaire-api";
import { GetMyQuestionnaireModelType } from "src/models/questionnaire";

import { RoomVideos } from "../room-videos";
import { RoomSidebar } from "../room-sidebar";

const videoConstraints = {
  height: 720,
  width: 1280
};

// Keep list of calls. Close all calls on user leave 

export function RoomView() {
  const {id} = useParams();
  const { accountInfo } = useAuth();
  const [stream, setStream] = useState<MediaStream | null>(null);
  const [remoteVideos, setRemoteVideos] = useRecoilState(roomUserAtom);
  const [questionnaire, setQuestionnaire] = useState<GetMyQuestionnaireModelType | null>(null);
  const [questions, setQuestions] = useState<QuestionModelType[]>([]);
  const onLeavePageCallback = useRef(Promise<void>);
  const [meetingOwner, setMeetingOwner] = useState<string>();

  useEffect(() => {
    async function getQuestionnaireWithQuestions() {
      if (!id)
        return;
      const meeting = await getMeeting(id);
      if (meeting.data.succeeded) {
        const {ownerId} = meeting.data.result;
        setMeetingOwner(ownerId);
        const preferableQuestionnaire = meeting.data.result.preferableQuestionnaireId;
        if (preferableQuestionnaire && ownerId === accountInfo.id) {
          const questionnaireResponse = await getQuestionnaire(preferableQuestionnaire);
          if (questionnaireResponse.data.succeeded) {
            setQuestionnaire(questionnaireResponse.data.result);
            const questionsResponse = await getByQuestionnaire(questionnaireResponse.data.result.id);
            if (questionsResponse.data.succeeded) {
              setQuestions(questionsResponse.data.result);
            }
          }
        }
      }
    }

    getQuestionnaireWithQuestions();
  }, [id]);

  useEffect(() => {
    setRemoteVideos([]);
    async function getStream() {
      const returnValue = await navigator.mediaDevices.getUserMedia({video: videoConstraints, audio: true});
      setStream(returnValue); 
    }
    const initPeers = () => {
      if (!stream) {
        return;
      }
      const joinRoomFunc = (data: userInfo) => {
        informJoinedUser(data.connectionId);
      };
      
      const informUser = (data: userInfo) => {
        addPeer(data.connectionId, stream);
        sendSignal(data.connectionId);
      };
      
      const sendSignalFunc = (connectionId: string) => {
        createPeer(connectionId, stream);
      };
      
      const { joinRoom, sendSignal, informJoinedUser, waitForHubConnection, getConnectionId, disconnect} = Connector({
        onUserJoinedRoom: joinRoomFunc,
        onInformJoinedUser: informUser,
        onSendSignal: sendSignalFunc,
        onUserDisconnect(connectionId) {
          console.log('disconnected');
          setRemoteVideos(v => [...v.filter(x => x.id !== connectionId)]);
        },
      });
  
      
  
      function createPeer(userToSignal: string, localStream: MediaStream | null) {
        const connectionId = getConnectionId() ?? '';
        const localPeer = new Peer(`${connectionId}-${userToSignal}`, {
          host: "localhost",
          port: 9000,
        });
  
        if (localStream)
        {
          const call = localPeer.call(`${userToSignal}-${connectionId}`, localStream);
          call.on('stream', (localRemoteStream) => {
            addStream(localRemoteStream, userToSignal, call);
          })
        }
  
        return localPeer;
      }
  
      function addStream(localStream: MediaStream, connectionId: string, call: MediaConnection | null) {
        setRemoteVideos(v => [...v.filter(x => x.id !== connectionId), {call, stream: localStream, id: connectionId}]);
      }
  
      function addPeer(userToSignal: string, localStream: MediaStream) {
          const connectionId = getConnectionId() ?? '';
          const localPeer = new Peer(`${connectionId}-${userToSignal}`, {
            host: "localhost",
            port: 9000,
          });
  
          localPeer.on('call', (call) => {
              call.answer(localStream); // Answer the call with an A/V stream.
              call.on('stream', (localRemoteStream) =>  {
                addStream(localRemoteStream, userToSignal, call);
              });
          });
  
          return localPeer;
      }
      
      waitForHubConnection().then(() => {
        const connectionId = getConnectionId();
        if (connectionId)
          addStream(stream, connectionId, null);
        if (id)
          joinRoom('user', id);
      });
      return disconnect;
    };

    if (!stream) {
      getStream()
    } else {
      const disconnect = initPeers();

      return () => {
        async function localCallAsync() {
          await disconnect()
        }
        localCallAsync();
      };
    }
  }, [setRemoteVideos, setStream, stream])
  
  useEffect(() => {
    console.log(remoteVideos);
  }, [remoteVideos]);
  
  const toggleMic = () => {
    stream?.getAudioTracks().forEach(a => {a.enabled = !a.enabled});
  };

  const toggleCam = () => {
    stream?.getVideoTracks().forEach(a => {a.enabled = !a.enabled});
  };



  return (
    <Stack flexDirection="row" sx={{height:'100vh', backgroundColor: 'rgb(249, 250, 251)'}}>
      {(meetingOwner === accountInfo.id) && <RoomSidebar meetingId={id} questions={questions} questionnaire={questionnaire} />}
      <RoomVideos stream={stream} remoteVideos={remoteVideos} toggleCam={toggleCam} toggleMic={toggleMic}  />
    </Stack>
  )
}