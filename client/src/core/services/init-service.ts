import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { Observable, of } from 'rxjs';
import { ClientService } from './client-service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private accountService = inject(AccountService);
  private clientService = inject(ClientService);

  init() {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      this.accountService.currentUser.set(user);
    }

    const clientString = localStorage.getItem('client');
    if (clientString) {
      const client = JSON.parse(clientString);
      this.clientService.currentClient.set(client);
    }

    return of(null);
  }
}
