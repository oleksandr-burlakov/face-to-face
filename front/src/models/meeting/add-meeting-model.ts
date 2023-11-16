export type AddMeetingModel = {
  recordLink?: string;
  participantsEmail: string;
  assignedTime?: Date;
  asslowConnectWithoutHost: boolean;
  maxAllowedParticipantsNumber?: number;
  saveChat : boolean;
  isFinished: boolean;
};