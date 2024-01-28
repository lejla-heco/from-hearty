import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AppointmentService } from './appointment.service';
import { Config } from '../configuration/config';
import { Cardiologist } from './models/cardiologist.model';
import { SharedModule } from '../.shared/shared.module';
import { CalendarComponent } from '../calendar/calendar.component';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrl: './appointment.component.css',
  standalone: true,
  imports: [SharedModule, CalendarComponent],
})
export class AppointmentComponent implements OnInit{
  @ViewChild(CalendarComponent, { static: true }) calendarComponent!: CalendarComponent;
  cardiologists: Cardiologist[] = [];
  selected: any;

  constructor(private httpClient: HttpClient, public appointmentService: AppointmentService) {
  }

  ngOnInit(): void {
    this.getCardiologists();
  }

  getCardiologists(): void {
    this.httpClient.get(Config.serverAddress + this.appointmentService.api.cardiologists).subscribe((response: any) => {
      this.cardiologists = response;
    });
  }

}
