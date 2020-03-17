import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Invitation } from '../models/invitation';
import { Observable } from 'rxjs';
import { NewInvitation } from '../models/new-invitation';

@Injectable({
  providedIn: 'root'
})
export class InvitationsService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  getInvitationsId(id: number): Observable<Invitation[]> {
    return this.http.get<Invitation[]>(this.urlService.getAbsolutePath('Invitations/' + id));
  }

  createInvitation(newInvitation: NewInvitation): any {
    return this.http.post(this.urlService.getAbsolutePath('Invitations'), newInvitation);
  }

  updateInvitation(id: number, invitation: Invitation): any {
    return this.http.put(this.urlService.getAbsolutePath('Invitations/' + id), invitation);
  }
}
