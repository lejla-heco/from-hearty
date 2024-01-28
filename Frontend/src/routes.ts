
import { Route } from '@angular/router';
import { LoginComponent } from './app/login/login.component';
import { HomePageComponent } from './app/home-page/home-page.component';
import { PatientExampleComponent } from './app/home-doctor/patient-example/patient-example.component';
import { AppointmentComponent } from './app/appointment/appointment.component';
import { PredictionTabComponent } from './app/home-doctor/prediction-tab/prediction-tab.component';
import { CardiologistCalendarComponent } from './app/cardiologist/cardiologist-calendar/cardiologist-calendar.component';

export const routes: Route[] = [
    { path: '', component: LoginComponent },
    { path: 'home-page', component: HomePageComponent },
    { path: 'patient-example', component: PatientExampleComponent },
    { path: 'appointment', component: AppointmentComponent },
    { path: 'prediction-tab', component: PredictionTabComponent },
    { path: 'calendar', component: CardiologistCalendarComponent}
];