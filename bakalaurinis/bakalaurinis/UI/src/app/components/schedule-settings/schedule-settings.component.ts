import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Settings } from 'src/app/models/settings';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { SettingsService } from 'src/app/services/settings.service';

@Component({
  selector: 'app-schedule-settings',
  templateUrl: './schedule-settings.component.html',
  styleUrls: ['./schedule-settings.component.css']
})
export class ScheduleSettingsComponent implements OnInit {
  userSettingsForm: FormGroup;
  hours: number[] = [];
  settings: Settings = new Settings();

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthServiceService,
    private settingsService: SettingsService
  ) { }

  ngOnInit() {
    this.initializeFormGroup();
    this.getAll();
  }
  getAll() {
    for (let i = 1; i <= 24; i++) {
      this.hours.push(i);
    }
  }

  initializeFormGroup() {
    this.userSettingsForm = this.formBuilder.group({
      startTime: new FormControl(''),
      endTime: new FormControl('')
    })
  };

  update(): void {
    this.settings.userId = this.authService.getUserId();
    this.settings.endTime = Number(this.userSettingsForm.controls.endTime.value);
    this.settings.startTime = Number(this.userSettingsForm.controls.startTime.value);

    this.settingsService.updateSettings(this.settings.userId, this.settings).subscribe(
      error => {
        console.log(error);
      }
    )
  }

}
