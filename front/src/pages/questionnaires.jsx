import { Helmet } from 'react-helmet-async';

import QuestionnaireView from 'src/sections/questionnaires/view/questionnaire-view';

// ----------------------------------------------------------------------

export default function QuestionnairePage() {
  return (
    <>
      <Helmet>
        <title> User | Minimal UI </title>
      </Helmet>

      <QuestionnaireView />
    </>
  );
}
