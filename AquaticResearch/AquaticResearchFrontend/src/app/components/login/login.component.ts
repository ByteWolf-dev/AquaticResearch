import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { FormsModule, NgModel } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  
  
  usernameInput: string = '';
  passwordInput: string = '';

  constructor(
    private router: Router,
    private dataService: DataService) {
  }

  ngOnInit(): void {
    if (this.dataService.loggedInUser) {
      console.log('navigate to Overview');
      this.router.navigate(['/overview']);
    }
  }

  onSubmit() {
    this.dataService.login(this.usernameInput, this.passwordInput).subscribe({
      next: (authResponse) => {
        console.log('authResponse', authResponse);
        sessionStorage.setItem('jwt', authResponse.accessToken);
        this.dataService.loggedInUser = authResponse.username;
        sessionStorage.setItem('Demo.Login-user', JSON.stringify(this.dataService.loggedInUser));
        this.router.navigate(['/overview']);
      },
      error: (error) => {
        console.log(error);
        alert('Login failed');
      }
    });
  }
}
