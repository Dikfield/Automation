import { Client } from './../../types/client';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { AccountService } from './account-service';
import { ClientFiles } from '../../types/clientFiles';
import { DeleteFileDto } from '../../dtos/deleteFileDto';
import { CpfDto } from '../../dtos/cpfDto';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  private baseUrl = environment.apiUrl;
  currentClient = signal<Client | null>(null);
  editMode = signal(false);

  getCLients() {
    return this.http.get<Client[]>(this.baseUrl + 'clients');
  }

  getClient(id: string) {
    return this.http.get<Client>(this.baseUrl + 'clients/' + id);
  }

  getClientByCpf(cpf: CpfDto) {
    return this.http
      .post<Client>(this.baseUrl + 'clients/loginclient/', cpf)
      .pipe(
        tap((client) => {
          if (client) {
            localStorage.setItem('client', JSON.stringify(client));
            this.currentClient.set(client as Client);
            this.accountService.logout();
          }
        })
      );
  }

  logout() {
    localStorage.removeItem('client');
    this.currentClient.set(null);
  }
  registerClient(client: Client) {
    return this.http.post<Client>(this.baseUrl + 'clients/', client);
  }

  getClientDocuments(id: string) {
    return this.http.get<ClientFiles[]>(
      this.baseUrl + 'Clients/' + id + '/documents'
    );
  }

  downloadDocuments(id: number) {
    return this.http.get(this.baseUrl + 'clients/download/' + id, {
      responseType: 'blob',
      observe: 'response',
    });
  }

  uploadFile(file: File, clientId: string) {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('clientId', clientId);
    return this.http.post<ClientFiles>(
      this.baseUrl + 'clients/uploadFile',
      formData
    );
  }

  deleteFile(deleteFileDto: DeleteFileDto) {
    return this.http.request('DELETE', this.baseUrl + 'clients/deletefile', {
      body: deleteFileDto,
    });
  }
}
