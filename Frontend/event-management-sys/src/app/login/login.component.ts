import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../auth/auth.service';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule],
})
export class LoginComponent {
  public naslov = 'Prijava';

  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);

      this.authService.login(this.loginForm.value).subscribe(
        (response) => {
          console.log('Prijavljeni ste:', response);
          this.authService.saveToken(response.token);
        },
        (error) => {
          console.error('Greška pri prijavi:', error);
        }
      );
    } else {
      console.log('Forma nije validna');
    }
  }
}
