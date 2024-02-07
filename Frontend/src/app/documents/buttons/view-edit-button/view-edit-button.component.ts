import { Component } from '@angular/core';
import { SharedModule } from '../../../.shared/shared.module';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { Router } from '@angular/router';
import { DocumentModel } from '../../models/document.model';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-view-edit-button',
  templateUrl: './view-edit-button.component.html',
  styleUrl: './view-edit-button.component.css',
  standalone: true,
  imports: [SharedModule],
})
export class ViewEditButtonComponent implements ICellRendererAngularComp {
  document!: DocumentModel;

  constructor(private router: Router) {}

  agInit(params: ICellRendererParams): void {
    this.document = params.data;
  }

  refresh(params: ICellRendererParams) {
    this.document = params.data;
    return true;
  }

  viewDocument() {
    this.router.navigate(['/document-overview'], {
      queryParams: { documentId: this.document.id },
    });
  }
  downloadDocument() {
    
  }

  private downloadFile(data: HttpResponse<Blob>) {
    const downloadedFile = new Blob([data.body!], { type: data.body!.type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = 'Invoice.pdf';
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }
}
