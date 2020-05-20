import { Component } from '@angular/core';
import { RefreshActivityService } from 'src/app/services/refresh-activity.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-refresh-activities',
  templateUrl: './refresh-activities.component.html',
  styleUrls: ['./refresh-activities.component.css']
})
export class RefreshActivitiesComponent {

  constructor(
    private refreshActivitiesService: RefreshActivityService,
    private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) { }

  refresh() {
    this.refreshActivitiesService.refreshActivities(this.authenticationService.getUserId()).subscribe(
      () => {
        this.alertService.showMessage("Schedule was updated");
      });
  }
}
