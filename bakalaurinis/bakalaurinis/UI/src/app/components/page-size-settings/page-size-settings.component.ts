import { Component, OnInit } from '@angular/core';
import { SettingsService } from 'src/app/services/settings.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UpdatePageSizeSetting } from 'src/app/models/update-page-size-setting';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-page-size-settings',
  templateUrl: './page-size-settings.component.html',
  styleUrls: ['./page-size-settings.component.css']
})
export class PageSizeSettingsComponent implements OnInit {
  updatePageSizeSetting = new UpdatePageSizeSetting();

  constructor(
    private authenticationService: AuthenticationService,
    private settingsService: SettingsService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.getPageSize();
  }

  getPageSize() {
    this.settingsService.getItemsPerPageSettings(this.authenticationService.getUserId()).subscribe(
      data => {
        this.updatePageSizeSetting.itemsPerPage = data.itemsPerPage;
      }
    );
  }

  updatePageSize() {
    this.updatePageSizeSetting.userId = this.authenticationService.getUserId();

    this.settingsService.updateItemsPerPageSettings(this.authenticationService.getUserId(), this.updatePageSizeSetting).subscribe(
      () => {
        this.alertService.showMessage('Page size was updated');
        this.getPageSize();
      }

    );
  }

}
