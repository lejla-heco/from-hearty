
import { Route } from '@angular/router';
import { LoginComponent } from './app/login/login.component';
import { HomePageComponent } from './app/home-page/home-page.component';
import { AiPredictionComponent } from './app/home-doctor/ai-prediction/ai-prediction.component';
import { CardiologExampleComponent } from './app/home-doctor/cardiolog-example/cardiolog-example.component';
import { PatientExampleComponent } from './app/home-doctor/patient-example/patient-example.component';
import { AppointmentComponent } from './app/appointment/appointment.component';
import { CalendarComponent } from './app/calendar/calendar.component';
import { PredictionTabComponent } from './app/home-doctor/prediction-tab/prediction-tab.component';

export const routes: Route[] = [
    { path: '', component: LoginComponent },
    { path: 'home-page', component: HomePageComponent },
    { path: 'cardiolog-example', component: CardiologExampleComponent },
    { path: 'patient-example', component: PatientExampleComponent },
    { path: 'appointment', component: AppointmentComponent },
    { path: 'prediction-tab', component: PredictionTabComponent }
];