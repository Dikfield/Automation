import { ActivatedRoute } from '@angular/router';
import { Client } from '../../../types/client';
import { Component, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';

@Component({
  selector: 'app-client-profile',
  imports: [],
  templateUrl: './client-profile.html',
  styleUrl: './client-profile.css',
})
export class ClientProfile implements OnInit {
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
