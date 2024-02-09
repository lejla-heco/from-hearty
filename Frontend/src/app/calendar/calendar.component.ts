import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges, signal } from '@angular/core';
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
import { Patient } from '../patient/models/patient.model';
import { NewAppointmentComponent } from '../appointment/components/new-appointment/new-appointment.component';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrl: './calendar.component.css',
  standalone: true,
  imports: [SharedModule, FullCalendarModule],
  providers: [MdbModalService]
})
export class CalendarComponent implements OnChanges {
  @Input() cardiologistId: any;
  @Input() patient?: Patient;
  appointments: Appointment[] = [];
  appointment!: Appointment;
  editPatient!: Patient;
  calendarVisible = false;
  calendarOptions = this.initCalendar();
  currentEvents = signal<EventApi[]>([]);
  roleType = AuthentificationHelper.getLoginToken().roleType;
  modalRef: MdbModalRef<NewAppointmentComponent> | null = null;
  currentEventsArray: EventApi[] = [];

  constructor(private changeDetector: ChangeDetectorRef, private calendarService: CalendarService, private httpClient: HttpClient,
    private toastr: ToastrService, public patientService: PatientService, private aiPredictionService: AiPredictionService,
    private modalService: MdbModalService) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.getAppointments();
    this.calendarVisible = true;
  }

  getAppointments(): void {
    if (this.cardiologistId) {
      this.httpClient.get(Config.serverAddress + this.calendarService.api.appointments + `\\${this.cardiologistId}`).subscribe((response: any) => {
        this.appointments = response;
        this.calendarOptions = this.initCalendar();
      });
    }
  }

  getAppointmentById(id: any): void {
    if (this.cardiologistId) {
      this.httpClient.get(Config.serverAddress + this.calendarService.api.appointment + `\\${id}`).subscribe((response: any) => {
        if (response.approved && this.roleType == RoleType.Doctor) {
          this.toastr.warning("You cannot edit an appointment that has been approved by the cardiologist!");
        } else {
          this.appointment = response;
          this.getPatient(this.appointment.patientId, id);
        }
      });
    }
  }

  getPatient(id: any, appointmentId: any): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients + '/' + id).subscribe((response: any) => {
      this.editPatient = response;
      let appointment = {
        id: appointmentId,
        title: `${this.editPatient?.firstName} ${this.editPatient?.lastName}`,
        patientName: `${this.editPatient?.firstName} ${this.editPatient?.lastName}`,
        patientId: this.editPatient?.id,
        start: this.appointment.start,
        end: this.appointment.end,
        allDay: this.appointment.allDay,
        cardiologistId: this.cardiologistId,
        description: this.appointment.description,
        approve: false,
        delete: false,
        approved: this.appointment.approved
      }
      this.openModal(appointment, true);
    });
  }

  handleDateSelect(selectInfo: DateSelectArg): void {
    let appointment = {
      title: `${this.patient?.firstName} ${this.patient?.lastName}`,
      patientName: `${this.patient?.firstName} ${this.patient?.lastName}`,
      patientId: this.patient?.id,
      start: selectInfo.startStr,
      end: selectInfo.endStr,
      allDay: selectInfo.allDay,
      cardiologistId: this.cardiologistId,
      approve: false,
      delete: false,
      approved: false,
    }
    this.openModal(appointment);
  }

  openModal(appointment: any, isEdit = false): any {
    this.modalRef = this.modalService.open(NewAppointmentComponent, {
      //modalClass: 'modal-lg',
      data: { appointmentInfo: appointment, appointments: this.appointments, isEdit: isEdit },
    });
    this.modalRef.onClose.subscribe((appointmentInfo: any) => {
      if (appointmentInfo) {
        if (appointmentInfo.delete && isEdit) {
          this.deleteAppointment(appointmentInfo.id);
        } else {
          if (!isEdit) this.createOrUpdateAppointment(appointmentInfo);
          if (isEdit) this.createOrUpdateAppointment(appointmentInfo, true);
        }
      }
    });
  }

  createOrUpdateAppointment(newAppointment: any, isEdit = false): void {
    if (!newAppointment) return;
    let request: Appointment = {
      id: newAppointment.id,
      title: newAppointment.title,
      start: new Date(newAppointment.start),
      end: new Date(newAppointment.end),
      allDay: newAppointment.allDay,
      cardiologistId: newAppointment.cardiologistId,
      patientId: newAppointment.patientId,
      description: newAppointment.description,
      approved: newAppointment.approved
    };
    let path = !isEdit ? this.calendarService.api.appointments : this.calendarService.api.appointment;
    this.httpClient.put(Config.serverAddress + path, request).subscribe((response: any) => {
      if (!isEdit) this.toastr.success("Successfully booked an appointment!");
      else this.toastr.success("Successfully updated the appointment!");
      this.getAppointments();
    });
  }

  deleteAppointment(id: any): void {
    if (this.cardiologistId) {
      this.httpClient.delete(Config.serverAddress + this.calendarService.api.appointments + '/' + id).subscribe((response: any) => {
        this.toastr.success("Successfully deleted an appointment!");
        this.getAppointments();
      });
    }
  }

  handleEventClick(clickInfo: EventClickArg): void {
    this.getAppointmentById(clickInfo.event.id);
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
      editable: false,
      selectable: true,
      selectMirror: true,
      dayMaxEvents: true,
      slotMinTime: '08:00:00',
      slotMaxTime: '17:00:00',
      select: this.handleDateSelect.bind(this),
      eventClick: this.handleEventClick.bind(this),
      eventsSet: this.handleEvents.bind(this),
      height: "auto",
    });
  }

  findAppointmentById(id: string): any {
    return this.appointments.find(appointment => appointment.id === id);
  }
}
