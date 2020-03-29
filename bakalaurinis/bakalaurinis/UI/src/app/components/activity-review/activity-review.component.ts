import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ActivityViewModal } from '../recieve-invitations/activity-view-modal';
import { ActivityService } from 'src/app/services/activity.service';
import { GetActivities } from 'src/app/models/get-activities';

@Component({
  selector: 'app-activity-review',
  templateUrl: './activity-review.component.html',
  styleUrls: ['./activity-review.component.css']
})
export class ActivityReviewComponent implements OnInit {

  activity: GetActivities = new GetActivities();
  author: string = '';

  constructor(
    public dialogRef: MatDialogRef<ActivityViewModal>,
    @Inject(MAT_DIALOG_DATA) public data: ActivityViewModal,
    private activityService: ActivityService
  ) { }

  ngOnInit() {
    this.activityService.getUserActivityById(this.data.activityId).subscribe(
      activity => {
        console.log(activity);
        this.activity = Object.assign({}, activity);
      }
    );
  }

  closeModal() {
    this.dialogRef.close();
  }

}
