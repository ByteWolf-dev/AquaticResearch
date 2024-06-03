import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(public router: Router) { }

  canActivate(): boolean {
    const jwt = sessionStorage.getItem('jwt');
    if (!jwt) {
      this.router.navigate(['login']);
      console.log("NO JWT TOKEN FOUND");
      return false;
    }
    console.log("JWT TOKEN FOUND");
    return true;
  }
}