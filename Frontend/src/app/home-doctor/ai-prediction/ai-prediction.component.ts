import { Component } from '@angular/core';
import { PredictionRequest } from './models/prediction-request.model';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../configuration/config';
import { AiPredictionService } from './ai-prediction.service';

@Component({
  selector: 'app-ai-prediction',
  templateUrl: './ai-prediction.component.html',
  styleUrl: './ai-prediction.component.css'
})
export class AiPredictionComponent {
  predictionRequest: PredictionRequest = new PredictionRequest();

  constructor(private httpClient: HttpClient, public aiPredictionService: AiPredictionService) {
  }

  updateExangValue(event: any): void {
    this.predictionRequest.exang = event.target.checked ? 1 : 0;
  }

  predict(): void {
    this.httpClient.post(Config.serverAddress + this.aiPredictionService.api.predict, this.predictionRequest, Config.httpOptions()).subscribe((response: any) => {
      alert("The likelihood percentage of the patient suffering from cardiovascular disease is: " + response + "%");
    });
  }
}
