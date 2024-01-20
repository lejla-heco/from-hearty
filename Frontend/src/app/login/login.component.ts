import { Component } from '@angular/core';
import { LoginRequest } from './models/login-request.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Config } from '../configuration/config';
import { AuthentificationHelper } from '../authentification/authentification-helper';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginRequest: LoginRequest = new LoginRequest();

  constructor(private httpClient: HttpClient, private router: Router, public loginService: LoginService) {
  }

  login(): void {
    this.httpClient.post(Config.serverAddress + this.loginService.api.login, this.loginRequest).subscribe((response: any) => {
      AuthentificationHelper.setLoginToken(response);
      this.router.navigateByUrl("home-page");
    });
  }
}
