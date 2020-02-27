import { Injectable } from '@angular/core';
import { UrlService } from './url.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetActivities } from '../models/get-activities';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  constructor(private urlService: UrlService, private http: HttpClient) { }

  getUserTodaysActivities(id: number): Observable<GetActivities[]> {
    return this.http.get<GetActivities[]>(this.urlService.getAbsolutePath('Schedule/') + id);
  }
}
