import { Component } from '@angular/core';
import { SharedModule } from '../../../../.shared/shared.module';
import { Patient } from '../../../models/patient.model';
import { Router } from '@angular/router';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';

@Component({
  selector: 'app-archives-button',
  templateUrl: './archives-button.component.html',
  styleUrl: './archives-button.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class ArchivesButtonComponent implements ICellRendererAngularComp {
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

  archives(): void {
    this.router.navigate(['/archives'], { queryParams: { patientId: this.patient.id } });
  }
}
