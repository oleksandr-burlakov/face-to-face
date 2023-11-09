import React, { useEffect } from "react";
import {useForm, Controller} from 'react-hook-form';

import LoadingButton from '@mui/lab/LoadingButton';
import { Box, Modal, Stack, Button, TextField, Typography } from "@mui/material";

import { addQuestionnaire, updateQuestionnaire } from "src/api/questionnaire-api";
import { UpdateQuestionnaireModel } from "src/models/questionnaire/update-questionnaire-model";
import { AddQuestionnareModelType, GetMyQuestionnaireModelType } from "src/models/questionnaire";

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


export default function QuestionnaireFormModal({open, handleClose, questionnaire}: QuestionnaireFormModalPropTypes) {
    const isEdit = questionnaire !== null && questionnaire !== undefined;

    const { control, handleSubmit, formState: {isValid}, reset } = useForm({
      mode: 'onChange'
    });

    useEffect(() => {
      reset({title: questionnaire?.title ?? ''});
    }, [reset, questionnaire]);

    const onSubmit = async (data: any) => {
      if (isEdit) {
        const requestData = data as UpdateQuestionnaireModel;
        requestData.id = questionnaire.id;
        const result = await updateQuestionnaire(requestData);
        if (result.data.succeeded) {
          handleClose();
        }
      } else {
        const requestData = data as AddQuestionnareModelType;
        const result = await addQuestionnaire(requestData);
        if (result.data.succeeded) {
          handleClose();
        }
      }
    };

    return (
    <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2" justifyContent='space-between' display='flex'>
            {isEdit ? "Edit" : "Add new"} questionnaire
            <Button variant="text" onClick={handleClose}>X</Button>
          </Typography>
          <Box id="modal-modal-description" sx={{ mt: 2 }}>
            <form onSubmit={handleSubmit(onSubmit)}>
              <Stack spacing={3}>
                <Controller name="title"
                  rules={
                    {required: 'This field is required'}
                  }
                  control={control}
                  render={({
                    field: {onChange, value},
                    fieldState: {error},
                  }) => <TextField 
                    helperText={error ? error?.message : ''} 
                    error={!!error}
                    onChange={onChange}
                    value={value}
                    fullWidth
                    label="Title" />}
                 />
                <LoadingButton
                  fullWidth
                  size="large"
                  type="submit"
                  variant="contained"
                  color="inherit"
                  disabled={!isValid}
                >
                  { isEdit ? "Save" : "Add" }
                </LoadingButton>
              </Stack>
            </form>
          </Box>
        </Box>
      </Modal>);
}

export type QuestionnaireFormModalPropTypes = {
  open: any,
  handleClose: any,
  questionnaire: GetMyQuestionnaireModelType | null | undefined
};