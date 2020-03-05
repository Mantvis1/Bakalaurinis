import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-base-invitation',
  templateUrl: './base-invitation.component.html',
  styleUrls: ['./base-invitation.component.css']
})
export class BaseInvitationComponent implements OnInit {

  switchPages: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  switch(): boolean {
    if (this.switchPages) return true; return false;
  }

  updateSwitch(settingValue: boolean): void {
    this.switchPages = settingValue;
    this.switch();
  }
}
