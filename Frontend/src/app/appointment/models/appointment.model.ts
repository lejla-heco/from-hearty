export class Appointment {
  id?: string;
  title?: string;
  start!: Date | string;
  end!: Date | string;
  allDay!: boolean;
  cardiologistId!: string;
  patientId?: string;
  description?: string;
  approved?: boolean;
}