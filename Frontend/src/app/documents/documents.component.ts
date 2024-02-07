import { Component, OnInit } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { SharedModule } from '../.shared/shared.module';
import { AiPredictionButtonComponent } from '../patient/patient-overview/buttons/ai-prediction-button/ai-prediction-button.component';
import { ArchivesButtonComponent } from '../patient/patient-overview/buttons/archives-button/archives-button.component';
import {
  ColDef,
  ValueFormatterParams,
  SizeColumnsToContentStrategy,
  SizeColumnsToFitGridStrategy,
  SizeColumnsToFitProvidedWidthStrategy,
} from 'ag-grid-community';
import { ViewEditButtonComponent } from './buttons/view-edit-button/view-edit-button.component';
import { DocumentModel } from './models/document.model';
import { DocumentService } from './document.service';
import { AuthentificationHelper } from '../authentification/authentification-helper';
import { Config } from '../configuration/config';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrl: './documents.component.css',
  standalone: true,
  imports: [SharedModule, AgGridAngular],
})
export class DocumentsComponent implements OnInit {
  userId: string = AuthentificationHelper.getLoginToken().userId;

  themeClass = 'ag-theme-quartz';
  documents: DocumentModel[] = [];
  defaultColDef: ColDef = { filter: true, floatingFilter: true };
  colDefs: ColDef<any>[] = [
    { field: 'id', hide: true },
    { field: 'name' },
    { field: 'createdDate', valueFormatter: this.dateFormatter },
    {
      headerName: 'Actions',
      floatingFilter: false,
      filter: false,
      minWidth: 250,
      cellRenderer: ViewEditButtonComponent,
    },
  ];

  constructor(
    private _documentService: DocumentService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.getDocuments();
  }

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

  getDocuments() {
    this._documentService
      .getUserDocuments(this.userId)
      .subscribe((response: any) => {
        this.documents = response.data;
      });
  }

  fileChangeEvent(files: FileList | null) {
    let file = files![0];
    const formData = new FormData();
    formData.append('file', file);

    let actionPath = `${Config.serverAddress}/upload-document/${this.userId}`;
    const req = new HttpRequest('POST', `${actionPath}`, formData, {
      reportProgress: true,
    });
    this.http
      .request<any>(req)
      .pipe(
        finalize(() => {
          this.getDocuments();
        })
      )
      .subscribe();
  }
}
