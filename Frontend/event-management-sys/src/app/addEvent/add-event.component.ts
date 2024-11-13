import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ILocation } from '../events/location';
import { IKategorija } from '../events/category';
import { EventService } from '../events/event.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { dateNotExpiredValidator, priceValidator } from './addEventValidators';

@Component({
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule],
})
export class AddEventComponent implements OnInit {
  addEventForm: FormGroup;
  locations: ILocation[] = [];
  categories: IKategorija[] = [];
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private eventService: EventService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.addEventForm = this.fb.group({
      naziv: ['', [Validators.required, Validators.minLength(3)]],
      datum: ['', [Validators.required, dateNotExpiredValidator()]],
      kapacitet: ['', [Validators.required, Validators.min(1)]],
      opis: ['', [Validators.required]],
      cenaKarte: ['', [Validators.required, priceValidator()]],
      urlImg: ['', Validators.required],
      lokacija: ['', Validators.required],
      kategorija: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    // Preuzimanje lokacija i kategorija
    this.eventService.getLocations().subscribe(
      (locations) => {
        this.locations = locations;
      },
      (error) => {
        this.errorMessage = 'Greška pri učitavanju lokacija.';
      }
    );

    this.eventService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
      },
      (error) => {
        this.errorMessage = 'Greška pri učitavanju kategorija.';
      }
    );
  }

  onSubmit(): void {
    if (this.addEventForm.valid) {
      console.log(this.addEventForm.value);

      const formData = this.addEventForm.value;
      this.eventService.addEvent(formData).subscribe(
        (response) => {
          console.log('Event je dodat:', response);

          this.snackBar.open('Uspešno dodat event!', 'Zatvori', {
            duration: 6000,
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });

          this.router.navigate(['/events']);
        },
        (error) => {
          console.error('Greška pri registraciji:', error);

          this.snackBar.open(
            'Greška prilikom dodavanja eventa. Pokušaj ponovo.',
            'Zatvori',
            {
              duration: 6000,
              horizontalPosition: 'center',
              verticalPosition: 'top',
            }
          );
          this.addEventForm.reset();
        }
      );
    } else {
      console.log('Forma nije validna');

      this.snackBar.open(
        'Popuni sve potrebne podatke. Pokušaj ponovo.',
        'Zatvori',
        {
          duration: 6000,
          horizontalPosition: 'center',
          verticalPosition: 'top',
        }
      );

      this.addEventForm.reset();
    }
  }

  onCancel(): void {
    this.addEventForm.reset();
    console.log('Forma je otkazana');
  }
}
