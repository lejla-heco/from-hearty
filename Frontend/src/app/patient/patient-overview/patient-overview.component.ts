import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, ValueFormatterParams, SizeColumnsToContentStrategy, SizeColumnsToFitGridStrategy, SizeColumnsToFitProvidedWidthStrategy } from 'ag-grid-community';
import { PatientService } from '../patient.service';
import { HttpClient } from '@angular/common/http';
import { Patient } from '../models/patient.model';
import { Config } from '../../configuration/config';
import { ToastrService } from 'ngx-toastr';
import { AiPredictionService } from '../../home-doctor/ai-prediction/ai-prediction.service';
import { PredictionRequest } from '../../home-doctor/ai-prediction/models/prediction-request.model';
import { AiPredictionButtonComponent } from './buttons/ai-prediction-button/ai-prediction-button.component';
import { ArchivesButtonComponent } from './buttons/archives-button/archives-button.component';

@Component({
  selector: 'app-patient-overview',
  templateUrl: './patient-overview.component.html',
  styleUrl: './patient-overview.component.css',
  standalone: true,
  imports: [SharedModule, AgGridAngular]
})
export class PatientOverviewComponent {
  themeClass = "ag-theme-quartz";
  patients: Patient[] = [];
  defaultColDef: ColDef = { filter: true, floatingFilter: true }
  colDefs: ColDef<any>[] = [
    { field: "id", hide: true },
    { field: 'firstName' },
    { field: 'lastName' },
    { field: 'birthDate', valueFormatter: this.dateFormatter },
    { field: 'dateRegistered', valueFormatter: this.dateFormatter },
    {
      field: 'gender',
      cellRenderer: function (params: any) {
        if (params.value)
          return '<span><i class="fas fa-female"></i></span>'
        return '<i class="fas fa-male"></i>'
      },
      cellStyle: { fontSize: '18px', textAlign: 'center' },
      floatingFilter: false,
      filter: false,
      width: 85
    },
    {
      headerName: 'Ai Prediction',
      floatingFilter: false,
      filter: false,
      minWidth: 250,
      cellRenderer: AiPredictionButtonComponent,
    },
    {
      headerName: 'Archives',
      floatingFilter: false,
      filter: false,
      minWidth: 250,
      cellRenderer: ArchivesButtonComponent
    },
  ];
  public autoSizeStrategy:
    | SizeColumnsToFitGridStrategy
    | SizeColumnsToFitProvidedWidthStrategy
    | SizeColumnsToContentStrategy = {
      type: 'fitGridWidth',
    };

  dateFormatter(params: ValueFormatterParams) {
    return new Date(params.value).toLocaleDateString('en-us', {
      weekday: 'long',
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  constructor(private patientService: PatientService, private httpClient: HttpClient, private toastr: ToastrService,
    private aiPredictionService: AiPredictionService) {
    this.aiPredictionService.predictionRequest = new PredictionRequest();
    this.getPatients();
  }

  getPatients(): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients).subscribe((response: any) => {
      this.patients = response;
    });
  }
}
