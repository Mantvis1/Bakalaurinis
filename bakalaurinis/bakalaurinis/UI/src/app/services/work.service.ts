import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetWork } from '../models/get-work';
import { NewWork } from '../models/new-work';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})

export class WorkService {

  constructor(
    private http: HttpClient,
    private urlService: UrlService
  ) { }

  getAllByUserId(userId: number): Observable<GetWork[]> {
    return this.http.get<GetWork[]>(this.urlService.getAbsolutePath('Work/user/') + userId);
  }

  getByUserId(userId: number): Observable<GetWork> {
    return this.http.get<GetWork>(this.urlService.getAbsolutePath('Work/' + userId));
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(this.urlService.getAbsolutePath('Work/') + id);
  }

  create(newWork: NewWork) {
    return this.http.post(this.urlService.getAbsolutePath('Work'), newWork);
  }

  update(newWork: NewWork, id: number) {
    return this.http.put(this.urlService.getAbsolutePath('Work/' + id), newWork);
  }
}
