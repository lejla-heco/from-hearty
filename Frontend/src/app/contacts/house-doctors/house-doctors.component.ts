import { Component } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, SizeColumnsToContentStrategy, SizeColumnsToFitGridStrategy, SizeColumnsToFitProvidedWidthStrategy } from 'ag-grid-community';
import { HttpClient } from '@angular/common/http';
import { ContactsService } from '../contacts.service';
import { Config } from '../../configuration/config';

@Component({
  selector: 'app-house-doctors',
  templateUrl: './house-doctors.component.html',
  styleUrl: './house-doctors.component.css',
  standalone: true,
  imports: [SharedModule, AgGridAngular]
})
export class HouseDoctorsComponent {
  themeClass = "ag-theme-quartz";
  houseDoctors: any[] = [];
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
    this.getHouseDoctors();
  }

  getHouseDoctors(): void {
    this.httpClient.get(Config.serverAddress + this.contactsService.api.houseDoctors).subscribe((response: any) => {
      this.houseDoctors = response;
    });
  }
}
