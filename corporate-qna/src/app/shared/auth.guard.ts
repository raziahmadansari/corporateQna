import { Injectable, Injector } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private injector: Injector) { }

  canActivate(): boolean {
    let authService = this.injector.get(AuthService);
    if (authService.loggedIn()) {
      return true;
    }
    else {
      // this.router.navigate(['/']); // navigate to login page
      authService.login();
      return false;
    }
  }
  
}
