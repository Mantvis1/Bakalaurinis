import { Injectable } from '@angular/core';
import { UrlService } from './url.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private urlService: UrlService, private http: HttpClient) { }

  getUsername(id: number): any {
    return this.http.get<any>(this.urlService.getAbsolutePath('Users/self/' + id));
  }
}
