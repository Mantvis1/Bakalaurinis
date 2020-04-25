import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/app/models/invitation';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { SettingsService } from 'src/app/services/settings.service';
import { ActivityReviewComponent } from '../activity-review/activity-review.component';

@Component({
  selector: 'app-recieve-invitations',
  templateUrl: './recieve-invitations.component.html',
  styleUrls: ['./recieve-invitations.component.css']
})
export class RecieveInvitationsComponent implements OnInit {
  isRowClick = true;
  invitations = new MatTableDataSource<Invitation>();
  displayedColumns: string[] = [
    "Row"
  ];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private readonly authService: AuthServiceService,
    private readonly invitationService: InvitationsService,
    private readonly settingsService: SettingsService,
    private readonly dialog: MatDialog
  ) { }

  ngOnInit() {
    this.getPageSize(this.authService.getUserId());
    this.getInvitations();
    this.invitations.paginator = this.paginator;
  }

  getInvitations() {
    this.invitationService.getInvitationsId(this.authService.getUserId()).subscribe(
      data => {
        this.invitations.data = Object.assign([], data);
      }
    );
  }

  decline(id: number, workId: number) {
    this.updateRowClick(false);

    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Atmestas;
    invitation.workId = workId;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.getInvitations();
        this.updateRowClick(true);
      }
    );
  }

  accept(id: number, workId: number) {
    this.updateRowClick(false);

    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Priimtas;
    invitation.workId = workId;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.getInvitations();
        this.updateRowClick(true);
      }
    );
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    );
  }

  applyFilter(filterValue: string): void {
    this.invitations.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(rowId: number) {
    if (this.isRowClick) {
      this.dialog.open(ActivityReviewComponent, {
        minWidth: "300px",
        width: "50%",
        data: {
          activityId: rowId
        }
      });
    }
  }

  private updateRowClick(isRowCanBeClicked: boolean): void {
    this.isRowClick = isRowCanBeClicked;
  }
}
