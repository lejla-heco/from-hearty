import { Component, DoCheck } from '@angular/core';
import { AuthentificationHelper } from './authentification/authentification-helper';
import { Router, RouterOutlet } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './authentification/auth.interceptor';
import { LoginComponent } from './login/login.component';
import { SharedModule } from './.shared/shared.module';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [
    SharedModule,
    RouterOutlet,
    LoginComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ]
})
export class AppComponent implements DoCheck{
  title = 'FromHearty';
  isLoggedIn: boolean = false;

  constructor(private router: Router) { }

  ngDoCheck() {
    this.checkLoginStatus();
  }

  checkLoginStatus() {
    this.isLoggedIn = AuthentificationHelper.isLoggedIn();
  }

  logout(): void {
    AuthentificationHelper.clearLocalStorage();
    this.checkLoginStatus();
    this.router.navigate(['']);
  }
}
