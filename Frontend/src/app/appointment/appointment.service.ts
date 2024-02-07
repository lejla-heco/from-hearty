import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class AppointmentService {
    api = {
        cardiologists: '/cardiologists',
        approvedAppointments: '/appointments/house-doctor/APPROVED',
        pendingAppointments: '/appointments/house-doctor/PENDING',
        approvedAppointmentsByCardiologistId: '/approved-appointments/cardiologist'
    }
}