import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { UserData } from './shared/models/user-data';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'corporate-qna';
  name: string = 'User';
  userdata: UserData;
  todayDate: Date;

  constructor(public authService: AuthService, public oidcSecurityService: OidcSecurityService) { }

  ngOnInit() {
    this.todayDate = new Date();

    this.oidcSecurityService.checkAuth().subscribe(
      (auth) => {
        console.log('is authenticated:', auth);
        if (auth) {
          this.oidcSecurityService.userData$.subscribe(
            (data:any) => {
              this.userdata = <UserData> data;
              this.name = this.userdata?.name;
              this.authService.verifyUser(this.userdata);
            }
          );
        }
      }
    );
  }

  login() {
    this.authService.login();
  }

  logout() {
    this.authService.logout();
  }

  callApi() {
    this.authService.callApi().subscribe(
      (data:any) => {
        console.log('data from api:', data);
      },
      err => {
        console.log('Api error:', err);
      }
    );
  }
}
