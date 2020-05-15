import { Component, OnInit, Inject } from '@angular/core';
import { IScheduleInfoReview } from './schedule-info-review';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-schedule-info',
  templateUrl: './schedule-info.component.html',
  styleUrls: ['./schedule-info.component.css']
})
export class ScheduleInfoComponent {

  constructor(
    public dialogRef: MatDialogRef<IScheduleInfoReview>,
    @Inject(MAT_DIALOG_DATA) public data: IScheduleInfoReview
  ) { }

  closeModal() {
    this.dialogRef.close();
  }

}
