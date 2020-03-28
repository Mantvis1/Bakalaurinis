import { InvitationStatus } from './invitation-status.enum';

export class NewUserInvitation {
  receiverId: number;
  username: string;
  invitationStatus: InvitationStatus
}
