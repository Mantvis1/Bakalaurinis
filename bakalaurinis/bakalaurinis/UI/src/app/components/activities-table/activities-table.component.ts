import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivityService } from "../../services/activity.service";
import { AuthServiceService } from "../../services/auth-service.service";
import { GetActivities } from "../../models/get-activities";
import { MatDialog, MatPaginator, MatTableDataSource } from "@angular/material";
import { ActivityFormComponent } from "../activity-form/activity-form.component";
import { NewActivity } from "../../models/new-activity";
import { ActivityPriority } from "./activity-priority.enum";
import { InviteUserComponent } from '../invite-user/invite-user.component';
import { SettingsService } from 'src/app/services/settings.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { ActivityReviewComponent } from '../activity-review/activity-review.component';

@Component({
  selector: "app-activities-table",
  templateUrl: "./activities-table.component.html",
  styleUrls: ["./activities-table.component.css"]
})
export class ActivitiesTableComponent implements OnInit {
  activities = new MatTableDataSource<GetActivities>();
  isRowClick = true;

  displayedColumns: string[] = [
    "Title",
    "Priority",
    "Edit",
    "Invite",
    "Delete"
  ];

  activityToEdit: NewActivity = new NewActivity();
  newActivity: NewActivity = new NewActivity();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private activityService: ActivityService,
    private authService: AuthServiceService,
    private dialog: MatDialog,
    private settingsService: SettingsService
  ) { }

  ngOnInit() {
    this.getPageSize(this.authService.getUserId());
    this.refreshTable();
    this.activities.paginator = this.paginator;
  }

  deleteById(id: number) {
    this.updateRowClick(false);

    this.activityService.deleteActivity(id).subscribe(() => {
      this.refreshTable();
      this.updateRowClick(true);
    });
  }

  refreshTable() {
    this.activityService
      .getUserActivities(this.authService.getUserId()).subscribe(
        data => {
          console.log(data);
          this.activities.data = data;
        });
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    );
  }

  openCreateModal() {
    this.updateRowClick(false);

    const dialogRef = this.dialog.open(ActivityFormComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        formTitle: "New work",
        activityFormData: this.newActivity,
        formConfirmationButtonName: "Create"
      }
    });

    dialogRef.afterClosed().subscribe(newActivity => {
      if (newActivity) {
        newActivity.userId = this.authService.getUserId();
      }
      this.activityService.createNewActivity(newActivity).subscribe(
        () => {
          this.refreshTable();
          this.newActivity = new NewActivity();
          this.updateRowClick(true);
        },
        error => {
          console.log(error);
        }
      );
    });
  }

  editFrom(element: NewActivity) {
    this.updateRowClick(false);
    this.activityToEdit = Object.assign({}, element);

    const dialogRef = this.dialog.open(ActivityFormComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        formTitle: "Edit work",
        activityFormData: this.activityToEdit,
        formConfirmationButtonName: "Edit"
      }
    });

    dialogRef.afterClosed().subscribe(editActivity => {
      if (editActivity)
        this.activityService
          .editActivity(editActivity, editActivity.id)
          .subscribe(
            () => {
              this.refreshTable();
              this.updateRowClick(true);
            },
            error => {
              console.log(error);
            }
          );
    });
  }

  getActivityPriority(priorityId: number) {
    return ActivityPriority[priorityId];
  }

  invite(workId: number) {
    this.updateRowClick(false);

    const dialogRef = this.dialog.open(InviteUserComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        senderId: this.authService.getUserId(),
        workId: workId,
        receiverName: ''
      }
    });

    dialogRef.afterClosed().subscribe(() => { this.updateRowClick(true); })
  }

  applyFilter(filterValue: string): void {
    this.activities.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(row) {
    if (this.isRowClick) {
      this.dialog.open(ActivityReviewComponent, {
        minWidth: "250px",
        width: "35%",
        data: {
          activityId: row.id
        }
      });
    }
  }

  private updateRowClick(isRowCanBeClicked: boolean): void {
    this.isRowClick = isRowCanBeClicked;
  }

}
