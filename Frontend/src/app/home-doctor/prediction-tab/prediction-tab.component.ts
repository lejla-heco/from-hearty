import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { AiPredictionComponent } from '../ai-prediction/ai-prediction.component';
import { TabType } from './models/tab.enum';
import { PatientOverviewComponent } from '../../patient/patient-overview/patient-overview.component';
import { PatientService } from '../../patient/patient.service';
import { AuthentificationHelper } from '../../authentification/authentification-helper';
import { RoleType } from '../../login/models/login-token.model';

@Component({
  selector: 'app-prediction-tab',
  templateUrl: './prediction-tab.component.html',
  styleUrl: './prediction-tab.component.css',
  standalone: true,
  imports: [SharedModule, AiPredictionComponent, PatientOverviewComponent]
})
export class PredictionTabComponent {
  patientsTab: boolean = true;
  aiPredictionTab: boolean = false;
  archivesTab: boolean = false;  
  roleType: RoleType = AuthentificationHelper.getLoginToken().roleType;

  constructor(public patientService: PatientService){

  }

  setTab(tab: string) : void{
    if (tab == TabType.Patients){
      this.patientsTab = true;
      this.aiPredictionTab = this.archivesTab = this.patientService.isPatientSelected = false;
    }
    if (tab == TabType.AiPrediction){
      this.aiPredictionTab = true;
      this.patientsTab = this.archivesTab = false;
    }
    if (tab == TabType.Archives){
      this.archivesTab = true;
      this.patientsTab = this.aiPredictionTab = false;
    }
  }
}
