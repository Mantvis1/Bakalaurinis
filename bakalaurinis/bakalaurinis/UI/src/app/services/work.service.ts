import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetActivities } from '../models/get-activities';
import { NewActivity } from '../models/new-activity';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})

export class WorkService {

  constructor(
    private http: HttpClient,
    private urlService: UrlService
  ) { }

  getAllByUserId(userId: number): Observable<GetActivities[]> {
    return this.http.get<GetActivities[]>(this.urlService.getAbsolutePath('Work/user/') + userId);
  }

  getByUserId(userId: number): Observable<GetActivities> {
    return this.http.get<GetActivities>(this.urlService.getAbsolutePath('Work/' + userId));
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(this.urlService.getAbsolutePath('Work/') + id);
  }

  create(newWork: NewActivity) {
    return this.http.post(this.urlService.getAbsolutePath('Work'), newWork);
  }

  update(newWork: NewActivity, id: number) {
    return this.http.put(this.urlService.getAbsolutePath('Work/' + id), newWork);
  }
}
