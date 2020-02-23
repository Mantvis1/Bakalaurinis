import { Component, OnInit } from "@angular/core";
import { MatSnackBar } from "@angular/material";
import { AuthServiceService } from "src/app/services/auth-service.service";
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(
    private authService: AuthServiceService,
    private router: Router,
    private matSnackBar: MatSnackBar,
    private alertService: AlertService
  ) {
    if (this.authService.isAuthenticated()) {
      this.router.navigateByUrl("/schedule");
    }
  }

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl(''),
      password: new FormControl('')
    });
  }

  login() {
    if (this._validateInput()) {
      this.authService.login(this.loginForm.value.username, this.loginForm.value.password).subscribe(() => {
        this.router.navigateByUrl("/schedule");
      }, error => {
        this.alertService.showMessage('Prisijungimo vardas arba slaptažodis yra neteisingi');
      });
    }
  }

  private _validateInput(): boolean {
    if (this.loginForm.value.username.length == 0 || this.loginForm.value.password.length == 0) {
      this.alertService.showCheckFormMessage();
      return false;
    }

    return true;
  }
}
