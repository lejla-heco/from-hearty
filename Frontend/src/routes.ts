
import { Route } from '@angular/router';
import { LoginComponent } from './app/login/login.component';
import { HomePageComponent } from './app/home-page/home-page.component';
import { AiPredictionComponent } from './app/home-doctor/ai-prediction/ai-prediction.component';

export const routes: Route[] = [
    { path: '', component: LoginComponent },
    { path: 'home-page', component: HomePageComponent },
    { path: 'ai-prediction', component: AiPredictionComponent },
];