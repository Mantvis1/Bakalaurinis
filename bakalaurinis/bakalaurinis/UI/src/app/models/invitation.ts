import { InvitationStatus } from './invitation-status.enum';

export class Invitation {
  id: number;
  message: string;
  invitationStatus: InvitationStatus;
}
