import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class RefreshActivityService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  refreshActivities(id: number) {
    return this.http.put(this.urlService.getAbsolutePath('ActivitiesRefresh/' + id), null);
  }
}
