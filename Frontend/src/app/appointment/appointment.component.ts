import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AppointmentService } from './appointment.service';
import { Config } from '../configuration/config';
import { Cardiologist } from './models/cardiologist.model';
import { SharedModule } from '../.shared/shared.module';
import { CalendarComponent } from '../calendar/calendar.component';
import { PatientService } from '../patient/patient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Patient } from '../patient/models/patient.model';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrl: './appointment.component.css',
  standalone: true,
  imports: [SharedModule, CalendarComponent],
})
export class AppointmentComponent implements OnInit {
  @ViewChild(CalendarComponent, { static: true }) calendarComponent!: CalendarComponent;
  cardiologists: Cardiologist[] = [];
  selected: any;
  patient?: Patient;

  constructor(private httpClient: HttpClient, public appointmentService: AppointmentService, public patientService: PatientService,
    private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.handleQueryParams();
    this.getCardiologists();
  }

  
  private handleQueryParams(): void {
    this.route.queryParams.subscribe(params => {
      if (params['patientId']) {
        this.getPatient(params['patientId']);
      }
    });
  }

  getPatient(id: any): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients + '/' + id).subscribe((response: any) => {
      this.patient = response;
    });
  }

  getCardiologists(): void {
    this.httpClient.get(Config.serverAddress + this.appointmentService.api.cardiologists).subscribe((response: any) => {
      this.cardiologists = response;
      if (this.cardiologists.length > 0){
        this.selected = this.cardiologists[0].id;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['./patients']);
  }

}
