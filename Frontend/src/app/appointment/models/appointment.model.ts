export class Appointment {
  id?: string;
  title?: string;
  start!: Date;
  end!: Date;
  allDay!: boolean;
  cardiologistId!: string;
  patientId?: string;
  description?: string;
  approved?: boolean;
}