import { Component } from '@angular/core';
import { SharedModule } from '../../../../.shared/shared.module';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { Router } from '@angular/router';
import { Patient } from '../../../../patient/models/patient.model';
import { AiPredictionService } from '../../ai-prediction.service';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../../../configuration/config';

@Component({
  selector: 'app-results-popup',
  templateUrl: './results-popup.component.html',
  styleUrl: './results-popup.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class ResultsPopupComponent {
  predictionMessages: string[] = [''];
  patient?: Patient;
  isUsingOpenAI: boolean = false;
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
    this.isUsingOpenAI = true;
    let request: any = this.aiPredictionService.predictionRequest;
    request.percentage = this.aiPredictionService.prediction;
    this.httpClient
      .post(
        Config.serverAddress + this.aiPredictionService.api.openAiPredict,
        this.aiPredictionService.predictionRequest
      )
      .subscribe(
        (response: any) => {
          this.predictionMessages.push(response.choices[0].text);
        },
        (error) => {
          this.predictionMessages[0] = 'An error occurred while predicting with Open AI.';
          for (let i = 1; i < this.predictionMessages.length; i++) {
            this.predictionMessages[i] = '';
          }
        }
      )
      .add(() => (this.loading = false));
  }
}
