import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IWorkViewModal } from '../receive-invitations/work-view-modal';
import { WorkService } from 'src/app/services/work.service';
import { String } from 'typescript-string-operations';
import { GetWork } from 'src/app/models/get-work';

@Component({
  selector: 'app-work-review',
  templateUrl: './work-review.component.html',
  styleUrls: ['./work-review.component.css']
})
export class WorkReviewComponent implements OnInit {
  work: GetWork = new GetWork();

  constructor(
    public dialogRef: MatDialogRef<IWorkViewModal>,
    @Inject(MAT_DIALOG_DATA) public data: IWorkViewModal,
    private workService: WorkService
  ) { }

  ngOnInit() {
    this.workService.getByUserId(this.data.workId).subscribe(
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
