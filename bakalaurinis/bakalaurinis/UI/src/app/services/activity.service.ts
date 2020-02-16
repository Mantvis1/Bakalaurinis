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
    return this.http.delete<any>(this.urlService.getAbsolutePath('Activities') + id);
  }

  createNewActivity(newActivity: NewActivity) {
    return this.http.post(this.urlService.getAbsolutePath('Activities'), newActivity);
  }

  editActivity(newActivity: NewActivity, id : number) {
    return this.http.put(this.urlService.getAbsolutePath('Activities') + id, newActivity);
  }
}
