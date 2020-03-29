import { InvitationStatus } from './invitation-status.enum';

export class Invitation {
  id: number;
  workId: number;
  message: string;
  invitationStatus: InvitationStatus;
}
