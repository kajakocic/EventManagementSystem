import { Injectable } from '@angular/core';
import { IEvent } from './event';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  // const baseUrl = '';
  //`${konstanta}Event/PrikaziFiltriraneEvente`
  private eventUrl = 'http://localhost:5245/api/Event/prikaziEvente';
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