import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Observable } from 'rxjs';
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  getUserMessages(userId: number): Observable<Message[]> {
    return this.http.get<Message[]>(this.urlService.getAbsolutePath('Messages/all/' + userId));
  }

  deleteUserAllMessagesById(userId: number) {
    return this.http.delete(this.urlService.getAbsolutePath('Messages/all/' + userId));
  }

  deleteUserMessagesById(userId: number, messageId: number) {
    return this.http.delete(this.urlService.getAbsolutePath('Messages/' + messageId));
  }
}
