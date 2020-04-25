import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthServiceService } from '../../services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-account',
  templateUrl: './delete-account.component.html',
  styleUrls: ['./delete-account.component.css']
})
export class DeleteAccountComponent {

  constructor(private userService: UserService,
    private authService: AuthServiceService,
    private router: Router) { }

  delete() {
    if (confirm("Are you sure about that?")) {
      this.userService.deleteUser(this.authService.getUserId()).subscribe(error => {
        console.log(error);
      });

      this.authService.logout();
      this.router.navigateByUrl("");
    }
  }
}
