import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class AuthServiceService {
  private currentUserId: number;

  constructor() {}

  login() {
    localStorage.setItem("userId", "1");
  }

  logout() {
    localStorage.removeItem("userId");
  }

  getUserId() {
    this.currentUserId = JSON.parse(localStorage.getItem("userId"));
    return this.currentUserId;
  }

  isAuthenticated(): boolean {
    if (localStorage.getItem("userId") != null) {
      return true;
    }

    return false;
  }
}
