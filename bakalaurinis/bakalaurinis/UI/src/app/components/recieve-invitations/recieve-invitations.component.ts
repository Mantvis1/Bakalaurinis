import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/app/models/invitation';
import { InvitationStatus } from 'src/app/models/invitation-status.enum';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { WorkReviewComponent } from '../work-review/work-review.component';

@Component({
  selector: 'app-recieve-invitations',
  templateUrl: './recieve-invitations.component.html',
  styleUrls: ['./recieve-invitations.component.css']
})
export class RecieveInvitationsComponent implements OnInit {
  isRowClick = true;
  invitations = new MatTableDataSource<Invitation>();
  displayedColumns: string[] = ["Row"];
  breakpoint: number = 7;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private readonly authenticationService: AuthenticationService,
    private readonly invitationService: InvitationsService,
    private readonly dialog: MatDialog
  ) { }

  ngOnInit() {
    this.getInvitations();
    this.onResize(window);
  }

  getInvitations() {
    this.invitationService.getInvitationsId(this.authenticationService.getUserId()).subscribe(
      data => {
        this.invitations.data = Object.assign([], data);
      }
    );
  }

  decline(id: number, workId: number) {
    this.updateRowClick(false);

    let invitation = new Invitation();
    invitation.id = id;
    invitation.invitationStatus = InvitationStatus.Declined;
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
    invitation.invitationStatus = InvitationStatus.Accepted;
    invitation.workId = workId;

    this.invitationService.updateInvitation(id, invitation).subscribe(
      () => {
        this.getInvitations();
        this.updateRowClick(true);
      }
    );
  }

  applyFilter(filterValue: string): void {
    this.invitations.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(rowId: number) {
    if (this.isRowClick) {
      this.dialog.open(WorkReviewComponent, {
        minWidth: "300px",
        width: "50%",
        data: {
          workId: rowId
        }
      });
    }
  }

  private updateRowClick(isRowCanBeClicked: boolean): void {
    this.isRowClick = isRowCanBeClicked;
  }

  onResize(event): void {
    if (event.innerWidth <= 1200 && event.innerWidth > 800) {
      this.breakpoint = 3;
    } else if (event.innerWidth <= 800 && event.innerWidth > 600) {
      this.breakpoint = 2;
    } else if (event.innerWidth > 1200) {
      this.breakpoint = 4;
    }
    else {
      this.breakpoint = 1;
    }
  }
}
