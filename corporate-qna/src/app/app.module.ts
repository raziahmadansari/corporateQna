import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';
import { NgxEditorModule } from 'ngx-editor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Constants } from './shared/constants';
import { InterceptorService } from './shared/interceptor.service';
import { QnaComponent } from './qna/qna.component';
import { HomeComponent } from './qna/home/home.component';
import { CategoriesComponent } from './qna/categories/categories.component';
import { UsersComponent } from './qna/users/users.component';
import { DateAgoPipe } from './pipes/date-ago.pipe';
import { UserComponent } from './qna/user/user.component';

export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
      oidcConfigService.withConfig({
          stsServer: Constants.isAuthority,
          redirectUrl: window.location.origin,
          postLogoutRedirectUri: window.location.origin,
          clientId: Constants.clientId,
          scope: 'openid profile api',
          responseType: 'code',
          silentRenew: false,
          silentRenewUrl: `${window.location.origin}/silent-renew.html`,
          logLevel: LogLevel.Debug,
      });
}

@NgModule({
  declarations: [
    AppComponent,
    QnaComponent,
    HomeComponent,
    CategoriesComponent,
    UsersComponent,
    DateAgoPipe,
    UserComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    NgxEditorModule,
    NgbModule,
  ],
  providers: [
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
