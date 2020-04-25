import { Component, OnInit } from '@angular/core';
import { Settings } from 'src/app/models/settings';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { SettingsService } from 'src/app/services/settings.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-schedule-settings',
  templateUrl: './schedule-settings.component.html',
  styleUrls: ['./schedule-settings.component.css']
})
export class ScheduleSettingsComponent implements OnInit {
  currentTime: Settings = new Settings();

  constructor(
    private authService: AuthServiceService,
    private settingsService: SettingsService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.getSettings();
  }

  update(): void {
    if (this.currentTime.startTime < this.currentTime.endTime) {
      this.currentTime.userId = this.authService.getUserId();

      this.settingsService.updateSettings(this.currentTime.userId, this.currentTime).subscribe(
        () => {
          this.getSettings();
          this.alertService.showMessage("Selected time was updated");
        }
      );
    } else
      this.alertService.showMessage('Start time value must smaller than end time');
  }

  getSettings() {
    this.settingsService.getSettings(this.authService.getUserId()).subscribe(
      data => {
        this.currentTime = Object.assign({}, data);
      }
    );
  }
}
