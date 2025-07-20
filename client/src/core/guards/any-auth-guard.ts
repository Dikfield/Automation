import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ClientService } from '../services/client-service';
import { AccountService } from '../services/account-service';
import { ToastService } from '../services/toast-service';

export const anyAuthGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const clientService = inject(ClientService);
  const toas = inject(ToastService);

  if (accountService.currentUser() || clientService.currentClient()) {
    return true;
  } else {
    toas.error('You must be logged in to access this page');
    return false;
  }
};
