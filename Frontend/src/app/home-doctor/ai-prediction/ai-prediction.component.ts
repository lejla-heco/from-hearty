import { Component } from '@angular/core';
import { PredictionRequest } from './models/prediction-request.model';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../configuration/config';
import { AiPredictionService } from './ai-prediction.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ai-prediction',
  templateUrl: './ai-prediction.component.html',
  styleUrl: './ai-prediction.component.css'
})
export class AiPredictionComponent {
  predictionRequest: PredictionRequest = new PredictionRequest();

  constructor(private httpClient: HttpClient, public aiPredictionService: AiPredictionService, private toastr: ToastrService) {
  }

  updateExangValue(event: any): void {
    this.predictionRequest.exang = event.target.checked ? 1 : 0;
  }

  clearPredictionForm(): void {
    this.predictionRequest = new PredictionRequest();
    this.toastr.info("Prediction form has been cleared!");
  }

  predict(): void {
    if (this.validateFields()) {
      this.httpClient.post(Config.serverAddress + this.aiPredictionService.api.predict, this.predictionRequest, Config.httpOptions()).subscribe((response: any) => {
        alert("The likelihood percentage of the patient suffering from cardiovascular disease is: " + response + "%");
      });
    }
  }

  validateFields(): any {
    if (this.predictionRequest.trestBps < 1 ||
      this.predictionRequest.chol < 1 ||
      this.predictionRequest.fbs < 1 ||
      this.predictionRequest.oldPeak < 1 ||
      this.predictionRequest.slope < 1 ||
      this.predictionRequest.ca < 1 ||
      this.predictionRequest.thal < 1) {
      this.toastr.error("Please ensure that numeric values are greater than or equal to 1!");
      return false;
    }
    return true;
  }
}
