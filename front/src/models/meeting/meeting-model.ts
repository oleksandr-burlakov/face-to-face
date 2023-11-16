export type MeetingModel = {
  id: string;
  recordLink?: string;
  participantsEmail: string;
  assignedTime?: Date;
  ownerId: string;
  asslowConnectWithoutHost: boolean;
  maxAllowedParticipantsNumber?: number;
  saveChat : boolean;
  isFinished: boolean;
};