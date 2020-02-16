import { Component, OnInit, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { AuthServiceService } from "src/app/services/auth-service.service";
import { Router } from '@angular/router';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public matData: any,
    private authService: AuthServiceService,
    private router: Router
  ) {
    if (this.authService.isAuthenticated()) {
      this.router.navigateByUrl("/schedule");
    }
  }

  ngOnInit() {}

  
}
