import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';

@Component({
  templateUrl: './patient-example.component.html',
  styleUrls: ['./patient-example.component.css'],
  standalone: true,
  imports: [SharedModule],
})
export class PatientExampleComponent {

}
