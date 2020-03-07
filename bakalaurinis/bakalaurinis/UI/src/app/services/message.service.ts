import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService } from './url.service';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient, private urlService: UrlService) { }

  getUserMessages(userId: number): Observable<Message[]> {
    return this.http.get<Message[]>(this.urlService.getAbsolutePath('Messages/all/' + userId));
  }
}
