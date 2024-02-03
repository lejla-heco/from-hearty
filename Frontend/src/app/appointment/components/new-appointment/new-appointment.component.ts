import { Component } from '@angular/core';
import { SharedModule } from '../../../.shared/shared.module';

@Component({
  selector: 'app-new-appointment',
  templateUrl: './new-appointment.component.html',
  styleUrl: './new-appointment.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class NewAppointmentComponent {

}
