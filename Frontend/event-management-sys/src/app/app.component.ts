import { Component } from '@angular/core';

@Component({
  selector: 'pm-root',
  template: `<div>
    <h1>{{ title }}</h1>
    <pm-events></pm-events>
  </div>`,
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Event Management System';
}
