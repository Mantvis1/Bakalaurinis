import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivityService } from "../../services/activity.service";
import { AuthServiceService } from "../../services/auth-service.service";
import { GetActivities } from "../../models/get-activities";
import { MatDialog, MatPaginator, MatTableDataSource } from "@angular/material";
import { ActivityFormComponent } from "../activity-form/activity-form.component";
import { NewActivity } from "../../models/new-activity";
import { ActivityPriority } from "./activity-priority.enum";
import { InvitationsService } from 'src/app/services/invitations.service';
import { NewInvitation } from 'src/app/models/new-invitation';
import { InviteUserComponent } from '../invite-user/invite-user.component';
import { SettingsService } from 'src/app/services/settings.service';

@Component({
  selector: "app-activities-table",
  templateUrl: "./activities-table.component.html",
  styleUrls: ["./activities-table.component.css"]
})
export class ActivitiesTableComponent implements OnInit {


  activities = new MatTableDataSource<GetActivities>();
  displayedColumns: string[] = [
    "Title",
    "Description",
    "Duration",
    "Priority",
    "Edit",
    "Delete",
    "Invite"
  ];
  activityToEdit: NewActivity = new NewActivity();
  newActivity: NewActivity = new NewActivity();
  pageSize = 0;

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
    this.activityService.deleteActivity(id).subscribe(() => {
      this.refreshTable();
    });
  }

  refreshTable() {
    this.activityService
      .getUserActivities(this.authService.getUserId())
      .subscribe(data => {
        this.activities.data = data;
      });
  }

  getPageSize(userId: number): void {
    this.settingsService.getItemsPerPageSettings(userId).subscribe(
      data => {
        this.paginator._changePageSize(data.itemsPerPage);
      }
    )
  }

  openCreateModal() {
    const dialogRef = this.dialog.open(ActivityFormComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        formTitle: "New activity",
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
        },
        error => {
          console.log(error);
        }
      );
    });
  }

  editFrom(element: NewActivity) {
    this.activityToEdit = Object.assign({}, element);
    const dialogRef = this.dialog.open(ActivityFormComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        formTitle: "Edit activity",
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

  isValueExists(value: any) {
    if (value === null) return "Nenustatyta";
    return value;
  }

  invite(workId: number) {
    const dialogRef = this.dialog.open(InviteUserComponent, {
      minWidth: "250px",
      width: "35%",
      data: {
        senderId: this.authService.getUserId(),
        workId: workId,
        receiverName: ''
      }
    });

  }
}
