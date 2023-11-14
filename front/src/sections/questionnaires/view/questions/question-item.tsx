import React, { useState } from 'react';
import { useForm, Controller } from 'react-hook-form';

import { Save, Delete } from '@mui/icons-material';
import { Box, Stack, Button,  TextField } from '@mui/material';

import { updateQuestion } from 'src/api/question-api';
import { QuestionModelType } from 'src/models/question';

// ----------------------------------------------------------------------
export type QuestionPropType = {
  item: QuestionModelType,
  onDelete: (id: string) => void
};
export default function QuestionItem (props: QuestionPropType) {
  const {item, onDelete} = props;
  const [content, setContent] = useState<string>(item.content);
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const { control, handleSubmit } = useForm({
    mode: 'onChange'
  });

  const onSubmit = async (data: any) => {
    const question = item as QuestionModelType;
    question.content = data.content;
    await updateQuestion(question);
    setContent(data.content);
    setIsEditing(!isEditing);
  };

  return (
    <Box>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Stack flexDirection="row" columnGap={2}>
          {!isEditing ? 
          <span role="presentation" onClick={() => setIsEditing(!isEditing)} style={{width:'100%', padding:'16px'}} >{content}</span>:
          <Controller name="content"
                    control={control}
                    rules={
                      {required: 'Field is requeired'}
                    }
                    render={({
                      field: {onChange},
                    }) => <TextField 
                      type="text"
                      autoFocus
                      onChange={onChange}
                      onBlur={() => {setIsEditing(!isEditing);}}
                      fullWidth
                      defaultValue={content}
                      placeholder='Add new question...'
                      />}
                  />
          }
          {
            !isEditing ?
            <Button onClick={() => onDelete(item.id)} type="button" color='error' variant='text'>
              <Delete color='error' fontSize='large'/>
            </Button> :
            <Button type="submit" color='success' variant='text'>
              <Save color='success' fontSize='large'/>
            </Button>
          }
        </Stack>
      </form>
    </Box>
  );
}
