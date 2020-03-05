import { Component, OnInit } from "@angular/core";
import { ActivityService } from "../../services/activity.service";
import { AuthServiceService } from "../../services/auth-service.service";
import { GetActivities } from "../../models/get-activities";
import { MatDialog } from "@angular/material";
import { ActivityFormComponent } from "../activity-form/activity-form.component";
import { NewActivity } from "../../models/new-activity";
import { ActivityPriority } from "./activity-priority.enum";

@Component({
  selector: "app-activities-table",
  templateUrl: "./activities-table.component.html",
  styleUrls: ["./activities-table.component.css"]
})
export class ActivitiesTableComponent implements OnInit {
  constructor(
    private activityService: ActivityService,
    private authService: AuthServiceService,
    private dialog: MatDialog
  ) { }

  activities: GetActivities[] = [];
  displayedColumns: string[] = [
    "Title",
    "Description",
    "Duration",
    "Priority",
    "Edit",
    "Delete"
  ];
  activityToEdit: NewActivity = new NewActivity();
  newActivity: NewActivity = new NewActivity();

  ngOnInit() {
    this.refreshTable();
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
        this.activities = data;
      });
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
}
