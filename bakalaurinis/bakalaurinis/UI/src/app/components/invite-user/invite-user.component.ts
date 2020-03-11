import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { InviteUserModal } from './invite-user-modal';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AlertService } from 'src/app/services/alert.service';
import { UserInvitation } from 'src/app/models/user-invitation';
import { UserInvitationService } from 'src/app/services/user-invitation.service';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-invite-user',
  templateUrl: './invite-user.component.html',
  styleUrls: ['./invite-user.component.css']
})
export class InviteUserComponent implements OnInit {

  userInvitations: UserInvitation[] = [];
  currentUserName: string;

  constructor(
    public dialogRef: MatDialogRef<InviteUserModal>,
    @Inject(MAT_DIALOG_DATA) public data: InviteUserModal,
    private invitationService: InvitationsService,
    private alertService: AlertService,
    private readonly userInvitationService: UserInvitationService,
    private readonly userService: UserService,
    private readonly authService: AuthServiceService
  ) { }

  ngOnInit() {
    this.loadAllUserInvitations();
    this.getCurrentUser();
  }

  invite() {
    let newInvitation = Object.assign({}, this.data);
    if (!this.isReceiverSameUserAsSender(newInvitation.receiverName)) {
      if (!this.isUserHaveInvitation(newInvitation.receiverName)) {
        this.invitationService.createInvitation(newInvitation).subscribe(
          () => {
            this.alertService.showMessage("Pakvietimas išsiųstas");
            this.loadAllUserInvitations();
          },
          error => {
            this.alertService.showMessage("Vartotojas neegzistuoja");
            console.log(error);
          }
        )
      }
      else {
        this.alertService.showMessage("Vartotojas jau turi pakvietimą");
      }
    } else {
      this.alertService.showMessage("Negalite siųsti pakvietimo sau");
    }
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

  isUserHaveInvitation(username: string): boolean {
    let result: boolean = false;
    this.userInvitations.forEach(element => {
      if (element.username == username) {
        result = true;
      }
    });

    return result;
  }

  isReceiverSameUserAsSender(receiverName: string) {
    if (this.currentUserName === receiverName) {
      return true;
    }

    return false;
  }

  getCurrentUser() {
    this.userService.getUsername(this.authService.getUserId()).subscribe(name => {
      this.currentUserName = name.username;
    });
  }

  closeModal() {
    this.dialogRef.close();
  }

}
