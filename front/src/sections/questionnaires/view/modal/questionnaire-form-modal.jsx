import { useEffect } from "react";
import PropTypes  from "prop-types";
import {useForm, Controller} from 'react-hook-form';

import LoadingButton from '@mui/lab/LoadingButton';
import { Box, Modal, Stack, Button, TextField, Typography } from "@mui/material";

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


export default function QuestionnaireFormModal({open, handleClose, questionnaire}) {
    const isEdit = questionnaire !== null && questionnaire !== undefined;

    const { control, handleSubmit, formState: {isValid}, reset } = useForm({
      mode: 'onChange'
    });

    useEffect(() => {
      reset({title: questionnaire?.title ?? ''});
    }, [reset, questionnaire]);

    const onSubmit = data => console.log(data);

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
                    {required: 'error message', minLength: 5}
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
                  Add 
                </LoadingButton>
              </Stack>
            </form>
          </Box>
        </Box>
      </Modal>);
}

QuestionnaireFormModal.propTypes = {
  open: PropTypes.any,
  handleClose: PropTypes.func,
  questionnaire: PropTypes.any
};