import { Injectable } from '@angular/core';
import { IEvent } from './event';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { ILocation } from './location';
import { IKategorija } from './category';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  // const baseUrl = '';
  //`${konstanta}Event/PrikaziFiltriraneEvente`
  private eventUrl = 'http://localhost:5245/api/Event/prikaziEvente';
  private locUrl = 'http://localhost:5245/api/Location/PrikaziSveLokacije';
  private katUtl = 'http://localhost:5245/api/Kategorija/PrikaziSveKategorije';
  private evUrl = environment.eventUrl;
  constructor(private http: HttpClient) {}

  /* getEventsFromServer(): Observable<IEvent[]> {
    return this.http.get<IEvent[]>(
      'http://localhost:5245/api/Event/PrikaziFiltriraneEvente'
    ); */

  getEvents(): Observable<IEvent[]> {
    return this.http.get<IEvent[]>(this.eventUrl).pipe(
      tap((data) => console.log('All', JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  getEvent(id: number): Observable<IEvent | undefined> {
    return this.getEvents().pipe(
      map((events: IEvent[]) => events.find((e) => e.id === id))
    );
  }

  getLocations(): Observable<ILocation[]> {
    return this.http.get<ILocation[]>(this.locUrl).pipe(
      tap((data) => console.log('All', JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  getCategories(): Observable<IKategorija[]> {
    return this.http.get<IKategorija[]>(this.katUtl).pipe(
      tap((data) => console.log('All', JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  addEvent(eventData: IEvent): Observable<IEvent>{
    return this.http.post<IEvent>(`${this.evUrl}/DodajEvent`, eventData);
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An Error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
