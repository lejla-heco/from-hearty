import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { ContactsService } from '../contacts.service';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../configuration/config';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, SizeColumnsToContentStrategy, SizeColumnsToFitGridStrategy, SizeColumnsToFitProvidedWidthStrategy } from 'ag-grid-community';

@Component({
  selector: 'app-cardiologists',
  templateUrl: './cardiologists.component.html',
  styleUrl: './cardiologists.component.css',
  standalone: true,
  imports: [SharedModule, AgGridAngular]
})
export class CardiologistsComponent {
  themeClass = "ag-theme-quartz";
  cardiologists: any[] = [];
  defaultColDef: ColDef = { filter: true, floatingFilter: true }
  colDefs: ColDef<any>[] = [
    { field: "id", hide: true },
    { field: 'firstName' },
    { field: 'lastName' },
    { field: 'email' },
  ];
  public autoSizeStrategy:
    | SizeColumnsToFitGridStrategy
    | SizeColumnsToFitProvidedWidthStrategy
    | SizeColumnsToContentStrategy = {
      type: 'fitGridWidth',
    };

  constructor( private httpClient: HttpClient, private contactsService: ContactsService) {
    this.getCardiologists();
  }

  getCardiologists(): void {
    this.httpClient.get(Config.serverAddress + this.contactsService.api.cardiologists).subscribe((response: any) => {
      this.cardiologists = response;
    });
  }
}
