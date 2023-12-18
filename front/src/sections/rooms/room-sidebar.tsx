import React, { useState } from "react";
import { useRecoilState } from "recoil";

import { Menu, MenuOpen } from "@mui/icons-material";
import { Box, Stack, Button, Checkbox, Typography, FormControlLabel } from "@mui/material";

import { errorAlert } from "src/utils/helpers/alert-helper";

import { sendData } from "src/api/record-api";
import { meetingQuestionnaireAtom } from "src/state";
import { QuestionModelType } from "src/models/question";
import { GetMyQuestionnaireModelType } from "src/models/questionnaire";


export function RoomSidebar({questions, questionnaire, meetingId }: {questions: QuestionModelType[], questionnaire: GetMyQuestionnaireModelType | null, meetingId?: string }) {
  const [currentPage, setCurrentPage] = useState<number>(0);
  const [isRecording, setIsRecording] = useState<boolean>(false);
  const [answeredQuestions, setAnsweredQuestion] = useRecoilState(meetingQuestionnaireAtom);
  const maxPerPageQuestions = 5;
  
  const getQuestions = (page: number) => questions.slice(0 + page*maxPerPageQuestions, maxPerPageQuestions + page*maxPerPageQuestions).map((q) => 
      <Stack flexDirection="row" columnGap={1} alignItems='center'>
        <FormControlLabel control={
        <Checkbox 
          defaultChecked={answeredQuestions.includes(q)}
          disabled={answeredQuestions.includes(q)}
          onChange={() => setAnsweredQuestion(prev => [...prev, q])}
          value={q.id}/>
        } label={q.content} />
      </Stack>
    );

  const startRecordingCallback = async () => {
    try {
      const videoStream = await navigator.mediaDevices.getDisplayMedia({
        video:true,
        audio: true,
        preferCurrentTab: true
      });
      const recordedChunks: any[] = [];
      const recorder = new MediaRecorder(videoStream, {mimeType: "video/webm;codecs=h264,opus"});
      recorder.start(1000);
      recorder.ondataavailable = async (blobEvent) => {
        handleDataAvailable(blobEvent, recordedChunks);
        // const blob = await  blobEvent.data.arrayBuffer();
      };

      
      setIsRecording(!isRecording);
      console.log(videoStream);
    } catch {
      errorAlert("You need to share this tab in order to record it");
    }
    // const {displaySurface} = videoStream.getVideoTracks()[0].getSettings();
    // if (displaySurface != "window" || displaySurface != "monitor") {
      //   alert("SSSS")
      // }
    };

  function handleDataAvailable(event: any, recordedChunks: any[]) {
    console.log("data-available");
    if (event.data.size > 0) {
      recordedChunks.push(event.data);
      console.log(recordedChunks);
      download(recordedChunks);
    } else {
      // …
    }
  }
  async function download(recordedChunks: any[]) {
    const data = new Blob(recordedChunks, {
      type: "video/webm",
    });
    const reader = new FileReader(); 
    reader.readAsDataURL(data); 
    reader.onloadend = async function () { 
    const base64String = reader.result as string; 
    const withoutKey = base64String.substr(base64String.indexOf(',') + 1); 
    await sendData({
      meetingId,
      blob: withoutKey
    });
  } 
  }
    
  const [sideBarOpen, setSideBarOpen] = useState<boolean>(true);
  return (
    <Stack height='100%' boxShadow="-1px -1px 1px 1px black" sx={{transition: 'width 0.25s'}} 
      overflow="hidden" width={sideBarOpen ? "64px" : "300px"}>
        <Button onClick={() => setSideBarOpen(!sideBarOpen)}>
          {!sideBarOpen ? <MenuOpen/> : <Menu/>}
        </Button>
        {!sideBarOpen &&
          <Box padding={2}>
            <Typography mb={3} variant="h5">Questionnaire {questionnaire?.title}</Typography>
            <Stack rowGap="10px">
              {getQuestions(currentPage)}
            </Stack>
            {questions.length > maxPerPageQuestions &&
              <Stack flexDirection="row" justifyContent="center" columnGap={2}>
                <Button variant="contained" onClick={() => setCurrentPage(currentPage-1)} disabled={currentPage === 0}>
                  Prev
                </Button>
                <Button variant="contained" onClick={() => setCurrentPage(currentPage+1)} disabled={currentPage === Math.ceil(questions.length / maxPerPageQuestions) - 1}>
                  Next
                </Button>
              </Stack>
            }
            <Button fullWidth variant='contained' color={isRecording ? 'error' : 'primary'} onClick={startRecordingCallback}> {!isRecording ? 'Start Recording' : 'Stop Recording'} </Button>
          </Box>
        }
      </Stack>
  )
}