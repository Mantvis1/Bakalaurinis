import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthServiceService } from '../../services/auth-service.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { GetActivities } from '../../models/get-activities';
import { ScheduleService } from '../../services/schedule.service';
import { ScheduleGenerationService } from '../../services/schedule-generation.service';
import { ActivityService } from '../../services/activity.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  isLoadSchedule = 1;
  activities: GetActivities[] = [];
  currentUserId: number = 0;

  constructor(
    private userService: UserService,
    private authService: AuthServiceService,
    private scheduleService: ScheduleService,
    private scheduleGenerationService: ScheduleGenerationService,
    private activityService: ActivityService
  ) { }

  ngOnInit() {
    this.currentUserId = this.authService.getUserId();

    this.genereteScheduleIfNotExsits();
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
  }

  getAllUserActivities() {
    this.scheduleService.getUserTodaysActivities(this.authService.getUserId()).subscribe(data => {
      this.activities = data;
    });
  }

  genereteScheduleIfNotExsits(): void {
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
