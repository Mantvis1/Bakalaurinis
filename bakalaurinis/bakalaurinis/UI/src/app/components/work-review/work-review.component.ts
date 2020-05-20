import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IActivityViewModal as ActivityViewModal } from '../recieve-invitations/activity-view-modal';
import { WorkService } from 'src/app/services/work.service';
import { GetActivities } from 'src/app/models/get-activities';
import { String } from 'typescript-string-operations';

@Component({
  selector: 'app-work-review',
  templateUrl: './work-review.component.html',
  styleUrls: ['./work-review.component.css']
})
export class WorkReviewComponent implements OnInit {
  isDescriptionPanelOpen: false;
  work: GetActivities = new GetActivities();

  constructor(
    public dialogRef: MatDialogRef<ActivityViewModal>,
    @Inject(MAT_DIALOG_DATA) public data: ActivityViewModal,
    private workService: WorkService
  ) { }

  ngOnInit() {
    this.workService.getByUserId(this.data.activityId).subscribe(
      work => {
        this.work = Object.assign({}, work);
      }
    );
  }

  closeModal() {
    this.dialogRef.close();
  }

  getDurationHoursAndMinutes(): string {
    let timeInString = String.Empty;
    let hours = Math.floor(this.work.durationInMinutes / 60);
    let minutes = this.work.durationInMinutes - (hours * 60);

    timeInString = String.Format("{0} hours {1} minutes", hours, minutes);

    return timeInString;
  }
}
