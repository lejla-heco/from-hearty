import { Component } from '@angular/core';
import { SharedModule } from '../../../../.shared/shared.module';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { Router } from '@angular/router';
import { Patient } from '../../../../patient/models/patient.model';
import { AiPredictionService } from '../../ai-prediction.service';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../../../configuration/config';
import { PredictionRequest } from '../../models/prediction-request.model';

@Component({
  selector: 'app-results-popup',
  templateUrl: './results-popup.component.html',
  styleUrl: './results-popup.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class ResultsPopupComponent {
  predictionMessage!: string;
  patient?: Patient;
  loading: boolean = false;
  constructor(private httpClient: HttpClient, public modalRefResults: MdbModalRef<ResultsPopupComponent>, 
    private router: Router, private aiPredictionService: AiPredictionService) {

  }

  bookAnAppointment(): void {
    this.router.navigate(['/appointment'], { queryParams: { patientId: this.patient?.id } });
    this.modalRefResults.close();
  }

  closeModalAndClearForm(): void {
    // Close the modal
    this.modalRefResults.close();
  }

  predictWithOpenAI() {
    this.loading = true;
    let request: any = this.aiPredictionService.predictionRequest;
    request.percentage = this.aiPredictionService.prediction;
    this.httpClient
      .post(
        Config.serverAddress + this.aiPredictionService.api.openAiPredict,
        this.aiPredictionService.predictionRequest
      )
      .subscribe(
        (response: any) => {
          this.predictionMessage = response.choices[0].text;
        },
        (error) => {
          this.predictionMessage = 'An error occurred while predicting with Open AI.';
        }
      )
      .add(() => (this.loading = false));
  }
}
