import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class CalendarService {
    api = {
        appointments: '/appointments',
        appointment: '/appointment'
    }
}