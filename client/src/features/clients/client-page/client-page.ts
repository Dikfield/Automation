import { Component, inject } from '@angular/core';
import { ClientService } from '../../../core/services/client-service';
import { ClientDocuments } from "../client-documents/client-documents";

@Component({
  selector: 'app-client-page',
  imports: [ClientDocuments],
  templateUrl: './client-page.html',
  styleUrl: './client-page.css',
})
export class ClientPage {
  protected clientService = inject(ClientService);
}
