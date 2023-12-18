import {useRecoilState} from 'recoil';
import { useParams } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';

import { activeQuestionnaireAtom } from 'src/state';
import { QuestionModelType, AddQuestionModelType } from 'src/models/question';
import { addQuestion, deleteQuestion, getByQuestionnaire } from 'src/api/question-api';

import AskAi from './ask-ai';
import QuestionItem from './question-item';
import AddQuestionItem from './add-question-item';

// ----------------------------------------------------------------------

export default function QuestionsView() {
  const {id} = useParams();
  const [activeQuestionnaire] = useRecoilState(activeQuestionnaireAtom);
  const [questions, setQuestions] = useState<QuestionModelType[]>([]);

  useEffect(() => {
      if (id) {
        getByQuestionnaire(id).then((x) => {
          setQuestions(x.data.result);
        });
      }
  },[id, setQuestions]);

  const removeQuestion = async (localId: string) => {
    await deleteQuestion(localId);
    const localQuestions = questions.filter(q => q.id !== localId);
    setQuestions(localQuestions);
  };

  const insertQuestion = async (questionModel: AddQuestionModelType) => {
    const addResponse = await addQuestion(questionModel);
    if (addResponse.data.succeeded) {
      const newQuestion = questionModel as QuestionModelType;
      newQuestion.id = addResponse.data.result;
      setQuestions([...questions, newQuestion]);
    }
  }

  return (
    <Container maxWidth={false}>
      <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
        <Typography variant="h4">{activeQuestionnaire ? `"${activeQuestionnaire.title}"` : ""} Questions</Typography>
      </Stack>
      <Stack direction='row' columnGap={5} justifyContent='center'>
        <Card sx={{"minWidth": "70%"}}>
          <Stack padding={4} rowGap={2}>
            <Typography marginBottom={2} variant='h4'>List of questions</Typography>
            { id && <AddQuestionItem addQuestion={insertQuestion} questionnaireId={id} />}
            {
              questions.map((x) => <QuestionItem onDelete={removeQuestion} item={x} key={x.id} />)
            }
          </Stack>
        </Card>
        <Card sx={{'minWidth': '30%'}}>
          <AskAi />
        </Card>
      </Stack>
    </Container>
  );
}
