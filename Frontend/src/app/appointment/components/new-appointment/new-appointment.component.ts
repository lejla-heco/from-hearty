import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedModule } from '../../../.shared/shared.module';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { NgbModule, NgbDatepickerModule, NgbTimepickerModule, NgbDate, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Appointment } from '../../models/appointment.model';
import { AuthentificationHelper } from '../../../authentification/authentification-helper';
import { RoleType } from '../../../login/models/login-token.model';

@Component({
  selector: 'app-new-appointment',
  templateUrl: './new-appointment.component.html',
  styleUrl: './new-appointment.component.css',
  standalone: true,
  imports: [SharedModule, NgbModule, NgbDatepickerModule, NgbTimepickerModule]
})
export class NewAppointmentComponent implements OnInit {
  public appointmentInfo?: any;
  isEdit: boolean = false;
  appointments: Appointment[] = [];
  startDateModel: NgbDateStruct = { year: 0, month: 0, day: 0 };
  endDateModel: NgbDateStruct = { year: 0, month: 0, day: 0 };
  startTime = {};
  endTime = {};
  constructor(public modalRef: MdbModalRef<NewAppointmentComponent>, public toastr: ToastrService) { }

  ngOnInit(): void {
    this.formatDateStruct(new Date(this.appointmentInfo.start), this.startDateModel);
    this.formatDateStruct(new Date(this.appointmentInfo.end), this.endDateModel);
    this.formatTime(new Date(this.appointmentInfo.start), this.startTime);
    this.formatTime(new Date(this.appointmentInfo.end), this.endTime);
  }

  formatDateStruct(date: Date, dateModel: NgbDateStruct): void {
    dateModel.day = date.getDate();
    dateModel.month = date.getMonth() + 1;
    dateModel.year = date.getFullYear();
  }

  formatTime(date: Date, time: any): void {
    time.hour = date.getHours();
    time.minute = date.getMinutes();
  }

  formatDate(dateModel: NgbDateStruct, time: any): Date {
    let date = new Date();
    date.setDate(dateModel.day);
    date.setMonth(dateModel.month - 1);
    date.setFullYear(dateModel.year);
    date.setHours(time.hour);
    date.setMinutes(time.minute);
    date.setSeconds(0);
    date.setMilliseconds(0);
    return date;
  }

  formatDateWithoutTime(dateModel: NgbDateStruct): Date {
    let date = new Date();
    date.setDate(dateModel.day);
    date.setMonth(dateModel.month - 1);
    date.setFullYear(dateModel.year);
    return date;
  }

  save() {
    this.appointmentInfo.start = this.formatDate(this.startDateModel, this.startTime);
    this.appointmentInfo.end = this.formatDate(this.endDateModel, this.endTime);
    if (this.validateDates() && this.validateAppointments()) {
      this.appointmentInfo.start.setHours(this.appointmentInfo.start.getHours() + 1);
      this.appointmentInfo.end.setHours(this.appointmentInfo.end.getHours() + 1);
      this.appointmentInfo.approved = false;
      this.modalRef.close(this.appointmentInfo);
    }
  }

  validateDates(): boolean {
    if (this.formatDate(this.startDateModel, this.startTime).getTime() > this.formatDate(this.endDateModel, this.endTime).getTime()) {
      this.toastr.error("The start time of the appointment must be greater than the end time!");
      return false;
    }
    if (this.formatDate(this.startDateModel, this.startTime).getTime() == this.formatDate(this.endDateModel, this.endTime).getTime()) {
      this.toastr.error("The start and end times of the appointment cannot be the same!");
      return false;
    }
    if (this.formatDateWithoutTime(this.startDateModel).getTime() != this.formatDateWithoutTime(this.endDateModel).getTime()) {
      this.toastr.error("Appointment should be completed in one day and should not extend to the following days!");
      return false;
    }
    return true;
  }

  validateAppointments(): boolean {
    let hasDuplicateAppointment = this.appointments.find(appointment =>
      (((this.appointmentInfo.start.getTime() >= (new Date(appointment.start)).getTime() &&
        this.appointmentInfo.start.getTime() < (new Date(appointment.end)).getTime()) ||
        (this.appointmentInfo.end.getTime() > (new Date(appointment.start)).getTime() &&
          this.appointmentInfo.end.getTime() <= (new Date(appointment.end)).getTime())
      ) ||
        (((new Date(appointment.start)).getTime() >= this.appointmentInfo.start.getTime() &&
          (new Date(appointment.start)).getTime() < this.appointmentInfo.end.getTime()) ||
          ((new Date(appointment.end)).getTime() > this.appointmentInfo.start.getTime() &&
            (new Date(appointment.end)).getTime() <= this.appointmentInfo.end.getTime())
        )) &&
      appointment.cardiologistId === this.appointmentInfo.cardiologistId &&
      appointment.id != this.appointmentInfo.id
    );
    if (hasDuplicateAppointment) {
      this.toastr.error("The selected appointment slot is already taken or overlaps with an existing one!");
      return false;
    }
    return true;
  }

  isVisible(): boolean {
    return AuthentificationHelper.getLoginToken().roleType == RoleType.Cardiolog;
  }
}
