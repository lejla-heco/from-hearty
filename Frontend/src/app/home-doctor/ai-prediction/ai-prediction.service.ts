import { Injectable } from "@angular/core";
import { PredictionRequest } from "./models/prediction-request.model";

@Injectable({
    providedIn: 'root'
})
export class AiPredictionService {
    predictionRequest: PredictionRequest = new PredictionRequest();
    prediction?: number;

    cpOptions = [
        { value: 1, label: 'typical angina' }, { value: 2, label: 'atypical angina' }, { value: 3, label: 'non-anginal pain' }, { value: 4, label: 'asymptomatic' },
    ];

    restEcgOptions = [
        { value: 0, label: 'normal' },
        { value: 1, label: 'having ST-T wave abnormality' },
        { value: 2, label: 'showing probable or definite left ventricular hypertrophy by Estes\' criteria' },
    ];

    thal = [
        { value: 0, label: 'No' },
        { value: 1, label: 'Yes' },
    ];

    trestBps  = [
        { value: 1, label: 'Optimal pressure (< 120/80 mmHg)' },
        { value: 2, label: 'Normal pressure (120/80 mmHg)'},
        { value: 3, label: 'Normal high blood pressure (130-139/85-89 mmHg)' },
        { value: 4, label: 'Stage 1 hypertension (mild) (140-159/90-99 mmHg)' },
        { value: 5, label: 'Stage 2 hypertension (moderate) (160-170/100-109 mmHg)' },
        { value: 6, label: 'Stage 3 hypertension (severe) (> 180/110 mmHg)' },
        { value: 7, label: 'Isolated systolic hypertension (> 140/ < 90 mmHg)' },
    ];


    thalachOptions = [
        { value: 3, label: 'normal (60 - 100 beats per minute)' }, { value: 6, label: 'Increased (Tachycardia) (> 100 beats per minute) ' }, { value: 7, label: 'Decreased (Bradycardia) (< 60 beats per minute)' },
    ];

    api = {
        predict: '/predict',
        openAiPredict: '/open-ai-predict',
        predictionResult: '/prediction-result'
    }
}