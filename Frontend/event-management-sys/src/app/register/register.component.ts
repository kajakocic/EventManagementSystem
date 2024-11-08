import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, HttpClientModule],
})
export class RegisterComponent {
  public naslov = 'Kreiraj svoj nalog';

  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      ime: ['', Validators.required],
      prezime: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      const formData = this.registerForm.value;
      this.authService.register(formData).subscribe(
        (response) => {
          console.log('Korisnik registrovan:', response);
        },
        (error) => {
          console.error('Greška pri registraciji:', error);
        }
      );
    } else {
      console.log('Forma nije validna');
    }
  }
}
