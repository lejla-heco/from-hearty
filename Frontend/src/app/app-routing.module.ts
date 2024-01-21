import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AiPredictionComponent } from './home-doctor/ai-prediction/ai-prediction.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home-page', component: HomePageComponent },
  { path: 'ai-prediction', component: AiPredictionComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
