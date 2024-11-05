import { Routes } from '@angular/router';
import { WelcomeComponent } from './home/welcome.component';

export const routes: Routes = [
  { path: 'welcome', component: WelcomeComponent },
  { path: '', redirectTo: 'welcome', pathMatch: 'full' },
  {
    path: 'events',
    loadChildren: () =>
      import('./events/event.routes').then((r) => r.EVENT_ROUTES),
  },
  {
    path: 'about',
    loadComponent: () =>
      import('./about/about.component').then((c) => c.AboutComponent),
  },
  { path: '**', redirectTo: 'welcome', pathMatch: 'full' },
];
