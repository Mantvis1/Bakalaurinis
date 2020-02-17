import { Component, OnInit, Inject } from "@angular/core";
import {MatSnackBar } from "@angular/material";
import { AuthServiceService } from "src/app/services/auth-service.service";
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl('')
  });

  constructor(
    private authService: AuthServiceService,
    private router: Router,
    private matSnackBar: MatSnackBar
  ) {
    if (this.authService.isAuthenticated()) {
      this.router.navigateByUrl("/schedule");
    }
  }

  ngOnInit() { }

  login() {
    this.authService.login(this.loginForm.value.username, this.loginForm.value.password).subscribe(() => {
      this.router.navigateByUrl("/schedule");
    }, error => {
        this.showWanrningMessage();
    });
  }

  showWanrningMessage() {
    this.matSnackBar.open('Prisijungimo vardas arba slaptažodis yra neteisingi', 'Uždaryti', {
      duration: 2000,
    });
  }

}
