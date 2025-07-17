import {
  Component,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  Router,
  RouterLink,
  RouterLinkActive,
  RouterOutlet,
} from '@angular/router';
import { filter } from 'rxjs';
import { Client } from '../../../types/client';

@Component({
  selector: 'app-client-detailed',
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './client-detailed.html',
  styleUrl: './client-detailed.css',
})
export class ClientDetailed implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  protected client = signal<Client | undefined>(undefined);
  protected title = signal<string | undefined>('Profile');

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => this.client.set(data['client']),
    });
    this.title.set(this.route.firstChild?.snapshot?.title);

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe({
        next: () => {
          this.title.set(this.route.firstChild?.snapshot?.title);
        },
      });
  }


}
