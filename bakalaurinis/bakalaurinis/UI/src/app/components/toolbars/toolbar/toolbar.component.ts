import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material";
import { LoginComponent } from "../../login/login.component";
import { RegistrationComponent } from "../../registration/registration.component";
import { Router } from "@angular/router";

@Component({
  selector: "app-toolbar",
  templateUrl: "./toolbar.component.html",
  styleUrls: ["./toolbar.component.css"]
})
export class ToolbarComponent implements OnInit {
  constructor(public dialog: MatDialog, private router: Router) {}

  ngOnInit() {}

  openRegistrationForm() {
    const dialogRef = this.dialog.open(RegistrationComponent, {
      width: "500px"
    });
  }

  openLoginForm() {
    const dialogRef = this.dialog.open(LoginComponent, { width: "500px" });

    dialogRef.afterClosed().subscribe(data => {
      console.log("T2");
    });

    this.router.navigate["activities"];
  }
}
