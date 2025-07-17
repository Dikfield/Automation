import { Client } from './../../types/client';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AccountService } from './account-service';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  private baseUrl = environment.apiUrl;

  getCLients() {
    return this.http.get<Client[]>(this.baseUrl + 'clients');
  }

  getClient(id: string) {
    return this.http.get<Client>(this.baseUrl + 'clients/' + id);
  }

  getClientDocuments(id: string) {
    return this.http.get<Document[]>(
      this.baseUrl + 'Clients/' + id + '/documents'
    );
  }
}
