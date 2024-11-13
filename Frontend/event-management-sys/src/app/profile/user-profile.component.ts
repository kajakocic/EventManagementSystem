import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { LoggedUser } from '../auth/logged-user';

@Component({
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  standalone: true,
})
export class UserProfileComponent implements OnInit {
  user: LoggedUser | undefined;
  errorMessage: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile(): void {
    const loggedUser = this.authService.getUser();
    if (loggedUser) {
      this.user = loggedUser;
    } else {
      this.errorMessage = 'Korisnik nije prijavljen.';
    }
  }
}
