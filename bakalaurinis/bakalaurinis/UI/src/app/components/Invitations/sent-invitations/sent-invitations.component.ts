import { Component, OnInit, ViewChild } from '@angular/core';
import { InvitationsService } from 'src/app/services/invitations.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { Invitation } from 'src/app/models/invitation';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';
import { ActivityPriority } from '../../activities-table/activity-priority.enum';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { SettingsService } from 'src/app/services/settings.service';

@Component({
  selector: 'app-sent-invitations',
  templateUrl: './sent-invitations.component.html',
  styleUrls: ['./sent-invitations.component.css']
})
export class SentInvitationsComponent implements OnInit {

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
    this.invitationService.getInvitationsId(this.authService.getUserId(), 'sender').subscribe(
      data => {
        this.invitations.data = Object.assign([], data);
      }
    )
  }

  getStatus(index: number): string {
    return InvitationStatus[index];
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    )
  }
}
