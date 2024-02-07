import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ContactsService {
    api = {
        cardiologists: '/cardiologists',
        houseDoctors: '/house-doctors',
    }
}