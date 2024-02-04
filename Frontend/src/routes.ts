
import { Route } from '@angular/router';
import { LoginComponent } from './app/login/login.component';
import { HomePageComponent } from './app/home-page/home-page.component';
import { PatientExampleComponent } from './app/home-doctor/patient-example/patient-example.component';
import { AppointmentComponent } from './app/appointment/appointment.component';
import { CardiologistCalendarComponent } from './app/cardiologist/cardiologist-calendar/cardiologist-calendar.component';
import { PatientOverviewComponent } from './app/patient/patient-overview/patient-overview.component';
import { AiPredictionComponent } from './app/home-doctor/ai-prediction/ai-prediction.component';
import { ArchiveComponent } from './app/archive/archive.component';

export const routes: Route[] = [
    { path: '', component: LoginComponent },
    { path: 'home-page', component: HomePageComponent },
    { path: 'patient-example', component: PatientExampleComponent },
    { path: 'appointment', component: AppointmentComponent },
    { path: 'patients', component: PatientOverviewComponent },
    { path: 'calendar', component: CardiologistCalendarComponent},
    { path: 'ai-prediction', component: AiPredictionComponent},
    { path: 'archives', component: ArchiveComponent}
];