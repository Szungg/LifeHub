import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AuthResponse, User } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/auth';
  private tokenKey = 'lifehub_token';
  private userSubject = new BehaviorSubject<User | null>(null);

  constructor(private http: HttpClient) {
    this.loadUser();
  }

  register(email: string, fullName: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, {
      email,
      fullName,
      password,
      confirmPassword: password
    });
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        tap(response => {
          if (response.success && response.token && response.user) {
            localStorage.setItem(this.tokenKey, response.token);
            localStorage.setItem('lifehub_user', JSON.stringify(response.user));
            this.userSubject.next(response.user);
          }
        })
      );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem('lifehub_user');
    this.userSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getCurrentUser(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  private loadUser(): void {
    if (this.isAuthenticated()) {
      const storedUser = localStorage.getItem('lifehub_user');
      if (storedUser) {
        try {
          this.userSubject.next(JSON.parse(storedUser));
        } catch {
          // Si hay error al parsear, ignorar
        }
      }
    }
  }
}
