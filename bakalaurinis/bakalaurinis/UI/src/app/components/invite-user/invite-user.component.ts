import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatTableDataSource, MatPaginator } from '@angular/material';
import { InviteUserModal } from './invite-user-modal';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AlertService } from 'src/app/services/alert.service';
import { UserInvitation } from 'src/app/models/user-invitation';
import { UserInvitationService } from 'src/app/services/user-invitation.service';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { UserService } from 'src/app/services/user.service';
import { SettingsService } from 'src/app/services/settings.service';
import { ActivityService } from 'src/app/services/activity.service';
import { WorkStatusConfirmation } from 'src/app/models/work-status-confirmation';

@Component({
  selector: 'app-invite-user',
  templateUrl: './invite-user.component.html',
  styleUrls: ['./invite-user.component.css']
})
export class InviteUserComponent implements OnInit {

  userInvitations = new MatTableDataSource<UserInvitation>();
  displayedColumns: string[] = [
    "User",
    "Status"
  ];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  currentUserName: string;
  isConfirmed: boolean;


  constructor(
    public dialogRef: MatDialogRef<InviteUserModal>,
    @Inject(MAT_DIALOG_DATA) public data: InviteUserModal,
    private invitationService: InvitationsService,
    private alertService: AlertService,
    private userInvitationService: UserInvitationService,
    private userService: UserService,
    private authService: AuthServiceService,
    private settingsService: SettingsService,
    private workService: ActivityService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authService.getUserId());
    this.loadAllUserInvitations();
    this.getCurrentUser();
    this.getWorkStatus();

    this.userInvitations.paginator = this.paginator;
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
    this.userInvitationService.getAllByActivityId(this.data.workId).subscribe(
      data => {
        this.userInvitations.data = Object.assign([], data);
      }
    );
  }

  getStatus(index: number): string {
    return InvitationStatus[index];
  }

  isUserHaveInvitation(username: string): boolean {
    let result: boolean = false;
    this.userInvitations.data.forEach(element => {
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

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    )
  }

  confirmUserList() {
    let workStatusConfirmation = new WorkStatusConfirmation();
    workStatusConfirmation.id = this.data.workId;
    workStatusConfirmation.isInvitationsConfirmed = true;

    this.workService.updateWorkStatus(this.data.workId, workStatusConfirmation).subscribe(() => {
      this.getWorkStatus();
    });

  }

  getWorkStatus() {
    this.workService.getWorkStatus(this.data.workId).subscribe(data => {
      this.isConfirmed = data.isInvitationsConfirmed;
    })
  }

  isExistAnyInvitations(): boolean {
    if (this.userInvitations.data.length > 0) {
      return false;
    }

    return true;
  }

}
