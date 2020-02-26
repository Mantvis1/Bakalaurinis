import { Injectable } from '@angular/core';
import { UrlService } from './url.service';
import { HttpClient } from '@angular/common/http';
import { UserRegister } from '../models/user-register';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private urlService: UrlService, private http: HttpClient) { }

  getUsername(id: number): any {
    return this.http.get<any>(this.urlService.getAbsolutePath('Users/self/' + id));
  }

  register(userRegister: UserRegister) {
    return this.http.post<any>(this.urlService.getAbsolutePath('Users/register'), userRegister);
  }

  deleteUser(id: number) {
    return this.http.delete(this.urlService.getAbsolutePath('Users/' + id));
  }
}
