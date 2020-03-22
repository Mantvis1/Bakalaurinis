import { Component, OnInit } from '@angular/core';
import timeGridPlugin from '@fullcalendar/timegrid';
import { ActivityService } from 'src/app/services/activity.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { GetActivities } from 'src/app/models/get-activities';
import dayGridPlugin from '@fullcalendar/daygrid';
import { ActivityView } from 'src/app/models/activity-view';

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
  activityView: ActivityView[] = [];
  events: any[] = [];
  options: any;
  viewDate: Date = new Date();

  ngOnInit() {
    this.getActivities();
    this.setOptions();

    this.events = Object.assign([], this.activityView);
    console.log(this.activityView);
    console.log(this.events);
  }

  getActivities() {
    this.activityService.getUserActivities(this.authService.getUserId()).subscribe(
      data => {
        console.log(data);

        data.forEach(item => {
          this.activityView.push({
            "title": item.title,
            "start": item.startTime,
            "end": item.endTime,
          })
        })
      }
    );
  }

  setOptions() {
    this.options = {
      plugins: [timeGridPlugin],
      defaultDate: this.viewDate,
      header: {
        left: 'prev,next',
        center: 'title',
        right: 'agendaWeek,agendaDay',
      },
      firstDay: 1,
      editable: true
    };
  }
}
