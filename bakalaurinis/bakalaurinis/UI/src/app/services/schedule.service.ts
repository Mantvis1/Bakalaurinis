import { Injectable } from '@angular/core';
import { UrlService } from './url.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetActivities } from '../models/get-activities';
import { ActivitiesAfterUpdate } from '../models/activities-after-update';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  constructor(private urlService: UrlService, private http: HttpClient) { }

  getUserTodaysActivities(id: number, date: string): Observable<GetActivities[]> {
    return this.http.get<GetActivities[]>(this.urlService.getAbsolutePath('Schedule/' + id + '/' + date));
  }

  getBusyness(id: number, date: string): any {
    return this.http.get<any>(this.urlService.getAbsolutePath('Schedule/' + id));
  }

  updateActivities(id: number, date: string, activitiesAfterUpdate: ActivitiesAfterUpdate): any {
    return this.http.put<any>(this.urlService.getAbsolutePath('Schedule/' + id + '/' + date), activitiesAfterUpdate);
  }
}
