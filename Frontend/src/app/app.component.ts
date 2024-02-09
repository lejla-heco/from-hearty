import { Component, DoCheck } from '@angular/core';
import { AuthentificationHelper } from './authentification/authentification-helper';
import { Router, RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { SharedModule } from './.shared/shared.module';
import { LoginService } from './login/login.service';
import { Config } from './configuration/config';
import { RoleType } from './login/models/login-token.model';
import { AccessibilityService } from './app.accessibilityservice';
import { AccessibilityServiceInit } from './app.accessibilityserviceinit';
import { DocumentService } from './documents/document.service';


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
  isAccessibilityOn = false;
  isSidebarOpen: boolean = false;
  roleType: RoleType = AuthentificationHelper.getLoginToken().roleType;

  constructor(
    private router: Router,
    private httpClient: HttpClient,
    private loginService: LoginService,
    private accessibilityService: AccessibilityService,
    private accessibilityServiceInit: AccessibilityServiceInit,
    private documentService: DocumentService
  ) { }

  ngDoCheck() {
    if (this.isAccessibilityOn == true) {
      this.accessibilityServiceInit.applySettings();
    }
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


  toggleAccessibilitySettings() {
    // Implement logic to toggle contrast, text color, and font size
    // Update the settings in the AccessibilityService
    this.isAccessibilityOn = !this.isAccessibilityOn;
    this.accessibilityService.applySettings();
  }

  isCardiolog(): boolean {
    return AuthentificationHelper.getLoginToken().roleType == RoleType.Cardiolog;
  }

  isHouseDoctor(): boolean {
    return AuthentificationHelper.getLoginToken().roleType == RoleType.Doctor;
  }

  downloadUserManual(): void {
    this.documentService.downloadUserManualDocument();
  }
}
