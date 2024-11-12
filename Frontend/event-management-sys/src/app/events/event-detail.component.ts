import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IEvent } from './event';
import { EventService } from './event.service';
import { CommonModule } from '@angular/common';

@Component({
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class EventDetailComponent implements OnInit {
  naslov: string = 'Detalji o eventu:';
  errorMessage = '';
  event: IEvent | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    //this.naslov += `: ${id}`;
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

  onEdit(): void {
    if (this.event) {
      this.router.navigate(['/edit-event', this.event.id]);
    }
  }
}
