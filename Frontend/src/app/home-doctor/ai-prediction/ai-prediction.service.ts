import { Injectable } from "@angular/core";
import { PredictionRequest } from "./models/prediction-request.model";
import { ValueFormatterParams } from 'ag-grid-community';


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

    fbs = [
        { value: 0, label: 'No' },
        { value: 1, label: 'Yes' },
    ];


    thalachOptions = [
        { value: 3, label: 'normal (60 - 100 beats per minute)' }, { value: 6, label: 'Increased (Tachycardia) (> 100 beats per minute) ' }, { value: 7, label: 'Decreased (Bradycardia) (< 60 beats per minute)' },
    ];

    api = {
        predict: '/predict',
        openAiPredict: '/open-ai-predict',
        predictionResult: '/prediction-result',
        predictionsPerMonth: '/predictions/house-doctor',
        predictionResultPercentages: '/prediction-result/percentage',
        urgentPatients: '/prediction-result/patients'
    }
}