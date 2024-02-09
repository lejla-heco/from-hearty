import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Config } from '../configuration/config';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  rootUrl: string = Config.serverAddress;

  constructor(private http: HttpClient) { }

  downloadDocument(id: string) {
    return this.http.get(this.rootUrl + '/download-document/' + id, {
      reportProgress: true,
      responseType: 'blob',
      observe: 'events'
    });
  }

  downloadUserManualDocument() {
    const filePath = 'assets/user-manual/User Manual.pdf';
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = "User Manual.pdf";
    a.href = filePath;
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }

  getUserDocuments(id: string) {
    return this.http.get(this.rootUrl + '/user-documents/' + id);
  }
}
