import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Observable } from 'rxjs';
import { UserInvitation } from '../models/user-invitation';

@Injectable({
  providedIn: 'root'
})

export class UserInvitationService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  getAllByActivityId(activityId: number): Observable<UserInvitation> {
    return this.http.get<UserInvitation>(this.urlService.getAbsolutePath('UserInvitations/' + activityId));
  }
}
