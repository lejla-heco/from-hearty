import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild, signal } from '@angular/core';
import { SharedModule } from '../.shared/shared.module';
import { FullCalendarComponent, FullCalendarModule } from '@fullcalendar/angular';
import { Calendar, CalendarOptions, DateSelectArg, EventApi, EventClickArg } from '@fullcalendar/core';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import { CalendarService } from './calendar.service';
import { Config } from '../configuration/config';
import { HttpClient } from '@angular/common/http';
import { Appointment } from '../appointment/models/appointment.model';
import { ToastrService } from 'ngx-toastr';

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

  constructor(private changeDetector: ChangeDetectorRef, private calendarService: CalendarService, private httpClient: HttpClient,
    private toastr: ToastrService) {
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
    const title = prompt('Please enter a new title for your event');
    const calendarApi = selectInfo.view.calendar;

    calendarApi.unselect();

    let newAppointment = {
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
    if (this.id) {
      this.httpClient.put(Config.serverAddress + this.calendarService.api.appointments, newAppointment).subscribe((response: any) => {
        this.getAppointments();
        this.toastr.success("Successfully booked an appointment");
      });
    }
  }

  handleEventClick(clickInfo: EventClickArg): void {
    /*if (confirm(`Are you sure you want to delete the event '${clickInfo.event.title}'`)) {
      clickInfo.event.remove();
    }*/
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
      eventClick: this.handleEventClick.bind(this),
      eventsSet: this.handleEvents.bind(this),
      height: "auto"
    });
  }

}
