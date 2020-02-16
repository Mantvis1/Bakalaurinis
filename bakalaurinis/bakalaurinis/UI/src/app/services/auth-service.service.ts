import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: "root"
})
export class AuthServiceService {

  private currentUserId: number;
  private readonly Api = 'https://localhost:44314/Users/authenticate';

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService
  ) { }

  login(username: string, password: string) {

    return this.http.post<any>(this.Api, { username, password })
      .pipe(map(it => {
        console.log(it);
        localStorage.setItem('userId', JSON.stringify(it.employeeId));
        localStorage.setItem('token', it.token);
      }));
  }

  logout() {
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
  }

  getUserId() {
    this.currentUserId = JSON.parse(localStorage.getItem("userId"));

    return this.currentUserId;
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }
}
