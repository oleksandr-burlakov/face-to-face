import { Helmet } from 'react-helmet-async';

import QuestionView from 'src/sections/questionnaires/view/questions/questions-view';

// ----------------------------------------------------------------------

export default function QuestionsPage() {
  return (
    <>
      <Helmet>
        <title> Questions | Minimal UI </title>
      </Helmet>

      <QuestionView/>
    </>
  );
}
