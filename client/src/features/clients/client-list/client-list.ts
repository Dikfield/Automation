import { Component, inject } from '@angular/core';
import { ClientService } from '../../../core/services/client-service';
import { Observable } from 'rxjs';
import { Client } from '../../../types/client';
import { AsyncPipe } from '@angular/common';
import { ClientCard } from '../client-card/client-card';
import { RegisterClient } from '../register-client/register-client';

@Component({
  selector: 'app-client-list',
  imports: [AsyncPipe, ClientCard, RegisterClient],
  templateUrl: './client-list.html',
  styleUrl: './client-list.css',
})
export class ClientList {
  private clientService = inject(ClientService);
  protected clients$: Observable<Client[]>;
  protected showForm = false;

  constructor() {
    this.clients$ = this.clientService.getCLients();
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }
}
