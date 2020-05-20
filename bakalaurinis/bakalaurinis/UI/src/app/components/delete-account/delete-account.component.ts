import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-account',
  templateUrl: './delete-account.component.html',
  styleUrls: ['./delete-account.component.css']
})
export class DeleteAccountComponent {

  constructor(private userService: UserService,
    private authenticationService: AuthenticationService,
    private router: Router) { }

  delete() {
    if (confirm("Are you sure about that?")) {
      this.userService.deleteUser(this.authenticationService.getUserId()).subscribe();

      this.authenticationService.logout();
      this.router.navigateByUrl("");
    }
  }
}
