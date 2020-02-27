import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthServiceService } from '../../services/auth-service.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { GetActivities } from '../../models/get-activities';
import { ActivityService } from '../../services/activity.service';
import { ScheduleService } from '../../services/schedule.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  isLoadSchedule = 1;
  activities: GetActivities[] = [];

  constructor(
    private userService: UserService,
    private authService: AuthServiceService,
    private activityService: ActivityService,
    private scheduleService: ScheduleService
  ) { }

  ngOnInit() {
    this.isScheduleExists();
    this.getAllUserActivities();
  }

  isScheduleExists() {
    let currentUserId = this.authService.getUserId();

    this.userService.getStatusById(currentUserId).subscribe(data => {
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
}
