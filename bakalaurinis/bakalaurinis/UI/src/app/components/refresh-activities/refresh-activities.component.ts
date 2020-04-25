import { Component } from '@angular/core';
import { RefreshActivityService } from 'src/app/services/refresh-activity.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-refresh-activities',
  templateUrl: './refresh-activities.component.html',
  styleUrls: ['./refresh-activities.component.css']
})
export class RefreshActivitiesComponent {

  constructor(
    private refreshActivitiesService: RefreshActivityService,
    private authService: AuthServiceService,
    private alertService: AlertService
  ) { }

  refresh() {
    this.refreshActivitiesService.refreshActivities(this.authService.getUserId()).subscribe(
      () => {
        this.alertService.showMessage("Schedule was updated");
      });
  }
}
