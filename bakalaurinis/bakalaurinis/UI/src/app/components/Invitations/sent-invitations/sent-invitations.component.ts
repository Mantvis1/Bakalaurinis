import { Component, OnInit } from '@angular/core';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { Invitation } from 'src/app/models/invitation';

@Component({
  selector: 'app-sent-invitations',
  templateUrl: './sent-invitations.component.html',
  styleUrls: ['./sent-invitations.component.css']
})
export class SentInvitationsComponent implements OnInit {

  invitations: Invitation[] = [];

  constructor(
    private authService: AuthServiceService,
    private invitationService: InvitationsService
  ) { }

  ngOnInit() {
    this.loadInvitations();
  }

  loadInvitations() {
    this.invitationService.getInvitationsId(this.authService.getUserId(), 'sender').subscribe(
      data => {
        this.invitations = Object.assign([], data);
      }
    )
  }

}
