import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'pm-root',
  template: `
    <head>
      <!-- Google Fonts link -->
      <link
        href="https://fonts.googleapis.com/css2?family=Poppins:wght@700&display=swap"
        rel="stylesheet"
      />
    </head>

    <div class="container-fluid">
      <!-- NavBar -->
      <nav class="navbar navbar-expand navbar-light bg-light ">
        <a class="navbar-brand">{{ naslov }}</a>

        <ul class="nav nav-pills ml-auto">
          <li>
            <a
              class="nav-link btn btn-purple text-white"
              routerLink="/register"
              >Registracija</a
            >
          </li>
          <li>
            <a
              class="nav-link btn btn-purple text-white"
              routerLink="/login"
              >Prijava</a
            >
          </li>
        </ul>
      </nav>

      <div class="row">
        <!-- Sidebar -->
        <div class="col-md-3">
          <div class="sidebar">
            <ul class="nav flex-column">
              <li class="nav-item">
                <a
                  class="nav-link btn btn-purple text-white"
                  routerLink="/welcome"
                  >Dobrodošli</a
                >
              </li>
              <li class="nav-item">
                <a
                  class="nav-link btn btn-purple text-white"
                  routerLink="/about"
                  >O nama</a
                >
              </li>
              <li class="nav-item">
                <a
                  class="nav-link btn btn-purple text-white"
                  routerLink="/events"
                  >Aktuelna dešavanja</a
                >
              </li>
            </ul>
          </div>
        </div>

        <!-- Main Content -->
        <div class="col-md-9">
          <router-outlet></router-outlet>
        </div>
      </div>
    </div>
  `,
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [RouterLinkActive, RouterLink, RouterOutlet],
})
export class AppComponent {
  naslov = '#EMS';
}
