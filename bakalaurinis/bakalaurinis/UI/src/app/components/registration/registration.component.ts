import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { UserRegister } from '../../models/user-register';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html",
  styleUrls: ["./registration.component.css"]
})
export class RegistrationComponent implements OnInit {

  registrationForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    reapeatPassword: new FormControl(''),
    email: new FormControl('')
  });

  user: UserRegister;

  constructor(
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() { }

  register() {
    if (this._validateInput()) {
      this.user = Object.assign({}, this.registrationForm.value);

      this.userService.register(this.user).subscribe(error => {
        console.log(error);
      });

      this.registrationForm.reset();
    }
  }

  private _validateInput(): boolean {
    if (this.registrationForm.value.username.length == 0 ||
      this.registrationForm.value.password.length == 0 ||
      this.registrationForm.value.reapeatPassword.length == 0 ||
      this.registrationForm.value.email.length == 0
    ) {
      this.alertService.showCheckFormMessage();

      return false;
    } else if (this.registrationForm.value.password !== this.registrationForm.value.reapeatPassword) {
      this.alertService.showMessage('Slaptažodžiai nesutampa');

      return false;
    }

    return true;
  }
}
