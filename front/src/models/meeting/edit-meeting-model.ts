export type EditMeetingModel = {
  id: string;
  recordLink?: string;
  participantsEmail: string;
  assignedTime?: Date;
  asslowConnectWithoutHost: boolean;
  maxAllowedParticipantsNumber?: number;
  saveChat : boolean;
  isFinished: boolean;
};