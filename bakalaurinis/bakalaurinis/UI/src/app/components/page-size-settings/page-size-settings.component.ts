import { Component, OnInit } from '@angular/core';
import { SettingsService } from 'src/app/services/settings.service';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { UpdatePageSizeSetting } from 'src/app/models/update-page-size-setting';

@Component({
  selector: 'app-page-size-settings',
  templateUrl: './page-size-settings.component.html',
  styleUrls: ['./page-size-settings.component.css']
})
export class PageSizeSettingsComponent implements OnInit {

  pageSizeSetting = 0;
  updatePageSizeSetting = new UpdatePageSizeSetting();

  constructor(
    private authService: AuthServiceService,
    private settingsService: SettingsService
  ) { }

  ngOnInit() {
    this.getPageSize();
  }

  getPageSize() {
    this.settingsService.getItemsPerPageSettings(this.authService.getUserId()).subscribe(
      data => {
        this.pageSizeSetting = data.itemsPerPage;
      }
    );
  }

  updatePageSize() {
    this.updatePageSizeSetting.userId = this.authService.getUserId();
    this.updatePageSizeSetting.itemsPerPage = this.pageSizeSetting;

    this.settingsService.updateItemsPerPageSettings(this.authService.getUserId(), this.updatePageSizeSetting).subscribe();
  }

}
