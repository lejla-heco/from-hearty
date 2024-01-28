import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { Patient } from '../../patient/models/patient.model';
import { CalendarComponent } from '../../calendar/calendar.component';
import { AuthentificationHelper } from '../../authentification/authentification-helper';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { PatientService } from '../../patient/patient.service';
import { Config } from '../../configuration/config';

@Component({
  selector: 'app-cardiologist-calendar',
  templateUrl: './cardiologist-calendar.component.html',
  styleUrl: './cardiologist-calendar.component.css',
  standalone: true,
  imports: [SharedModule, CalendarComponent]
})
export class CardiologistCalendarComponent {
  patients: Patient[] = [];
  userId: string = AuthentificationHelper.getLoginToken().userId;
  selected?: Patient;

  constructor(private httpClient: HttpClient, private toastr: ToastrService, public patientService: PatientService) {
    this.getPatients();
  }
  
  getPatients(): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients).subscribe((response: any) => {
      this.patients = response;
      this.patientService.selectedPatient = this.patients[0];
    });
  }

  getPatient(id: any): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients + '/' + id).subscribe((response: any) => {
      this.patientService.selectedPatient = response;
    });
  }

  onPatientSelectionChange(event: any): void {
    this.getPatient(event.target.value);
  }

}
