import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { EventListComponent } from './events/event-list.component';
import { FormsModule } from '@angular/forms';
import { FirstSentence } from './shared/first-sentence.pipe';
import { HttpClientModule } from '@angular/common/http';
import { EventDetailComponent } from './events/event-detail.component';
import { RouterModule } from '@angular/router';
import { WelcomeComponent } from './home/welcome.component';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    EventListComponent,
    FirstSentence,
    EventDetailComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'events', component: EventListComponent },
      { path: 'events/:id', component: EventDetailComponent },
      { path: 'welcome', component: WelcomeComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' },
    ]),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
