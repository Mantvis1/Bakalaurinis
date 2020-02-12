import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ActivityModal } from './activity-modal';

@Component({
  selector: 'app-activity-form',
  templateUrl: './activity-form.component.html',
  styleUrls: ['./activity-form.component.css']
})
export class ActivityFormComponent{

  constructor(
    public dialogRef: MatDialogRef<ActivityModal>,
    @Inject(MAT_DIALOG_DATA) public data: ActivityModal
  ) { }

  closeModal(returnValue: any) {
    this.dialogRef.close(returnValue);
  }

}
