import { Component, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  Router,
  RouterOutlet,
} from '@angular/router';
import { Client } from '../../../types/client';
import { filter } from 'rxjs';

@Component({
  selector: 'app-client-documents',
  imports: [RouterOutlet],
  templateUrl: './client-documents.html',
  styleUrl: './client-documents.css',
})
export class ClientDocuments implements OnInit {
  private route = inject(ActivatedRoute);
  protected client = signal<Client | undefined>(undefined);

  ngOnInit(): void {
    this.route.parent?.data.subscribe({
      next: (data) => {
        this.client.set(data['client']);
      },
    });
  }

  @ViewChild('carousel', { static: false }) carousel!: ElementRef;

  scrollLeft() {
    this.carousel.nativeElement.scrollBy({ left: -320, behavior: 'smooth' });
  }

  scrollRight() {
    this.carousel.nativeElement.scrollBy({ left: 320, behavior: 'smooth' });
  }
}
