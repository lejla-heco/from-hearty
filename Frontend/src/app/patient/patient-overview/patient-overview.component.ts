import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, ValueFormatterParams, SelectionChangedEvent, SizeColumnsToContentStrategy, SizeColumnsToFitGridStrategy, SizeColumnsToFitProvidedWidthStrategy } from 'ag-grid-community';
import { PatientService } from '../patient.service';
import { HttpClient } from '@angular/common/http';
import { Patient } from '../models/patient.model';
import { Config } from '../../configuration/config';
import { ToastrService } from 'ngx-toastr';
import { AuthentificationHelper } from '../../authentification/authentification-helper';

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
  defaultColDef: ColDef = { filter: true }
  colDefs: ColDef<any>[] = [
    { field: "id", hide: true },
    { field: '', checkboxSelection: true, width: 50 },
    { field: 'firstName' },
    { field: 'lastName' },
    { field: 'birthDate', valueFormatter: this.dateFormatter },
    { field: 'dateRegistered', valueFormatter: this.dateFormatter },
    { field: 'gender' }
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

  constructor(private patientService: PatientService, private httpClient: HttpClient, private toastr: ToastrService) {
    this.displayRoleMessage();
    this.getPatients();
  }

  displayRoleMessage(): void {
    if (AuthentificationHelper.getLoginToken().roleType == 0) {
      this.toastr.info("To create an AI Prediction or review the Archive, you must first select a patient!");
    }
    if (AuthentificationHelper.getLoginToken().roleType == 0) {
      this.toastr.info("To review the Archive, you must first select a patient!");
    }
  }

  getPatients(): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients).subscribe((response: any) => {
      this.patients = response;
    });
  }

  onSelectionChanged = (event: SelectionChangedEvent) => {
    let selectedPatient: Patient[] = event.api.getSelectedRows();
    this.patientService.selectedPatient = selectedPatient[0];
    this.patientService.isPatientSelected = selectedPatient.length > 0;
  };
}
