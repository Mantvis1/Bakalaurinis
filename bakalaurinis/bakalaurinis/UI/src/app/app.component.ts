import { Component } from "@angular/core";
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = "UI";

  constructor(private authenticationService: AuthenticationService) { }

  isAuthenticated() {
    if (this.authenticationService.isAuthenticated()) {
      return true;
    }

    return false;
  }
}
