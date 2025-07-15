import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { ClientList } from '../features/clients/client-list/client-list';
import { ClientDetailed } from '../features/clients/client-detailed/client-detailed';
import { Lists } from '../features/lists/lists';
import { authGuard } from '../core/guards/auth-guard';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'clients', component: ClientList },
      { path: 'clients/:id', component: ClientDetailed },
      { path: 'lists', component: Lists },
    ],
  },
  { path: '**', component: Home },
];
