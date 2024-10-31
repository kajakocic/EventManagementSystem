import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { EventListComponent } from './events/event-list.component';
import { FormsModule } from '@angular/forms';
import { FirstSentence } from './shared/first-sentence.pipe';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent, EventListComponent, FirstSentence],
  imports: [BrowserModule, FormsModule, HttpClientModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
