import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { UrlService } from './url.service';

@Injectable({
  providedIn: "root"
})

export class AuthenticationService {

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private urlService: UrlService
  ) { }

  login(username: string, password: string) {
    return this.http.post<any>(this.urlService.getAbsolutePath('Users/authenticate'), { username, password })
      .pipe(map(it => {
        localStorage.setItem('userId', JSON.stringify(it.id));
        localStorage.setItem('token', it.token);
      }));
  }

  logout() {
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
  }

  getUserId() {
    return JSON.parse(localStorage.getItem("userId"));
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }
}
