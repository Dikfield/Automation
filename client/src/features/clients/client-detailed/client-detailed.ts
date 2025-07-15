import { Component, inject, OnInit } from '@angular/core';
import { ClientService } from '../../../core/services/client-service';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { Client } from '../../../types/client';

@Component({
  selector: 'app-client-detailed',
  imports: [AsyncPipe],
  templateUrl: './client-detailed.html',
  styleUrl: './client-detailed.css',
})
export class ClientDetailed implements OnInit {
  private clientService = inject(ClientService);
  private route = inject(ActivatedRoute);
  protected client$?: Observable<Client>;

  ngOnInit(): void {
    this.client$ = this.loadClient();
  }

  loadClient() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;
    return this.clientService.getMember(id);
  }
}
