import { faker } from '@faker-js/faker';

// ----------------------------------------------------------------------

export const questionnaires = [...Array(24)].map(() => ({
  id: faker.string.uuid(),
  title: faker.company.name(),
}));
