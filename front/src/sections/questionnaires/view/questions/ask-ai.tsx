import { useForm, Controller } from "react-hook-form";
import React, { useRef, useState, useEffect } from "react";

import { Send, Sync } from "@mui/icons-material";
import { Box, Stack, Button, TextField, Typography } from "@mui/material";

import AIConnector  from 'src/hooks/signalr-ai-connector';

export default function AskAi () {
  const { control, handleSubmit, formState: {isValid} } = useForm({
    mode: 'onChange'
  });

  const [isFinished, setIsFinished] = useState<boolean>(true);
  const [response, setResponse] = useState<string[]>([]);
  const [previousQuestion, setPreviousQuestion] = useState<string>('');
  const initialized = useRef(false)

  const onSubmit = async (data: any, e: any) => {
    setResponse([]);
    setIsFinished(false);
    setPreviousQuestion(data.content);
    await generateResposne(data.content);
    e.target.reset();
  };
  
  const appendResponse = (data: string) => {
    if (data.trim() === '>') {
      setIsFinished(true);
    } else {
      setResponse(x => [...x, data]);
    }
  }

  useEffect(() => {
    if (!initialized.current) {
      initialized.current = true;
      AIConnector({
        onGenerateResponse: (data) => {
          appendResponse(data);
        }
      }, true);
    }
  });

  const { generateResposne } = AIConnector({ onGenerateResponse: () => {}});

  return (
    <Box padding={4}>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Stack direction="row">
          <Controller name="content"
                    control={control}
                    rules={
                      {required: 'Field is requeired'}
                    }
                    render={({
                      field: {onChange},
                    }) => <TextField 
                    type="text"
                    multiline
                    onChange={onChange}
                    fullWidth
                    placeholder='Ask question'
                    />}
                  />
          <Button disabled={!isFinished} type="submit" variant="text">
            {isFinished && <Send/>}
            {!isFinished && <Sync className="autorotate"/>}
          </Button>
        </Stack>
      </form>
      <Box mt={3}>
        <b>Generated response: </b>
        {previousQuestion && 
          <Typography>
            <>Question: <i>{previousQuestion}</i></>
          </Typography>
        }
        <Typography className="text-wrap" component="pre">
          {response.length > 0 && response.map(i => i)}
        </Typography>
      </Box>
    </Box>
  );
}
