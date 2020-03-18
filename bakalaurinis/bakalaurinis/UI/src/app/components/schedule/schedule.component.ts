import { Component, OnInit } from '@angular/core';
import timeGridPlugin from '@fullcalendar/timegrid';
import { ActivityService } from 'src/app/services/activity.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { GetActivities } from 'src/app/models/get-activities';
import dayGridPlugin from '@fullcalendar/daygrid';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {
  constructor(
    private activityService: ActivityService,
    private authService: AuthServiceService
  ) { }

  activities: GetActivities[] = [];
  events: any[];
  options: any;
  viewDate: Date = new Date();

  ngOnInit() {
    this.getActivities();
    this.setOptions();
  }

  getActivities() {
    this.activityService.getUserActivityById(this.authService.getUserId()).subscribe(
      data => {
        console.log(data);
        this.events = [{
          "title": data.title,
          "start": data.startTime,
          "end": data.endTime
        }];
      }
    );
  }

  setOptions() {
    this.options = {
      plugins: [timeGridPlugin, dayGridPlugin],
      defaultDate: this.viewDate,
      header: {
        left: 'prev,next',
        center: 'title',
        right: 'timeGridWeek,dayGridWeek',
      },
      firstDay: 1,
      editable: true
    };
  }
}
