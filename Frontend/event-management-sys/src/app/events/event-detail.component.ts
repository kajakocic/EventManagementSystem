import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IEvent } from './event';

@Component({
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css'],
})
export class EventDetailComponent implements OnInit {
  naslov: string = 'Detalji o eventu';
  event: IEvent | undefined;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.naslov += `: ${id}`;
  }
 
  onBack(): void {
    this.router.navigate(['/events']);
  }
}
