import { Component, OnInit } from "@angular/core";
import { AuthServiceService } from "../../../services/auth-service.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-toolbar-after-log-in",
  templateUrl: "./toolbar-after-log-in.component.html",
  styleUrls: ["./toolbar-after-log-in.component.css"]
})
export class ToolbarAfterLogInComponent implements OnInit {
  constructor(private auth: AuthServiceService, private router: Router) {}

  ngOnInit() {}

  logout() {
    this.auth.logout();
    this.router.navigateByUrl("");
  }
}
