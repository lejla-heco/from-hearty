import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class HomePageService {
    //additional calls designed for the home page will be added to the API later
    api = {
        patients: '/patients',
    }
}