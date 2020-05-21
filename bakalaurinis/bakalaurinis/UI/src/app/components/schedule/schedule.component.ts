import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GetActivities } from 'src/app/models/get-activities';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ScheduleService } from 'src/app/services/schedule.service';
import { ActivitiesAfterUpdate } from 'src/app/models/activities-after-update';
import { ScheduleInfoComponent } from '../schedule-info/schedule-info.component';
import { MatDialog } from '@angular/material/dialog';
import { ConvertToStringService } from 'src/app/services/convert-to-string.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
})

export class ScheduleComponent implements OnInit {
  busyness: number;
  startTime: number;
  endTime: number;

  constructor(
    private scheduleService: ScheduleService,
    private authenticationService: AuthenticationService,
    private dialog: MatDialog,
    public convertToStringService: ConvertToStringService
  ) { }

  activities: GetActivities[] = [];
  currentDate: Date = new Date();
  dateString: string;
  activitiesAfterUpdate: ActivitiesAfterUpdate = new ActivitiesAfterUpdate();

  ngOnInit() {
    this.setCurrentDate();
    this.getAllUserActivities();
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.activities, event.previousIndex, event.currentIndex);
    this.activitiesAfterUpdate.activities = Object.assign([], this.activities);

    this.updateActivitiesTime();
  }

  getAllUserActivities() {
    this.scheduleService.getTodaysWorks(this.authenticationService.getUserId(), this.dateString).subscribe(data => {
      this.activities = Object.assign([], data.works);
      this.busyness = data.busyness;
      this.startTime = data.startTime;
      this.endTime = data.endTime;
    });
  }

  setCurrentDate() {
    this.dateString = this.convertToStringService.getFullDate(this.currentDate);
    this.getAllUserActivities();
  }

  updateActivitiesTime() {
    this.scheduleService.updateSchedule(this.authenticationService.getUserId(), this.dateString, this.activitiesAfterUpdate).subscribe(() => {
      this.getAllUserActivities();
    });
  }

  updateDate(dayCount: number) {
    this.currentDate.setDate(this.currentDate.getDate() + dayCount);
    this.setCurrentDate();
  }

  openInfoModal() {
    this.dialog.open(ScheduleInfoComponent, {
      minWidth: "300px",
      width: "35%",
      data: {
        date: this.dateString,
        startTime: this.startTime,
        endTime: this.endTime,
        worksCount: this.activities.length,
        busyness: this.busyness
      }
    });
  }
}
