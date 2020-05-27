import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from '../../services/alert.service';
import { EncryptionDecryptionService } from 'src/app/services/encryption-decryption.service';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private alertService: AlertService,
    private encryptionDecryptionService: EncryptionDecryptionService
  ) {
    if (this.authenticationService.isAuthenticated()) {
      this.router.navigateByUrl("/schedule");
    }
  }

  ngOnInit() {
    this.createLoginForm();
  }

  login(): void {
    if (this.validateInput()) {
      this.authenticationService.login(
        this.loginForm.value.username,
        this.encryptionDecryptionService.encrypt(this.loginForm.value.password)
      ).subscribe(() => {
        this.router.navigateByUrl("/schedule");
      }, error => {
        this.alertService.showMessage(error);
      });
    }
  }

  private validateInput(): boolean {
    if (this.loginForm.value.username.length === 0 || this.loginForm.value.password.length === 0) {
      this.alertService.showCheckFormMessage();
      return false;
    }

    return true;
  }

  createLoginForm(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(''),
      password: new FormControl('')
    });
  }
}
