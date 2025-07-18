import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { ClientList } from '../features/clients/client-list/client-list';
import { ClientDetailed } from '../features/clients/client-detailed/client-detailed';
import { Lists } from '../features/lists/lists';
import { authGuard } from '../core/guards/auth-guard';
import { ClientDocuments } from '../features/clients/client-documents/client-documents';
import { ClientProfile } from '../features/clients/client-profile/client-profile';
import { clientResolver } from '../features/clients/client-resolver-resolver';
import { ClientDestinies } from '../features/clients/client-destinies/client-destinies';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'clients', component: ClientList },
      {
        path: 'clients/:id',
        resolve: { client: clientResolver },
        runGuardsAndResolvers: 'always',
        component: ClientDetailed,
        children: [
          { path: '', redirectTo: 'Profile', pathMatch: 'full' },
          { path: 'profile', component: ClientProfile, title: 'Perfil' },
          {
            path: 'documents',
            component: ClientDocuments,
            title: 'Documentos',
          },
          { path: 'destinies', component: ClientDestinies, title: 'Destinos' },
        ],
      },
      { path: 'lists', component: Lists },
    ],
  },
  { path: '**', component: Home },
];
