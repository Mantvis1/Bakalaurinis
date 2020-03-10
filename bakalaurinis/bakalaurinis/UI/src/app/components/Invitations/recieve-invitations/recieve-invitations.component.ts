import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/app/models/invitation';
import { InvitationTypes } from 'src/app/models/invitation-types.enum';

@Component({
  selector: 'app-recieve-invitations',
  templateUrl: './recieve-invitations.component.html',
  styleUrls: ['./recieve-invitations.component.css']
})
export class RecieveInvitationsComponent implements OnInit {

  heroes = ['Windstorm', 'Bombasto', 'Magneta', 'Tornado'];
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

}
