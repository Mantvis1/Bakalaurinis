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
  hours: number[] = [];
  settings: Settings = new Settings();
  currentTime: Settings = new Settings();

  constructor(
    private authService: AuthServiceService,
    private settingsService: SettingsService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.getSettings();
    this.getAll();
  }

  getAll() {
    for (let i = 1; i <= 24; i++) {
      this.hours.push(i);
    }
  }

  update(): void {
    if (this.settings.startTime == null
      || this.settings.endTime == null
      || this.settings.startTime - this.settings.endTime < 0
    ) {
      this.settings.userId = this.authService.getUserId();

      this.settingsService.updateSettings(this.settings.userId, this.settings).subscribe(
        () => {
          this.getSettings();
        }
      );
    } else
      this.alertService.showMessage('Neteisingai įvedėte laikus!');
    // this.settings = new Settings();
  }

  getSettings() {
    this.settingsService.getSettings(this.authService.getUserId()).subscribe(
      data => {
        this.currentTime = Object.assign({}, data);
      }
    );
  }
}
