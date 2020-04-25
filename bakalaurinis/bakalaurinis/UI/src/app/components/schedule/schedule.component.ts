import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { GetActivities } from 'src/app/models/get-activities';
import { DatePipe } from '@angular/common';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ScheduleService } from 'src/app/services/schedule.service';
import { ActivitiesAfterUpdate } from 'src/app/models/activities-after-update';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
})
export class ScheduleComponent implements OnInit {
  busyness: number;
  constructor(
    private scheduleService: ScheduleService,
    private authService: AuthServiceService,
    private datePipe: DatePipe,
  ) { }

  activities: GetActivities[] = [];
  currentDate: Date;
  dateString: string;
  activitiesAfterUpdate: ActivitiesAfterUpdate = new ActivitiesAfterUpdate();

  ngOnInit() {
    this.currentDate = new Date();
    this.setCurrentDate();
    this.getAllUserActivities();
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.activities, event.previousIndex, event.currentIndex);
    this.activitiesAfterUpdate.activities = Object.assign([], this.activities);

    this.updateActivitiesTime();
  }

  getAllUserActivities() {
    this.scheduleService.getUserTodaysActivities(this.authService.getUserId(), this.dateString).subscribe(data => {
      this.activities = Object.assign([], data.works);
      this.busyness = data.busyness;
    });
  }

  setCurrentDate() {
    this.dateString = this.datePipe.transform(this.currentDate, 'yyyy-MM-dd');

    this.getAllUserActivities();
  }

  updateActivitiesTime() {
    this.scheduleService.updateActivities(this.authService.getUserId(), this.dateString, this.activitiesAfterUpdate).subscribe(() => {
      this.getAllUserActivities();
    });
  }

  updateDate(dayCount: number) {
    this.currentDate.setDate(this.currentDate.getDate() + dayCount);

    this.setCurrentDate();
  }

  getDataString(date: Date) {
    return this.datePipe.transform(date, 'HH:mm:ss');
  }
}
