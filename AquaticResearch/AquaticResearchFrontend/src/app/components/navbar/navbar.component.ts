import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(public dataService: DataService, private router : Router) { }

  onLogout(): void {
    sessionStorage.removeItem('jwt');
    this.dataService.loggedInUser = undefined;
    this.router.navigate(['/login']);
  }
}
