import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { UserRegister } from '../../models/user-register';

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
    private userService: UserService
  ) {}

  ngOnInit() { }

  register() {
    this.user = Object.assign({}, this.registrationForm.value);

    this.userService.register(this.user).subscribe(error => {
      console.log(error);
    })

    this.registrationForm.reset();
  }
}
