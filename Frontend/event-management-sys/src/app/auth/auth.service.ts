import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { IUser } from './user';
import { Observable } from 'rxjs';
import { LoggedUser } from './logged-user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) {}

  register(userData: IUser): Observable<IUser> {
    return this.http.post<IUser>(`${this.authUrl}/Register`, userData);
  }

  login(credentials: {
    email: string;
    password: string;
  }): Observable<LoggedUser> {
    return this.http.post<LoggedUser>(`${this.authUrl}/Login`, credentials);
  }

  decodeToken(token: string) {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  saveToken(token: LoggedUser): void {
    localStorage.setItem('user', JSON.stringify(token));
  }

  getToken(): LoggedUser | null {
    return JSON.parse(localStorage.getItem('user') ?? '');
  }

  logout(): void {
    localStorage.removeItem('user');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const decodeToken = this.decodeToken(token.Token);

    const expiry = decodeToken(token);
    const now = Math.floor(new Date().getTime() / 1000);
    return now < expiry;
  }
}
