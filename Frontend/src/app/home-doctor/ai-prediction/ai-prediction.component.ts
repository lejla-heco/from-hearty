// ai-prediction.component.ts
import { Component, OnChanges, SimpleChanges } from '@angular/core';
import { PredictionRequest } from './models/prediction-request.model';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../configuration/config';
import { AiPredictionService } from './ai-prediction.service';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../.shared/shared.module';
import { SliderComponent } from '../../.shared/slider/slider.component';

@Component({
  selector: 'app-ai-prediction',
  templateUrl: './ai-prediction.component.html',
  styleUrls: ['./ai-prediction.component.css'],
  standalone: true,
  imports: [SharedModule, SliderComponent],
})
export class AiPredictionComponent {
  predictionRequest: PredictionRequest = new PredictionRequest();
  predictionMessage: string = '';
  loading: boolean = false; 

  constructor(
    private httpClient: HttpClient,
    public aiPredictionService: AiPredictionService,
    private toastr: ToastrService
  ) {
  }

  updateExangValue(event: any): void {
    this.predictionRequest.exang = event.target.checked ? 1 : 0;
  }

  clearPredictionForm(): void {
    this.predictionRequest = new PredictionRequest();
    this.toastr.info('Prediction form has been cleared!');
    this.predictionMessage = '';
  }

  predict(): void {
    if (this.validateFields()) {
      this.loading = true; 
      this.httpClient
        .post(
          Config.serverAddress + this.aiPredictionService.api.predict,
          this.predictionRequest
        )
        .subscribe(
          (response: any) => {
            this.predictionMessage =
              'The likelihood percentage of the patient suffering from cardiovascular disease is: ' +
              response +
              '%';
          },
          (error) => {
            this.predictionMessage = 'An error occurred while predicting.';
          }
        )
        .add(() => (this.loading = false)); 
    }
  }

  predictWithOpenAI(): void {
    if (this.validateFields()) {
      this.loading = true; 
      this.httpClient
        .post(
          Config.serverAddress + this.aiPredictionService.api.openAiPredict,
          this.predictionRequest
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

  validateFields(): any {
    if (
      this.predictionRequest.trestBps < 1 ||
      this.predictionRequest.chol < 1 ||
      this.predictionRequest.fbs < 1 ||
      this.predictionRequest.oldPeak < 1 ||
      this.predictionRequest.slope < 1 ||
      this.predictionRequest.ca < 1 ||
      this.predictionRequest.thal < 1
    ) {
      this.toastr.error(
        'Please ensure that numeric values are greater than or equal to 1!'
      );
      this.predictionMessage = '';
      return false;
    }
    return true;
  }
}
