import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../.shared/shared.module';
import { AgGridAngular } from 'ag-grid-angular';
import { PredictionResult } from '../calendar/models/prediction-result.model';
import { ColDef, ValueFormatterParams } from 'ag-grid-community';
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
    { field: 'percentage', headerName: 'Probability Percentage', valueFormatter: this.percentageFormatter, },
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
    { field: 'created', headerName: 'Created At', valueFormatter: this.dateFormatter },
    { field: 'label', headerName: 'Result' }
  ];
  patient?: Patient;
  gridOptions: any;

  constructor(private aiPredictionService: AiPredictionService, private httpClient: HttpClient, private patientService: PatientService,
    private route: ActivatedRoute, private router: Router) {
    this.gridOptions = {
      getRowStyle: (params: any) => {
        const percentage = params.data.percentage;
        if (percentage >= 0 && percentage <= 15) {
          return { background: 'rgba(204, 255, 204, 0.5)' };
        } else if (percentage >= 16 && percentage <= 55) {
          return { background: 'rgba(255, 230, 204, 0.5)' };
        } else {
          return { background: 'rgba(255, 204, 204, 0.5)' };
        }
      }
    };
  }

  ngOnInit(): void {
    this.handleQueryParams();
  }

  dateFormatter(params: ValueFormatterParams) {
    return new Date(params.value).toLocaleDateString('en-us', {
      weekday: 'long',
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  percentageFormatter(params: ValueFormatterParams) {
    return `${params.value} %`
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
      console.log(this.predictionResults);
    });
  }

  goBack(): void {
    this.router.navigate(['../']);
  }

}
