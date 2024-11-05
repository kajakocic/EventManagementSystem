import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'pm-root',
  template: `
    <head>
      <!--Google Fonts link-->
      <link
        href="https://fonts.googleapis.com/css2?family=Poppins:wght@700&display=swap"
        rel="stylesheet"
      />
    </head>
    <nav class="navbar navbar-expand navbar-light bg-light">
      <a class="navbar-brand">{{ naslov }}</a>
      <ul class="nav nav-pills">
        <li>
          <a
            class="nav-link btn btn-purple text-white font-weight-bold"
            routerLink="/welcome"
            >Dobrodošli</a
          >
        </li>
        <li>
          <a
            class="nav-link btn btn-purple text-white font-weight-bold"
            routerLink="/about"
            >O nama</a
          >
        </li>
        <li>
          <a
            class="nav-link btn btn-purple text-white font-weight-bold"
            routerLink="/events"
            >Aktuelna dešavanja</a
          >
        </li>
      </ul>
    </nav>
    <div class="container">
      <router-outlet></router-outlet>
    </div>
  `,
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [RouterLinkActive, RouterLink, RouterOutlet],
})
export class AppComponent {
  naslov = 'EMS';
}
