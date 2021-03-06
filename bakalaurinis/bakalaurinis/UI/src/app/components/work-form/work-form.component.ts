import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IWorkModal } from './work-modal';

@Component({
  selector: 'app-work-form',
  templateUrl: './work-form.component.html',
  styleUrls: ['./work-form.component.css']
})
export class WorkFormComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<IWorkModal>,
    @Inject(MAT_DIALOG_DATA) public data: IWorkModal
  ) { }

  ngOnInit() {
    console.log(this.data);
  }

  closeModal(returnValue: any) {
    this.dialogRef.close(returnValue);
  }

}
