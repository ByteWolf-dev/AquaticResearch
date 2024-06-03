import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthResponse, User } from '../model/model';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  loggedInUser: string | undefined;
  baseUrl = '';

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.apiBaseUrl;
    const jwt = sessionStorage.getItem('jwt');
    if (jwt) {
      const user = sessionStorage.getItem('Demo.Login-user');
      this.loggedInUser = user ? JSON.parse(user) : undefined;
    }
  }

  login(usernameInput: string, passwordInput: string): Observable<AuthResponse> {
    const url = `${this.baseUrl}/login`;
    const user: User = {
      username: usernameInput,
      password: passwordInput
    };
    return this.httpClient.post<AuthResponse>(url, user);
  }
}
