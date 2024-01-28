import { Injectable } from "@angular/core";
import { Patient } from "./models/patient.model";

@Injectable({
    providedIn: 'root'
})
export class PatientService {
    isPatientSelected: boolean = false;
    selectedPatient?: Patient;

    api = {
        patients: '/patients',
    }
}