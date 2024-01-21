import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class AiPredictionService {
    cpOptions = [
        { value: 1, label: 'typical angina' }, { value: 2, label: 'atypical angina' }, { value: 3, label: 'non-anginal pain' }, { value: 4, label: 'asymptomatic' },
    ];

    restEcgOptions = [
        { value: 0, label: 'normal' },
        { value: 1, label: 'having ST-T wave abnormality' },
        { value: 2, label: 'showing probable or definite left ventricular hypertrophy by Estes\' criteria' },
    ];

    thalachOptions = [
        { value: 3, label: 'normal' }, { value: 6, label: 'fixed defect' }, { value: 7, label: 'reversible defect' },
    ];

    api = {
        predict: '/predict',
    }
}