export type AddMeetingModel = {
  title: string;
  participantsEmail: string;
  assignedTime?: Date;
  allowedConnectWithoutHost: boolean;
  maxAllowedParticipantsNumber?: number;
  saveChat : boolean;
  preferableQuestionnaireId?: string;
};