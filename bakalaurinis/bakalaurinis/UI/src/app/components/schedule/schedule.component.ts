import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthServiceService } from '../../services/auth-service.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { GetActivities } from '../../models/get-activities';
import { ScheduleService } from '../../services/schedule.service';
import { ScheduleGenerationService } from '../../services/schedule-generation.service';
import { ActivityService } from '../../services/activity.service';
import { ActivitiesAfterUpdate } from 'src/app/models/activities-after-update';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  isLoadSchedule = 1;
  activities: GetActivities[] = [];
  currentUserId: number = 0;
  activitiesAfterUpdate: ActivitiesAfterUpdate = new ActivitiesAfterUpdate();

  constructor(
    private userService: UserService,
    private authService: AuthServiceService,
    private scheduleService: ScheduleService,
    private scheduleGenerationService: ScheduleGenerationService,
    private activityService: ActivityService
  ) { }

  ngOnInit() {
    this.currentUserId = this.authService.getUserId();

    this.generateScheduleIfNotExists();
    this.isScheduleExists();
    this.getAllUserActivities();
  }

  isScheduleExists() {
    this.userService.getStatusById(this.currentUserId).subscribe(data => {
      this.isLoadSchedule = data.scheduleStatus;
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.activities, event.previousIndex, event.currentIndex);
    this.activitiesAfterUpdate.activities = Object.assign([], this.activities);

    this.updateActivitiesTime();
  }

  updateActivitiesTime() {
    this.scheduleService.updateActivities(this.currentUserId, this.activitiesAfterUpdate).subscribe(() => {
      this.getAllUserActivities();
    });
  }

  getAllUserActivities() {
    this.scheduleService.getUserTodaysActivities(this.authService.getUserId()).subscribe(data => {
      this.activities = data;
    });
  }

  generateScheduleIfNotExists(): void {
    this.scheduleGenerationService.generateScheduleForUser(this.currentUserId).subscribe();
  }

  extend(id: number) {
    this.activityService.extendActivity(this.currentUserId, id).subscribe(() => {
      this.getAllUserActivities();
    });
  }

  finish(id: number) {
    this.activityService.finishActivity(this.currentUserId, id).subscribe(() => {
      this.getAllUserActivities();
    });
  }

}
