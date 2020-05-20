import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IActivityViewModal as ActivityViewModal } from '../recieve-invitations/activity-view-modal';
import { ActivityService } from 'src/app/services/activity.service';
import { GetActivities } from 'src/app/models/get-activities';
import { String } from 'typescript-string-operations';

@Component({
  selector: 'app-work-review',
  templateUrl: './work-review.component.html',
  styleUrls: ['./work-review.component.css']
})
export class WorkReviewComponent implements OnInit {
  descriptionPanelOpenState: false;
  activity: GetActivities = new GetActivities();

  constructor(
    public dialogRef: MatDialogRef<ActivityViewModal>,
    @Inject(MAT_DIALOG_DATA) public data: ActivityViewModal,
    private activityService: ActivityService
  ) { }

  ngOnInit() {
    this.activityService.getUserActivityById(this.data.activityId).subscribe(
      activity => {
        this.activity = Object.assign({}, activity);
      }
    );
  }

  closeModal() {
    this.dialogRef.close();
  }

  getDurationHoursAndMinutes(): string {
    let timeInString = String.Empty;
    let hours = Math.floor(this.activity.durationInMinutes / 60);
    let minutes = this.activity.durationInMinutes - (hours * 60);

    timeInString = String.Format("{0} hours {1} minutes", hours, minutes);

    return timeInString;
  }
}
