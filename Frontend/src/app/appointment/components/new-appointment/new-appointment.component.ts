import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedModule } from '../../../.shared/shared.module';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { NgbModule, NgbDatepickerModule, NgbTimepickerModule, NgbDate, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Appointment } from '../../models/appointment.model';
import { AuthentificationHelper } from '../../../authentification/authentification-helper';
import { RoleType } from '../../../login/models/login-token.model';
import { ConfirmationModalComponent } from '../popups/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-new-appointment',
  templateUrl: './new-appointment.component.html',
  styleUrl: './new-appointment.component.css',
  standalone: true,
  imports: [SharedModule, NgbModule, NgbDatepickerModule, NgbTimepickerModule],
  providers: [MdbModalService]
})
export class NewAppointmentComponent implements OnInit {
  public appointmentInfo?: any;
  isEdit: boolean = false;
  appointments: Appointment[] = [];
  startDateModel: NgbDateStruct = { year: 0, month: 0, day: 0 };
  endDateModel: NgbDateStruct = { year: 0, month: 0, day: 0 };
  startTime = {};
  endTime = {};
  modalRefConfirmation: MdbModalRef<ConfirmationModalComponent> | null = null;
  approveMessage: string = 'Are you sure you want to approve the appointment?';

  constructor(public modalRef: MdbModalRef<NewAppointmentComponent>, public toastr: ToastrService,
    private modalService: MdbModalService) { }

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

  validateDateModels(dateModel: NgbDateStruct, time: any): boolean {
    if (dateModel == null ||
      dateModel.day == null ||
      dateModel.year == null ||
      time == null ||
      time.hour == null ||
      time.minute == null) return false;
    return true;
  }

  formatDateWithoutTime(dateModel: NgbDateStruct): Date {
    let date = new Date();
    date.setDate(dateModel.day);
    date.setMonth(dateModel.month - 1);
    date.setFullYear(dateModel.year);
    return date;
  }

  saveClicked(): void {
    if (AuthentificationHelper.getLoginToken().roleType == RoleType.Doctor)
      this.save();
    else if (AuthentificationHelper.getLoginToken().roleType == RoleType.Cardiolog && !this.appointmentInfo.id) {
      this.openApprove('Would you like to approve the newly created appointment?');
    }
    else if (AuthentificationHelper.getLoginToken().roleType == RoleType.Cardiolog && this.appointmentInfo.id) {
      this.save();
    }
  }

  save(): void {
    if (!this.validateFields()) return;
    this.appointmentInfo.start = this.formatDate(this.startDateModel, this.startTime);
    this.appointmentInfo.end = this.formatDate(this.endDateModel, this.endTime);
    if (this.validateDates() && this.validateAppointments()) {
      this.appointmentInfo.start.setHours(this.appointmentInfo.start.getHours() + 1);
      this.appointmentInfo.end.setHours(this.appointmentInfo.end.getHours() + 1);
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

  isVisibleApprove(): boolean {
    return this.isVisible() && !this.appointmentInfo.approved && this.appointmentInfo.id;
  }

  isVisibleCancel(): boolean {
    return this.isVisible() && this.appointmentInfo.id;
  }

  isVisible(): boolean {
    return AuthentificationHelper.getLoginToken().roleType == RoleType.Cardiolog
  }

  openApprove(message: string = this.approveMessage): void {
    let confirmationInfo: any = {
      title: 'Approve the Appointment',
      question: message,
      action: 'Approve',
      realise: false
    };
    this.openConfirmationModal(confirmationInfo);
  }

  openCancel(): void {
    let confirmationInfo: any = {
      title: 'Cancel the Appointment',
      question: 'Are you sure you want to cancel the appointment?',
      action: 'Cancel',
      realise: false
    };
    this.openConfirmationModal(confirmationInfo);
  }

  approve(): void {
    this.appointmentInfo.approved = true;
    this.save();
  }

  cancel(): void {
    this.appointmentInfo.delete = true;
    this.modalRef.close(this.appointmentInfo);
  }

  openConfirmationModal(confirmationInfo: any): any {
    this.modalRefConfirmation = this.modalService.open(ConfirmationModalComponent, {
      modalClass: 'modal-sm',
      data: { confirmationInfo: confirmationInfo },
    });
    this.modalRefConfirmation.onClose.subscribe((confirmationInfo: any) => {
      if (confirmationInfo && confirmationInfo.realise && confirmationInfo.action === 'Approve') this.approve();
      else if (confirmationInfo && !confirmationInfo.realise && confirmationInfo.action === 'Approve') this.save();
      else if (confirmationInfo && confirmationInfo.realise && confirmationInfo.action === 'Cancel') this.cancel();
    });
  }

  validateFields(): boolean {
    if (
      this.appointmentInfo.title == undefined ||
      this.appointmentInfo.title == null ||
      this.appointmentInfo.title.trim().length === 0
    ) {
      this.toastr.warning("Title is a mandatory field!");
      return false;
    } else if (!this.validateDateModels(this.startDateModel, this.startTime) ||
      !this.validateDateModels(this.endDateModel, this.endTime)
    ) {
      this.toastr.warning("Dates are mandatory fields!");
      return false;
    }
    return true;
  }

}
