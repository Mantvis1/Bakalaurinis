import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthServiceService } from '../../services/auth-service.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  isLoadSchedule = 1;

  constructor(private userService: UserService, private authService: AuthServiceService) { }

  ngOnInit() {
    this.isScheduleExists();
    console.log(this.isLoadSchedule);
  }

  isScheduleExists() {
    let currentUserId = this.authService.getUserId();

    this.userService.getStatusById(currentUserId).subscribe(data => {
      this.isLoadSchedule = data.scheduleStatus;
    });
  }
}
