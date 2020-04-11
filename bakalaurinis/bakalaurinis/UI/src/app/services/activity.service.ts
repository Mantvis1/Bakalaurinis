import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetActivities } from '../models/get-activities';
import { NewActivity } from '../models/new-activity';
import { UrlService } from './url.service';
import { WorkStatusConfirmation } from '../models/work-status-confirmation';

@Injectable({
  providedIn: 'root'
})

export class ActivityService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  getUserActivities(id: number): Observable<GetActivities[]> {
    return this.http.get<GetActivities[]>(this.urlService.getAbsolutePath('Work/user/') + id);
  }
  getUserActivityById(id: number): Observable<GetActivities> {
    return this.http.get<GetActivities>(this.urlService.getAbsolutePath('Work/' + id));
  }

  deleteActivity(id: number): Observable<any> {
    return this.http.delete<any>(this.urlService.getAbsolutePath('Work/') + id);
  }

  createNewActivity(newActivity: NewActivity) {
    return this.http.post(this.urlService.getAbsolutePath('Work'), newActivity);
  }

  editActivity(newActivity: NewActivity, id: number) {
    return this.http.put(this.urlService.getAbsolutePath('Work/' + id), newActivity);
  }

  getWorkStatus(workId: number): Observable<WorkStatusConfirmation> {
    return this.http.get<WorkStatusConfirmation>(this.urlService.getAbsolutePath('Work/status/' + workId));
  }

  updateWorkStatus(workId: number, workStatusConfirmation: WorkStatusConfirmation): Observable<any> {
    return this.http.put(this.urlService.getAbsolutePath('Work/status/' + workId), workStatusConfirmation);
  }
}
