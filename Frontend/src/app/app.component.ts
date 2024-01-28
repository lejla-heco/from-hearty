import { Component, DoCheck } from '@angular/core';
import { AuthentificationHelper } from './authentification/authentification-helper';
import { Router, RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { SharedModule } from './.shared/shared.module';
import { LoginService } from './login/login.service';
import { Config } from './configuration/config';

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
})
export class AppComponent implements DoCheck {
  title = 'FromHearty';
  isLoggedIn: boolean = false;
  isSidebarOpen: boolean = false;

  constructor(
    private router: Router,
    private httpClient: HttpClient,
    private loginService: LoginService,
  ) { }

  ngDoCheck() {
    this.checkLoginStatus();
  }

  checkLoginStatus() {
    this.isLoggedIn = AuthentificationHelper.isLoggedIn();
  }

  logout(): void {
    this.httpClient
      .get<boolean>(Config.serverAddress + this.loginService.api.logout)
      .subscribe(response => {
        console.log('Logout API was successful? ', response);
        AuthentificationHelper.clearLocalStorage();
        this.checkLoginStatus();
        this.router.navigate(['']);
      })
  }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
}
