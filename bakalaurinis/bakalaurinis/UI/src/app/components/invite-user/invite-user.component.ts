import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { InviteUserModal } from './invite-user-modal';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-invite-user',
  templateUrl: './invite-user.component.html',
  styleUrls: ['./invite-user.component.css']
})
export class InviteUserComponent implements OnInit {

  heroes = ['Windstorm', 'Bombasto', 'Magneta', 'Tornado'];

  constructor(
    public dialogRef: MatDialogRef<InviteUserModal>,
    @Inject(MAT_DIALOG_DATA) public data: InviteUserModal,
    private invitationService: InvitationsService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
  }

  invite() {
    let newInvitation = Object.assign({}, this.data);
    this.invitationService.createInvitation(newInvitation).subscribe(
      () => {
        this.alertService.showMessage("Pakvietimas išsiūstas");
      },
      error => {
        this.alertService.showMessage("Vartotojas neegzistuoja");
        console.log(error);
      }
    )
  }

}
