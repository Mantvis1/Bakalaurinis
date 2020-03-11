import { InvitationStatus } from './invitation-status.enum';

export class UserInvitation {
  receiverId: number;
  username: string;
  invitationStatus: InvitationStatus
}
