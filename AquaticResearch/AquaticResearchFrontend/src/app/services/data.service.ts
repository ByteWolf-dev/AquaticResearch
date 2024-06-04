import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthResponse, ObservationDto, ResearchProjectDto, User } from '../model/model';
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

  getAllProjects(): Observable<ResearchProjectDto[]> {
    const url = `${this.baseUrl}/ResearchProject`;
    return this.httpClient.get<ResearchProjectDto[]>(url);
  }

  getAllObservationsForProject(projectTitle: string): Observable<ObservationDto[]> {
    const options = {
        params: new HttpParams().set('title', projectTitle)
    };

    const url = `${this.baseUrl}/Observation`;
    return this.httpClient.get<ObservationDto[]>(url, options);
}

  addObservation(observation: ObservationDto, projectTitle: string): Observable<ObservationDto> {
    const url = `${this.baseUrl}/Observation`;
    return this.httpClient.post<ObservationDto>(url, observation);
  }

  addProject(project: ResearchProjectDto): Observable<ResearchProjectDto> {
    const url = `${this.baseUrl}/ResearchProject`;
    return this.httpClient.post<ResearchProjectDto>(url, project);
  }
}
