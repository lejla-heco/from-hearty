
import { Route } from '@angular/router';
import { LoginComponent } from './app/login/login.component';
import { HomePageComponent } from './app/home-page/home-page.component';
import { AiPredictionComponent } from './app/home-doctor/ai-prediction/ai-prediction.component';
import { CardiologExampleComponent } from './app/home-doctor/cardiolog-example/cardiolog-example.component';
import { PatientExampleComponent } from './app/home-doctor/patient-example/patient-example.component';

export const routes: Route[] = [
    { path: '', component: LoginComponent },
    { path: 'home-page', component: HomePageComponent },
    { path: 'ai-prediction', component: AiPredictionComponent },
    { path: 'cardiolog-example', component: CardiologExampleComponent },
    { path: 'patient-example', component: PatientExampleComponent },
];