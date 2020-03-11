import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/app/models/invitation';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';

@Component({
  selector: 'app-recieve-invitations',
  templateUrl: './recieve-invitations.component.html',
  styleUrls: ['./recieve-invitations.component.css']
})
export class RecieveInvitationsComponent implements OnInit {

  invitations: Invitation[] = [];

  constructor(
    private authService: AuthServiceService,
    private invitationService: InvitationsService
  ) { }

  ngOnInit() {
    this.loadInvitations();
  }

  loadInvitations() {
    this.invitationService.getInvitationsId(this.authService.getUserId(), 'receiver').subscribe(
      data => {
        this.invitations = Object.assign([], data);
      }
    )
  }

  decline(id: number) {
    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Atmestas;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.loadInvitations();
      }
    );
  }

  accept(id: number) {
    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Priimtas;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.loadInvitations();
      }
    );
  }
}
