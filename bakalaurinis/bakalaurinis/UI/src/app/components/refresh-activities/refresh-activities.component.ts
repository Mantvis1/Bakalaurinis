import { Component } from '@angular/core';
import { RefreshActivityService } from 'src/app/services/refresh-activity.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';

@Component({
  selector: 'app-refresh-activities',
  templateUrl: './refresh-activities.component.html',
  styleUrls: ['./refresh-activities.component.css']
})
export class RefreshActivitiesComponent {

  constructor(
    private refreshActivitiesService: RefreshActivityService,
    private authService: AuthServiceService
  ) { }

  refresh() {
    this.refreshActivitiesService.refreshActivities(this.authService.getUserId()).subscribe();
  }

}
