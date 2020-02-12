import { Component, OnInit } from "@angular/core";
import { ActivityService } from '../../services/activity.service';
import { AuthServiceService } from '../../services/auth-service.service';
import { GetActivities } from '../../models/get-activities';
import { MatDialog } from '@angular/material';
import { ActivityFormComponent } from '../activity-form/activity-form.component';
import { NewActivity } from '../../models/new-activity';

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
  displayedColumns: string[] = ['Title', 'Description', 'Delete', 'Edit'];

  ngOnInit() {
    this.refreshTable();
  }

  deleteById(id: number) {
    this.activityService.deleteActivity(id).subscribe(error => {
      console.log(error);
    });

    this.refreshTable();
  }

  refreshTable() {
    this.activityService.getUserActivities(this.authService.getUserId()).subscribe(data => {
      this.activities = data;
    });
  }

  openCreateModal() {
    const dialogRef = this.dialog.open(ActivityFormComponent, {
      minWidth: '200px',
      width: '50%',
      data: {
        formTitle: 'New activity',
        activityFormData: null,
        formConfirmationButtonName: 'Create'
      }
    });
  }

  editFrom(element: NewActivity) {
    const dialogRef = this.dialog.open(ActivityFormComponent, {
      minWidth: '200px',
      width: '50%',
      data: {
        formTitle: 'Edit activity',
        activityFormData: null,
        formConfirmationButtonName: 'Edit'
      }
    });
  }
}
