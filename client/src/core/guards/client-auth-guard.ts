import { CanActivateFn } from '@angular/router';
import { ClientService } from '../services/client-service';
import { inject } from '@angular/core';
import { ToastService } from '../services/toast-service';

export const clientAuthGuard: CanActivateFn = (route, state) => {
  const clientService = inject(ClientService);
  const toas = inject(ToastService);

  if (clientService.currentClient()) return true;
  else {
    toas.error('Você precisa ser cliente para acessar a página');
    return false;
  }
};
