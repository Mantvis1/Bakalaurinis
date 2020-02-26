import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-schedule-settings',
  templateUrl: './schedule-settings.component.html',
  styleUrls: ['./schedule-settings.component.css']
})
export class ScheduleSettingsComponent implements OnInit {
  userSettingsForm: FormGroup;
  hours: number[] = [];

  constructor(private formBuilder: FormBuilder) { }

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
      startMonday: new FormControl(''),
      startTuesday: new FormControl(''),
      startWensday: new FormControl(''),
      startThursday: new FormControl(''),
      startFriday: new FormControl(''),
      startSaturday: new FormControl(''),
      startSunday: new FormControl(''),
      endMonday: new FormControl(''),
      endTuesday: new FormControl(''),
      endWensday: new FormControl(''),
      endThursday: new FormControl(''),
      endFriday: new FormControl(''),
      endSaturday: new FormControl(''),
      endSunday: new FormControl('')
    })
  };

  update() {
    console.log(this.userSettingsForm);
  }

}
