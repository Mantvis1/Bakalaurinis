import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Settings } from '../models/settings';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  updateSettings(userId: number, settings: Settings): any {
    return this.http.put<any>(this.urlService.getAbsolutePath('UsersSettings/' + userId), settings);
  }
}
