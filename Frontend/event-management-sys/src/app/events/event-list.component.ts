import { Component, OnDestroy, OnInit } from '@angular/core';
import { IEvent } from './event';
import { EventService } from './event.service';
import { Subscription } from 'rxjs';

@Component({
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css'],
})
export class EventListComponent implements OnInit, OnDestroy {
  naslov: string = 'Aktuelna dešavanja';
  errorMessage: string = '';
  sub!: Subscription;

  private _listFilter: string = '';

  get listFilter(): string {
    return this._listFilter;
  }

  set listFilter(value: string) {
    this._listFilter = value;
    console.log('in setter:', value);
    this.filteredEvents = this.filtriraj(value);
  }

  filteredEvents: IEvent[] = [];
  events: IEvent[] = [];

  constructor(private eventService: EventService) {}

  filtriraj(filtrirajPo: string): IEvent[] {
    filtrirajPo = filtrirajPo.toLocaleLowerCase();
    return this.events.filter((e: IEvent) =>
      e.naziv.toLocaleLowerCase().includes(filtrirajPo)
    );
  }

  ngOnInit(): void {
    // this.events = this.eventService.getEvents();
    this.sub = this.eventService.getEvents().subscribe(
      /* (data) => {
      this.events = data;
    } */
      {
        next: (events) => {
          this.events = events;
          this.filteredEvents = this.events;
        },
        error: (err) => this.errorMessage,
      }
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
