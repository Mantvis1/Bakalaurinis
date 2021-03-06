import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Settings } from '../models/settings';
import { Observable } from 'rxjs';
import { UserPageSizeSetting } from '../models/user-page-size-setting';
import { UpdatePageSizeSetting } from '../models/update-page-size-setting';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  update(userId: number, settings: Settings): any {
    return this.http.put<any>(this.urlService.getAbsolutePath('UsersSettings/' + userId), settings);
  }

  getByUserId(userId: number): Observable<Settings> {
    return this.http.get<Settings>(this.urlService.getAbsolutePath('UsersSettings/' + userId));
  }

  getItemsPerPageSetting(userId: number): Observable<UserPageSizeSetting> {
    return this.http.get<UserPageSizeSetting>(this.urlService.getAbsolutePath('UsersSettings/itemsPerPage/' + userId));
  }

  updateItemsPerPageSetting(userId: number, updatePageSize: UpdatePageSizeSetting): any {
    return this.http.put<any>(this.urlService.getAbsolutePath('UsersSettings/itemsPerPage/' + userId), updatePageSize);
  }
}
