import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetActivities } from '../models/get-activities';
import { NewActivity } from '../models/new-activity';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  private readonly Api = 'https://localhost:44314/api/Activities/';

  constructor(private http: HttpClient) { }

  getUserActivities(id: number): Observable<GetActivities[]> {
    return this.http.get<GetActivities[]>(this.Api + 'user/' + id);
  }

  deleteActivity(id: number): Observable<any> {
    return this.http.delete<any>(this.Api + id);
  }

  createNewActivity(newActivity: NewActivity) {
    return this.http.post(this.Api, newActivity);
  }

  editActivity(newActivity: NewActivity, id : number) {
    return this.http.put(this.Api + id, newActivity);
  }
}
