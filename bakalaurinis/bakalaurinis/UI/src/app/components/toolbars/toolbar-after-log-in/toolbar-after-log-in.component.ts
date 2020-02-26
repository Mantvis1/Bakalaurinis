import { Component, OnInit } from "@angular/core";
import { AuthServiceService } from "../../../services/auth-service.service";
import { Router } from "@angular/router";
import { UserService } from '../../../services/user.service';

@Component({
  selector: "app-toolbar-after-log-in",
  templateUrl: "./toolbar-after-log-in.component.html",
  styleUrls: ["./toolbar-after-log-in.component.css"]
})
export class ToolbarAfterLogInComponent implements OnInit {
  currentUserName: string;

  constructor(private auth: AuthServiceService, private router: Router, private userService: UserService) { }

  ngOnInit() {
    this.userService.getUsername(this.auth.getUserId()).subscribe(name => {
      this.currentUserName = name.username;
    });
  }

  logout() {
    this.auth.logout();
    this.router.navigateByUrl("");
  }
}
