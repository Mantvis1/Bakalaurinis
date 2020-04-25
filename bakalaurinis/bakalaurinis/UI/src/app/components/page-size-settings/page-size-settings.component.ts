import { Component, OnInit } from '@angular/core';
import { SettingsService } from 'src/app/services/settings.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
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
    private authService: AuthServiceService,
    private settingsService: SettingsService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.getPageSize();
  }

  getPageSize() {
    this.settingsService.getItemsPerPageSettings(this.authService.getUserId()).subscribe(
      data => {
        this.updatePageSizeSetting.itemsPerPage = data.itemsPerPage;
      }
    );
  }

  updatePageSize() {
    this.updatePageSizeSetting.userId = this.authService.getUserId();

    this.settingsService.updateItemsPerPageSettings(this.authService.getUserId(), this.updatePageSizeSetting).subscribe(
      () => {
        this.alertService.showMessage('Page size was updated');
        this.getPageSize();
      }

    );
  }

}
