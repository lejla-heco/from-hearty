import { Component } from '@angular/core';
import { SharedModule } from '../../../../.shared/shared.module';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { Patient } from '../../../models/patient.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ai-prediction-button',
  templateUrl: './ai-prediction-button.component.html',
  styleUrl: './ai-prediction-button.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class AiPredictionButtonComponent implements ICellRendererAngularComp {
  public patient!: Patient;

  constructor(private router: Router) {
  }

  agInit(params: ICellRendererParams): void {
    this.patient = params.data;
  }

  refresh(params: ICellRendererParams) {
    this.patient = params.data;
    return true;
  }

  aiPrediction(): void {
    this.router.navigate(['/ai-prediction'], { queryParams: { patientId: this.patient.id } });
  }

}
