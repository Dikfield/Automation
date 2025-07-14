import { Component, Input, signal } from '@angular/core';
import { Client } from '../../types/client';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  @Input({ required: true }) clientsFromApp: Client[] = [];
}
