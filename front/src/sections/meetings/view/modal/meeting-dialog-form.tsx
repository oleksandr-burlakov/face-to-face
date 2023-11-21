import React, { useEffect, useState } from "react";
import { useForm, Controller } from "react-hook-form";

import { LoadingButton } from "@mui/lab";
import { Box, Modal, Stack, Button, Checkbox, TextField, Typography, FormControlLabel, Select, MenuItem, FormControl, InputLabel } from "@mui/material";

import { addMeeting, updateMeeting } from "src/api/meeting-api";
import { MeetingModel, AddMeetingModel, EditMeetingModel } from "src/models/meeting";
import { GetMyQuestionnaireModelType } from "src/models/questionnaire";
import { getMyQuestionnaires } from "src/api/questionnaire-api";

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  borderRadius: '20px',
  boxShadow: 24,
  p: 4,
};

export default function MeetingDialogForm({open, closeModal, meeting} : {open: boolean, closeModal: (state: boolean) => void, meeting?: MeetingModel | null}) {

  const [quesitonnaires, setQuestionnaires] = useState<GetMyQuestionnaireModelType[]>([]);

  const { control, setValue, handleSubmit, formState: {isValid}, reset } = useForm({
    mode: 'onChange',
  });

  useEffect(() => {
    reset();

    async function loadQuestionnairesList() {
      let result = await getMyQuestionnaires();
      if (result.data.succeeded) {
        setQuestionnaires(result.data.result);
      }
    }
    loadQuestionnairesList();

  }, [open, reset]);

  useEffect(() => {
    setValue("title", meeting?.title ?? '');
    setValue("participantsEmail", meeting?.participantsEmail ?? '');
    setValue("assignedTime", meeting?.assignedTime);
    setValue("maxAllowedParticipantsNumber", meeting?.maxAllowedParticipantsNumber ?? null);
    setValue("allowedConnectWithoutHost", meeting?.allowedConnectWithoutHost ?? false);
    setValue("saveChat", meeting?.saveChat ?? false);
    setValue("preferableQuestionnaireId", meeting?.preferableQuestionnaireId ?? '');

  }, [meeting, setValue])

  const onSubmit = async (data: any) => {
    console.log(data);
    if (meeting != null) {
      const request = data as EditMeetingModel;
      request.id = meeting?.id;
      const result = await updateMeeting(request);
      if (result.status === 200) {
        closeModal(true);
      }
    } else {
      const result = await addMeeting(data as AddMeetingModel);
      if (result.data.succeeded) {
        closeModal(true);
      }
    }
  };

  return (
    <Modal
        open={open}
        onClose={() =>closeModal(false)}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2" justifyContent='space-between' display='flex'>
            {meeting != null? "Edit" : "Add new"} questionnaire
            <Button variant="text" onClick={() => closeModal(false)}>X</Button>
          </Typography>
          <Box id="modal-modal-description" sx={{ mt: 2 }}>
            <form onSubmit={handleSubmit(onSubmit)}>
              <Stack spacing={3}>
              <Controller
                  name="title"
                  control={control}
                  rules={
                    {required: 'This field is required'}
                  }
                  render={({
                    field: {onChange, value},
                    fieldState: {error}
                  }) => <TextField 
                    type="text"
                    error={!!error}
                    helperText={error?.message}
                    onChange={onChange}
                    defaultValue={value}
                    fullWidth
                    label="Title*" />}
                 />
                 <Controller
                  name="participantsEmail"
                  control={control}
                  render={({
                    field: {onChange, value},
                  }) => <TextField 
                    type="text"
                    onChange={onChange}
                    defaultValue={value}
                    fullWidth
                    label="Participant emails" />}
                 />
                <Controller
                  name="assignedTime"
                  control={control}
                  render={({
                    field: {onChange, value},
                  }) => <TextField 
                    type="datetime-local"
                    onChange={onChange}
                    defaultValue={value}
                    fullWidth
                    InputLabelProps={{shrink:true}}
                    label="Assigned time" />}
                 />
                 <Controller 
                  name="maxAllowedParticipantsNumber"
                  control={control}
                  render={({
                    field: {onChange, value}
                  }) => <TextField 
                    type="number"
                    onChange={onChange}
                    defaultValue={value}
                    fullWidth
                    label="Maximum allowed participants"
                  />}
                 />
                 <Controller name="allowedConnectWithoutHost"
                  control={control}
                  render={({
                    field: {onChange, value}
                  }) => 
                    <FormControlLabel  control={<Checkbox onChange={onChange} checked={value ?? false}/>} label="Is allowed to connect without host"  />
                  }
                   />
                   <Controller name="saveChat"
                  control={control}
                  render={({
                    field: {onChange, value}
                  }) => 
                    <FormControlLabel  control={<Checkbox onChange={onChange} checked={value ?? false}/>} label="Save chat history after meeting?"  />
                  }
                   />
                   <Controller name="preferableQuestionnaireId"
                      control={control}
                      render={({
                        field: {onChange, value}
                      }) => 
                        <FormControl fullWidth>
                          <InputLabel id="preferable">Preferable quesitonnaire</InputLabel>
                          <Select value={value ?? ''} onChange={onChange} id="preferable" label="Preferable questionnaire">
                            <MenuItem>Without preferable</MenuItem>
                            {quesitonnaires.map((q,i) => (<MenuItem value={q.id}>{q.title}</MenuItem>))}
                          </Select>
                        </FormControl>
                      }
                   />
                <LoadingButton
                  fullWidth
                  size="large"
                  type="submit"
                  variant="contained"
                  color="inherit"
                  disabled={!isValid}
                >
                  { meeting != null ? "Save" : "Add" }
                </LoadingButton>
              </Stack>
            </form>
          </Box>
        </Box>
      </Modal>);
}