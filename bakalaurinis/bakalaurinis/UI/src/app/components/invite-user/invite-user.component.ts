import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatTableDataSource, MatPaginator } from '@angular/material';
import { InviteUserModal } from './invite-user-modal';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AlertService } from 'src/app/services/alert.service';
import { UserInvitationService } from 'src/app/services/user-invitation.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';
import { SettingsService } from 'src/app/services/settings.service';
import { UserInvitation } from 'src/app/models/user-invitation';
import { ConvertToStringService } from 'src/app/services/convert-to-string.service';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'app-invite-user',
  templateUrl: './invite-user.component.html',
  styleUrls: ['./invite-user.component.css']
})

export class InviteUserComponent implements OnInit {
  currentUserName: string;
  userInvitations = new MatTableDataSource<UserInvitation>();

  displayedColumns: string[] = [
    "User",
    "Status",
    "Withdraw"
  ];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    public dialogRef: MatDialogRef<InviteUserModal>,
    @Inject(MAT_DIALOG_DATA) public data: InviteUserModal,
    private invitationService: InvitationsService,
    private alertService: AlertService,
    private userInvitationService: UserInvitationService,
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private settingsService: SettingsService,
    public convertToStringService: ConvertToStringService,
    private filterService: FilterService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authenticationService.getUserId());
    this.loadAllUserInvitations();
    this.getCurrentUser();
  }

  invite() {
    let newInvitation = Object.assign({}, this.data);

    if (newInvitation.receiverName.length !== 0) {
      if (!this.isReceiverSameUserAsSender(newInvitation.receiverName)) {
        if (!this.isUserHaveInvitation(newInvitation.receiverName)) {
          this.invitationService.createInvitation(newInvitation).subscribe(
            () => {
              this.alertService.showMessage("Invitation sent");
              this.loadAllUserInvitations();
            },
            () => {
              this.alertService.showMessage("User does not exists");
            }
          );
        }
        else {
          this.alertService.showMessage("User already got invitation");
        }
      } else {
        this.alertService.showMessage("You cant send invitation to yourself");
      }
    } else {
      this.alertService.showMessage("Receiver name can not be empty");
    }
  }

  loadAllUserInvitations() {
    this.userInvitationService.getAllByActivityId(this.data.workId).subscribe(
      data => {
        this.userInvitations.data = Object.assign([], data);
        this.userInvitations.paginator = this.paginator;
        this.userInvitations.filterPredicate = this.filterTable;

        this.userInvitations.data.forEach(invitation => {
          invitation.status = this.convertToStringService.getInvitationStatusByIndex(invitation.invitationStatus);
        });
      }
    );
  }

  isUserHaveInvitation(username: string): boolean {
    let result: boolean = false;

    this.userInvitations.data.forEach(element => {
      if (element.username === username) {
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
    this.userService.getUsername(this.authenticationService.getUserId()).subscribe(name => {
      this.currentUserName = name.username;
    });
  }

  closeModal() {
    this.dialogRef.close();
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSetting(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    );
  }

  isExistAnyInvitations(): boolean {
    if (this.userInvitations.data.length > 0) {
      return false;
    }

    return true;
  }

  deleteInvitation(id: any): void {
    if (confirm("Are sure about withdrawing current invitation?")) {
      this.invitationService.delete(id).subscribe(
        () => {
          this.loadAllUserInvitations();
        }
      );
    }
  }

  applyFilter(value: string): void {
    this.userInvitations.filter = this.filterService.getFilteredValue(value);
  }

  private filterTable(invitation: UserInvitation, filterText: string): boolean {
    return (invitation.username && invitation.username.toLowerCase().indexOf(filterText) >= 0) ||
      (invitation.status && invitation.status.toLowerCase().indexOf(filterText) >= 0);
  }

}
