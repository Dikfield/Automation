import { Component, input } from '@angular/core';
import { Client } from '../../../types/client';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-client-card',
  imports: [RouterLink],
  templateUrl: './client-card.html',
  styleUrl: './client-card.css',
})
export class ClientCard {
  client = input.required<Client>();
}
