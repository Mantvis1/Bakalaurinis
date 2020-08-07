import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "../../../services/authentication.service";
import { Router } from "@angular/router";
import { UserService } from '../../../services/user.service';

@Component({
  selector: "app-toolbar-after-log-in",
  templateUrl: "./toolbar-after-log-in.component.html",
  styleUrls: ["./toolbar-after-log-in.component.css"]
})
export class ToolbarAfterLogInComponent implements OnInit {
  currentUserName: string;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.getUserNameById();
    this.resizeMenu();
  }

  getUserNameById() {
    this.userService.getUsername(this.authenticationService.getUserId()).subscribe(name => {
      this.currentUserName = name.username;
    });
  }

  resizeMenu(): boolean {
    if (window.innerWidth > 600) {
      return true;
    }

    return false;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigateByUrl("");
  }
}
