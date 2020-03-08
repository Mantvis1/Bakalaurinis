import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Settings } from '../models/settings';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  updateSettings(userId: number, settings: Settings): any {
    return this.http.put<any>(this.urlService.getAbsolutePath('UsersSettings/' + userId), settings);
  }

  getSettings(userId: number): Observable<Settings> {
    return this.http.get<Settings>(this.urlService.getAbsolutePath('UsersSettings/' + userId));
  }
}
