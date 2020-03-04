import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetActivities } from '../models/get-activities';
import { NewActivity } from '../models/new-activity';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})

export class ActivityService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  getUserActivities(id: number): Observable<GetActivities[]> {
    return this.http.get<GetActivities[]>(this.urlService.getAbsolutePath('Activities/user/') + id);
  }

  deleteActivity(id: number): Observable<any> {
    return this.http.delete<any>(this.urlService.getAbsolutePath('Activities/') + id);
  }

  createNewActivity(newActivity: NewActivity) {
    console.log(newActivity);
    return this.http.post(this.urlService.getAbsolutePath('Activities'), newActivity);
  }

  editActivity(newActivity: NewActivity, id: number) {
    console.log(newActivity);
    return this.http.put(this.urlService.getAbsolutePath('Activities/' + id), newActivity);
  }

  extendActivity(userId: number, activityId: number) {
    return this.http.put(this.urlService.getAbsolutePath('Activities/extend/' + userId + '/' + activityId), null);
  }

  finishActivity(userId: number, activityId: number) {
    return this.http.put(this.urlService.getAbsolutePath('Activities/finish/' + userId + '/' + activityId), null);
  }
}
