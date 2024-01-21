import { Component, DoCheck, OnInit } from '@angular/core';
import { LoginService } from './login/login.service';
import { AuthentificationHelper } from './authentification/authentification-helper';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
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
