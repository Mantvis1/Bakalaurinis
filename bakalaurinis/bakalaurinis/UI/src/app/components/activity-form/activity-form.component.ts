import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IActivityModal } from './activity-modal';

@Component({
  selector: 'app-activity-form',
  templateUrl: './activity-form.component.html',
  styleUrls: ['./activity-form.component.css']
})
export class ActivityFormComponent {

  constructor(
    public dialogRef: MatDialogRef<IActivityModal>,
    @Inject(MAT_DIALOG_DATA) public data: IActivityModal
  ) { }

  closeModal(returnValue: any) {
    console.log(this.data.activityFormData);
    this.dialogRef.close(returnValue);
  }

}
