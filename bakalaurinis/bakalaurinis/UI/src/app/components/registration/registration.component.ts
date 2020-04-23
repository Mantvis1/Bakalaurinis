import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { UserRegister } from '../../models/user-register';
import { AlertService } from '../../services/alert.service';
import { MessageService } from 'src/app/services/message.service';

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
    if (this.validateInput()) {
      this.user = Object.assign({}, this.registrationForm.value);

      this.userService.register(this.user).subscribe(
        () => {
          this.alertService.showMessage("registration successful");
          this.registrationForm.reset()
        },
        error => {
          this.alertService.showMessage(error);
        });

      ;
    }
  }

  private validateInput(): boolean {
    if (!this.registrationForm.valid) {
      this.alertService.showCheckFormMessage();

      return false;
    } else if (this.registrationForm.value.password !== this.registrationForm.value.reapeatPassword) {
      this.alertService.showMessage('Passwords do not match');

      return false;
    }

    return true;
  }
}
