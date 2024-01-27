import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Config } from '../configuration/config';
import { HomePageService } from './home-page.service';
import { SharedModule } from '../.shared/shared.module';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
  standalone: true,
  imports: [
    SharedModule,
  ]
})
export class HomePageComponent {
  constructor(private httpClient: HttpClient, public homePageService: HomePageService) {
    this.getData();
  }

  getData() {
    // Test call for authentication and authorization
    this.httpClient.get(Config.serverAddress + this.homePageService.api.patients).subscribe((response: any) => {
      console.log(response);
    });
  }
}
