import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { EventListComponent } from './events/event-list.component';

@NgModule({
  declarations: [AppComponent, EventListComponent],
  imports: [BrowserModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
