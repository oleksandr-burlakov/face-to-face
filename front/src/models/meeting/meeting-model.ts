export type MeetingModel = {
  id: string;
  title: string;
  recordLink?: string;
  participantsEmail: string;
  assignedTime?: Date;
  ownerId: string;
  allowedConnectWithoutHost: boolean;
  maxAllowedParticipantsNumber?: number;
  saveChat : boolean;
  isFinished: boolean;
  preferableQuestionnaireId?: string;
};