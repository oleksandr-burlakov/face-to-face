import React from 'react';
import { useForm, Controller } from 'react-hook-form';

import { Add } from '@mui/icons-material';
import { Box,  Stack, Button, TextField } from '@mui/material';

import { AddQuestionModelType } from 'src/models/question';

// ----------------------------------------------------------------------
export type AddQuestionItemPropType = {
  questionnaireId: string,
  addQuestion: (model: AddQuestionModelType) => void
}
export default function AddQuestionItem (props: AddQuestionItemPropType) {
  const {questionnaireId, addQuestion} = props;
  const { control, handleSubmit, formState: {isValid} } = useForm({
    mode: 'onChange'
  });

  const onSubmit = async (data: any, e: any) => {
    const questionModel = data as AddQuestionModelType;
    questionModel.questionnaireId = questionnaireId;
    addQuestion(questionModel);
    e.target.reset();
  };

  return (
    <Box>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Stack flexDirection="row" columnGap={2}>
        <Controller name="content"
                  control={control}
                  rules={
                    {required: 'Field is requeired'}
                  }
                  render={({
                    field: {onChange},
                  }) => <TextField 
                    type="text"
                    onChange={onChange}
                    fullWidth
                    placeholder='Add new question...'
                    />}
                 />
        <Button disabled={!isValid} type="submit" variant='outlined'>
          <Add fontSize='large'/>
        </Button>
        </Stack>
      </form>
    </Box>
  );
}
