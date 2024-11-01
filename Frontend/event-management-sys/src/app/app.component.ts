import { Component } from '@angular/core';

@Component({
  selector: 'pm-root',
  template: `
    <nav class="navbar navbar-expand navbar-light bg-light">
      <a class="navbar-brand">{{ naslov }}</a>
      <ul class="nav nav-pills">
        <li><a class="nav-link" routerLink="/welcome">Početna stranica</a></li>
        <li><a class="nav-link" routerLink ="/events">Aktuelna dešavanja</a></li>
      </ul>
    </nav>
    <div class="container">
      <router-outlet></router-outlet>
    </div>
  `,
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  naslov = 'Event Management System';
}
