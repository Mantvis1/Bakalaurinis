import { Injectable } from '@angular/core';
import { UrlService } from './url.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivitiesAfterUpdate } from '../models/activities-after-update';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  constructor(private urlService: UrlService, private http: HttpClient) { }

  getTodaysWorks(id: number, date: string): Observable<any> {
    return this.http.get<any>(this.urlService.getAbsolutePath('Schedule/' + id + '/' + date));
  }

  updateSchedule(id: number, date: string, activitiesAfterUpdate: ActivitiesAfterUpdate): any {
    return this.http.put<any>(this.urlService.getAbsolutePath('Schedule/' + id + '/' + date), activitiesAfterUpdate);
  }
}
