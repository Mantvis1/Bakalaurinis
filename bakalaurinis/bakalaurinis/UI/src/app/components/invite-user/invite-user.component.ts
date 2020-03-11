import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { InviteUserModal } from './invite-user-modal';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AlertService } from 'src/app/services/alert.service';
import { UserInvitation } from 'src/app/models/user-invitation';
import { UserInvitationService } from 'src/app/services/user-invitation.service';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';

@Component({
  selector: 'app-invite-user',
  templateUrl: './invite-user.component.html',
  styleUrls: ['./invite-user.component.css']
})
export class InviteUserComponent implements OnInit {

  userInvitations: UserInvitation[] = [];

  constructor(
    public dialogRef: MatDialogRef<InviteUserModal>,
    @Inject(MAT_DIALOG_DATA) public data: InviteUserModal,
    private invitationService: InvitationsService,
    private alertService: AlertService,
    private readonly userInvitationService: UserInvitationService
  ) { }

  ngOnInit() {
    this.loadAllUserInvitations();
  }

  invite() {
    let newInvitation = Object.assign({}, this.data);
    this.invitationService.createInvitation(newInvitation).subscribe(
      () => {
        this.alertService.showMessage("Pakvietimas išsiūstas");
        this.loadAllUserInvitations();
      },
      error => {
        this.alertService.showMessage("Vartotojas neegzistuoja");
        console.log(error);
      }
    )
  }

  loadAllUserInvitations() {
    this.userInvitationService.getAllByActivityId(this.data.activityId).subscribe(
      data => {
        this.userInvitations = Object.assign([], data);
      }
    );
  }

  getStatus(index: number): string {
    return InvitationStatus[index];
  }

}
