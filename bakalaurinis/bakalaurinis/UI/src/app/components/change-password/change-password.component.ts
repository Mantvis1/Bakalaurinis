import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../../services/auth-service.service';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup;

  constructor(
    private authService: AuthServiceService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.initializeFormGroup();
  }
  initializeFormGroup() {
    this.changePasswordForm = this.formBuilder.group({
      password: new FormControl(''),
      newPassword: new FormControl(''),
      repeatNewPassword: new FormControl(''),
    });
  }

  changePassword() {
    console.log("password was changed");
  }

}
