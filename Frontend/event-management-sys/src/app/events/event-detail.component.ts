import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { IEvent } from './event';
import { EventService } from './event.service';
import { CommonModule } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../auth/auth.service';

@Component({
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule],
})
export class EventDetailComponent implements OnInit {
  naslov: string = 'Detalji o eventu:';
  errorMessage = '';
  event: IEvent | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.getEvent(id);
  }

  getEvent(id: number): void {
    this.eventService.getEvent(id).subscribe({
      next: (event) => (this.event = event),
      error: (err) => (this.errorMessage = err),
    });
  }

  onBack(): void {
    this.router.navigate(['/events']);
  }

  onDelete(): void {
    if (this.event && this.event.id) {
      const eventId = this.event.id;

      this.eventService.deleteEvent(eventId).subscribe({
        next: () => {
          this.snackBar.open('Event je uspešno obrisan!', 'Zatvori', {
            duration: 6000,
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });

          this.router.navigate(['/events']);
        },
        error: (err) => {
          console.error('Greška prilikom brisanja:', err);
          this.snackBar.open(
            'Došlo je do greške prilikom brisanja eventa. Pokušaj ponovo.',
            'Zatvori',
            {
              duration: 6000,
              horizontalPosition: 'center',
              verticalPosition: 'top',
            }
          );
        },
      });
    }
  }

  show(): boolean {
    if (this.authService.isAdmin()) return true;
    return false;
  }
}
