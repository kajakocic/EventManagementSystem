import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IEvent } from '../events/event';
import { EventService } from '../events/event.service';


@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css'],
})
export class EditEventComponent implements OnInit {
  editEventForm!: FormGroup;  // Koristimo "!" da označimo da će biti inicijalizovan
  event: IEvent | undefined;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private eventService: EventService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
  }



  onSubmit(): void {
/*     if (this.editEventForm.valid && this.event) {
      const updatedEvent: IEvent = this.editEventForm.value;
      this.eventService.updateEvent(this.event.id, updatedEvent).subscribe(
        (response) => {
          console.log('Događaj je uspešno ažuriran', response);
          this.router.navigate(['/events']);
        },
        (error) => {
          console.error('Greška pri ažuriranju događaja', error);
        }
      );
    } else {
      console.log('Forma nije validna');
    } */
  }

  onCancel(): void {
    this.router.navigate(['/events']);
  }
}
