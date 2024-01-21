import { Component, OnInit } from '@angular/core';
import { LoginRequest } from './models/login-request.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Config } from '../configuration/config';
import { AuthentificationHelper } from '../authentification/authentification-helper';
import { LoginService } from './login.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  loginRequest: LoginRequest = new LoginRequest();

  constructor(private httpClient: HttpClient, private router: Router, public loginService: LoginService,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {
    if (AuthentificationHelper.getLoginToken().id) {
      this.router.navigateByUrl('ai-prediction');
    };
  }

  login(): void {
    this.httpClient.post(Config.serverAddress + this.loginService.api.login, this.loginRequest).subscribe((response: any) => {
      if (response) {
        AuthentificationHelper.setLoginToken(response);
        AuthentificationHelper.isLoggedIn(); 
        this.router.navigateByUrl("ai-prediction");
      } else {
        this.toastr.error("Invalid credentials. Please check your login information.");
      }
    });
  }
}
