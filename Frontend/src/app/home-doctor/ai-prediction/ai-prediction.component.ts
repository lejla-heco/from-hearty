// ai-prediction.component.ts
import { Component, OnChanges, SimpleChanges } from '@angular/core';
import { PredictionRequest } from './models/prediction-request.model';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../configuration/config';
import { AiPredictionService } from './ai-prediction.service';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../.shared/shared.module';
import { SliderComponent } from '../../.shared/slider/slider.component';
import { AppointmentComponent } from '../../appointment/appointment.component';
import { PatientService } from '../../patient/patient.service';

@Component({
  selector: 'app-ai-prediction',
  templateUrl: './ai-prediction.component.html',
  styleUrls: ['./ai-prediction.component.css'],
  standalone: true,
  imports: [SharedModule, SliderComponent, AppointmentComponent],
})
export class AiPredictionComponent {
  predictionMessage: string = '';
  appointment: boolean = false;
  loading: boolean = false;

  constructor(private httpClient: HttpClient, public aiPredictionService: AiPredictionService, private toastr: ToastrService,
    public patientService: PatientService) {
  }

  updateExangValue(event: any): void {
    this.aiPredictionService.predictionRequest.exang = event.target.checked ? 1 : 0;
  }

  clearPredictionForm(): void {
    this.aiPredictionService.predictionRequest = new PredictionRequest();
    this.toastr.info('Prediction form has been cleared!');
    this.predictionMessage = '';
    this.appointment = false;
  }

  predict(): void {
    if (this.validateFields()) {
      this.loading = true;
      this.httpClient
        .post(
          Config.serverAddress + this.aiPredictionService.api.predict,
          this.aiPredictionService.predictionRequest
        )
        .subscribe(
          (response: any) => {
            this.predictionMessage =
              'The likelihood percentage of the patient suffering from cardiovascular disease is: ' +
              response +
              '%';
            this.aiPredictionService.prediction = response;
            this.appointment = true;
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

  validateFields(): any {
    if (
      this.aiPredictionService.predictionRequest.trestBps < 1 ||
      this.aiPredictionService.predictionRequest.chol < 1 ||
      this.aiPredictionService.predictionRequest.fbs < 1 ||
      this.aiPredictionService.predictionRequest.oldPeak < 1 ||
      this.aiPredictionService.predictionRequest.slope < 1 ||
      this.aiPredictionService.predictionRequest.ca < 1 ||
      this.aiPredictionService.predictionRequest.thal < 1
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
