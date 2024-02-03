import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, signal } from '@angular/core';
import { SharedModule } from '../.shared/shared.module';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions, DateSelectArg, EventApi, EventClickArg } from '@fullcalendar/core';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import { CalendarService } from './calendar.service';
import { Config } from '../configuration/config';
import { HttpClient } from '@angular/common/http';
import { Appointment } from '../appointment/models/appointment.model';
import { ToastrService } from 'ngx-toastr';
import { PatientService } from '../patient/patient.service';
import { AuthentificationHelper } from '../authentification/authentification-helper';
import { RoleType } from '../login/models/login-token.model';
import { AiPredictionService } from '../home-doctor/ai-prediction/ai-prediction.service';
import { PredictionResult } from './models/prediction-result.model';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrl: './calendar.component.css',
  standalone: true,
  imports: [SharedModule, FullCalendarModule]
})
export class CalendarComponent implements OnChanges {
  @Input() id: any;
  appointments: Appointment[] = [];
  calendarVisible = false;
  calendarOptions = this.initCalendar();
  currentEvents = signal<EventApi[]>([]);
  roleType = AuthentificationHelper.getLoginToken().roleType;

  constructor(private changeDetector: ChangeDetectorRef, private calendarService: CalendarService, private httpClient: HttpClient,
    private toastr: ToastrService, public patientService: PatientService, private aiPredictionService: AiPredictionService) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['id'] && changes['id'].currentValue) {
      this.getAppointments();
      this.calendarVisible = true;
    } else {
      this.calendarVisible = false;
    }
  }

  getAppointments(): void {
    if (this.id) {
      this.httpClient.get(Config.serverAddress + this.calendarService.api.appointments + `\\${this.id}`).subscribe((response: any) => {
        this.appointments = response;
        this.calendarOptions = this.initCalendar();
      });
    }
  }

  handleDateSelect(selectInfo: DateSelectArg): void {
    const title = `${this.patientService.selectedPatient?.firstName} ${this.patientService.selectedPatient?.lastName}`;
    const calendarApi = selectInfo.view.calendar;

    calendarApi.unselect();

    let newAppointment = {
      patientId: this.patientService.selectedPatient?.id,
      title: title,
      start: selectInfo.startStr,
      end: selectInfo.endStr,
      allDay: selectInfo.allDay,
      cardiologistId: this.id
    }

    if (title) {
      calendarApi.addEvent({ newAppointment });
      this.createAppointment(newAppointment);
    }

  }

  createAppointment(newAppointment: any): void {
    if (this.id && this.validateCreate()) {
      this.httpClient.put(Config.serverAddress + this.calendarService.api.appointments, newAppointment).subscribe((response: any) => {
        this.displayRoleMessage();
        this.getAppointments();
        if (this.roleType == RoleType.Doctor) {
          this.createPredictionResult();
        }
      });
    }
  }

  createPredictionResult(): void {
    let predictionResult: PredictionResult = new PredictionResult(this.aiPredictionService.predictionRequest);
    predictionResult.patientId = this.patientService.selectedPatient!.id;
    predictionResult.houseDoctorId = AuthentificationHelper.getLoginToken().userId;
    predictionResult.cardiologistId = this.id;
    predictionResult.label = Math.round(this.aiPredictionService.prediction!);

    this.httpClient.put(Config.serverAddress + this.aiPredictionService.api.predictionResult, predictionResult).subscribe((response: any) => {
      this.toastr.success("The created prediction has been stored in the Archive!");
    });
  }

  validateCreate(): boolean {
    if (!this.patientService.selectedPatient && this.roleType == RoleType.Cardiolog) {
      this.toastr.error("To create an appointment, the patient must be selected!")
      return false;
    }
    return true;
  }

  deleteAppointment(id: any): void {
    if (this.id) {
      this.httpClient.delete(Config.serverAddress + this.calendarService.api.appointments + '/' + id).subscribe((response: any) => {
        this.toastr.success("Successfully deleted an appointment!");
        this.getAppointments();
      });
    }
  }

  displayRoleMessage(): void {
    if (this.roleType == RoleType.Doctor) {
      this.toastr.info("You do not have permission to edit the calendar.");
      this.toastr.success("Upon selecting the preferred time slot, an appointment request was successfully submitted.");
    }
    if (this.roleType == RoleType.Cardiolog) {
      this.toastr.success("Successfully booked an appointment!");
    }
  }

  handleEventClick(clickInfo: EventClickArg): void {
    if (confirm(`Are you sure you want to delete the event '${clickInfo.event.title}'`)) {
      clickInfo.event.remove();
      this.deleteAppointment(clickInfo.event.id);
    }
  }

  handleEvents(events: EventApi[]): void {
    this.currentEvents.set(events);
    this.changeDetector.detectChanges();
  }

  initCalendar(): any {
    return signal<CalendarOptions>({
      plugins: [
        interactionPlugin,
        dayGridPlugin,
        timeGridPlugin,
        listPlugin,
      ],
      headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
      },
      initialView: 'timeGridWeek',
      events: this.appointments,
      weekends: true,
      editable: false,
      selectable: true,
      selectMirror: true,
      dayMaxEvents: true,
      select: this.handleDateSelect.bind(this),
      eventClick: this.roleType == RoleType.Cardiolog ? this.handleEventClick.bind(this) : undefined,
      eventsSet: this.handleEvents.bind(this),
      height: "auto"
    });
  }

}
