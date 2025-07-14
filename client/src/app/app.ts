import { AccountService } from './../core/services/account-service';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from '../layout/nav/nav';
import { Home } from '../features/home/home';
import { Client } from '../types/client';

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  private http = inject(HttpClient);
  protected title = 'Viva Tubismo';
  protected clients = signal<Client[]>([]);

  async ngOnInit() {
    this.setCurrentUser();
    this.clients.set(await this.getClients());
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getClients() {
    try {
      return lastValueFrom(this.http.get<Client[]>('https://localhost:5001/api/clients'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
