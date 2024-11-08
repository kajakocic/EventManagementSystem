import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { IUser } from './user';
import { Observable } from 'rxjs';
import { ILoginResponse } from './loginResponse';

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
  }): Observable<ILoginResponse> {
    return this.http.post<ILoginResponse>(`${this.authUrl}/Login`, credentials);
  }

  decodeToken(token: string) {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  saveToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const decodeToken = this.decodeToken(token);

    const expiry = decodeToken(token);
    const now = Math.floor(new Date().getTime() / 1000);
    return now < expiry;
  }
}
