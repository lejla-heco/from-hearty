import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';

@Component({
  selector: 'app-cardiologist-dashboard',
  templateUrl: './cardiologist-dashboard.component.html',
  styleUrl: './cardiologist-dashboard.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class CardiologistDashboardComponent {

}
