import { Component, OnInit } from '@angular/core';
import { LoginRequest } from './models/login-request.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Config } from '../configuration/config';
import { AuthentificationHelper } from '../authentification/authentification-helper';
import { LoginService } from './login.service';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../.shared/shared.module';
import { LoginToken, RoleType } from './models/login-token.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true,
  imports: [
    SharedModule
  ]
})
export class LoginComponent implements OnInit {
  loginRequest: LoginRequest = new LoginRequest();
  loginToken!: LoginToken;

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    public loginService: LoginService,
    private toastr: ToastrService
  ) {
  }

  navigateToDefaultRoute(loginToken: LoginToken) {
    if (loginToken.id) {
      if (RoleType.Doctor == loginToken.roleType)
        this.router.navigateByUrl('patients');
      else if (RoleType.Cardiolog == loginToken.roleType)
        this.router.navigateByUrl('calendar');
      else
        this.router.navigateByUrl('home-page');
    };
  }

  ngOnInit(): void {
    this.loginToken = AuthentificationHelper.getLoginToken();
    this.navigateToDefaultRoute(this.loginToken);
  }

  login(): void {
    this.httpClient.post<LoginToken | null>(Config.serverAddress + this.loginService.api.login, this.loginRequest).subscribe(response => {
      if (response) {
        this.loginToken = response;
        AuthentificationHelper.setLoginToken(response);
        AuthentificationHelper.isLoggedIn();
        this.navigateToDefaultRoute(this.loginToken);
      } else {
        this.toastr.error("Invalid credentials. Please check your login information.");
      }
    });
  }
}
