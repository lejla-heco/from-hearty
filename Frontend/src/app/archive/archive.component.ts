import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../.shared/shared.module';
import { AgGridAngular } from 'ag-grid-angular';
import { PredictionResult } from '../calendar/models/prediction-result.model';
import { ColDef } from 'ag-grid-community';
import { HttpClient } from '@angular/common/http';
import { AiPredictionService } from '../home-doctor/ai-prediction/ai-prediction.service';
import { Config } from '../configuration/config';
import { PatientService } from '../patient/patient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Patient } from '../patient/models/patient.model';

@Component({
  selector: 'app-archive',
  templateUrl: './archive.component.html',
  styleUrl: './archive.component.css',
  standalone: true,
  imports: [SharedModule, AgGridAngular]
})
export class ArchiveComponent implements OnInit {
  themeClass = "ag-theme-quartz";
  predictionResults: PredictionResult[] = [];
  defaultColDef: ColDef = { filter: true, floatingFilter: true }
  colDefs: ColDef<any>[] = [
    { field: "id", headerName: 'ID', hide: true },
    { field: 'cp', headerName: 'Chest Pain Type' },
    { field: 'trestBps', headerName: 'Resting Blood Pressure' },
    { field: 'chol', headerName: 'Serum Cholesterol' },
    { field: 'fbs', headerName: 'Fasting Blood Sugar' },
    { field: 'restEcg', headerName: 'Rest ECG' },
    { field: 'thalach', headerName: 'Maximum Heart Rate Achieved' },
    { field: 'exang', headerName: 'Exercise Induced Angina' },
    { field: 'oldPeak', headerName: 'Old Peak' },
    { field: 'slope', headerName: 'Slope' },
    { field: 'ca', headerName: 'Number of Vessels Colored' },
    { field: 'thal', headerName: 'Thalassemia' },
    { field: 'label', headerName: 'Result' }
  ];
  patient?: Patient;

  constructor(private aiPredictionService: AiPredictionService, private httpClient: HttpClient, private patientService: PatientService,
    private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.handleQueryParams();
  }

  private handleQueryParams(): void {
    this.route.queryParams.subscribe(params => {
      if (params['patientId']) {
        this.getPatient(params['patientId']);
      }
    });
  }

  getPatient(id: any): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients + '/' + id).subscribe((response: any) => {
      this.patient = response;
      this.getArchives();
    });
  }

  getArchives(): void {
    this.httpClient.get(Config.serverAddress + this.aiPredictionService.api.predictionResult + '/' + this.patient!.id).subscribe((response: any) => {
      this.predictionResults = response;
    });
  }

  goBack(): void {
    this.router.navigate(['../']);
  }

}
