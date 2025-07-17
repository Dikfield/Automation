import { ActivatedRoute } from '@angular/router';
import { ClientService } from './../../../core/services/client-service';
import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-clients-files',
  imports: [],
  templateUrl: './clients-files.html',
  styleUrl: './clients-files.css',
})
export class ClientsFiles {
  protected clientService = inject(ClientService);
  private route = inject(ActivatedRoute);
  protected document$?: Observable<Document[]>;

  constructor() {
    const clientId = this.route.parent?.snapshot.paramMap.get('clientId');
    if (clientId) {
      this.document$ = this.clientService.getClientDocuments(clientId);
    }
  }
}
