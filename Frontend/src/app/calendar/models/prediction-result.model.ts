import { PredictionRequest } from "../../home-doctor/ai-prediction/models/prediction-request.model";

export class PredictionResult {
    id!: string;
    cp!: number;
    trestBps!: number;
    chol!: number;
    fbs!: number;
    restEcg!: number;
    thalach!: number;
    exang!: number;
    oldPeak!: number;
    slope!: number;
    ca!: number;
    thal!: number;
    label!: number;
    created!: Date;
    percentage!: number;
    houseDoctorId?: string;
    patientId!: string;

    constructor(request: PredictionRequest){
        this.cp = request.cp;
        this.trestBps = request.trestBps;
        this.chol = request.chol;
        this.fbs = request.fbs;
        this.restEcg = request.restEcg;
        this.thalach = request.thalach;
        this.exang = request.exang;
        this.oldPeak = request.oldPeak;
        this.slope = request.slope;
        this.ca = request.ca;
        this.thal = request.thal;
    }
}