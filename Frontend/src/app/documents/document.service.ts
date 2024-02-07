import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Config } from '../configuration/config';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  rootUrl: string = Config.serverAddress;

  constructor(private http: HttpClient) {}

  downloadDocument(id: string) {
    return this.http.get(this.rootUrl + '/download-document/' + id);
  }

  getUserDocuments(id: string) {
    return this.http.get(this.rootUrl + '/user-documents/' + id);
  }
}
