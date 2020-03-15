import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/app/models/invitation';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { SettingsService } from 'src/app/services/settings.service';

@Component({
  selector: 'app-recieve-invitations',
  templateUrl: './recieve-invitations.component.html',
  styleUrls: ['./recieve-invitations.component.css']
})
export class RecieveInvitationsComponent implements OnInit {

  invitations = new MatTableDataSource<Invitation>();
  displayedColumns: string[] = [
    "Row"
  ];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private authService: AuthServiceService,
    private invitationService: InvitationsService,
    private settingsService: SettingsService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authService.getUserId());
    this.getInvitations();
    this.invitations.paginator = this.paginator;
  }

  getInvitations() {
    this.invitationService.getInvitationsId(this.authService.getUserId(), 'receiver').subscribe(
      data => {
        this.invitations.data = Object.assign([], data);
      }
    )
  }

  decline(id: number) {
    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Atmestas;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.getInvitations();
      }
    );
  }

  accept(id: number) {
    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Priimtas;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.getInvitations();
      }
    );
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    )
  }
}
