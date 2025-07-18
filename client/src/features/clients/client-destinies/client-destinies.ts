import {
  Component,
  ElementRef,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client } from '../../../types/client';

@Component({
  selector: 'app-client-destinies',
  imports: [],
  templateUrl: './client-destinies.html',
  styleUrl: './client-destinies.css',
})
export class ClientDestinies implements OnInit {
  private route = inject(ActivatedRoute);
  protected client = signal<Client | undefined>(undefined);
  @ViewChild('carousel', { static: false }) carousel!: ElementRef;

  ngOnInit(): void {
    this.route.parent?.data.subscribe({
      next: (data) => {
        this.client.set(data['client']);
      },
    });
  }

  scrollLeft() {
    this.carousel.nativeElement.scrollBy({ left: -320, behavior: 'smooth' });
  }

  scrollRight() {
    this.carousel.nativeElement.scrollBy({ left: 320, behavior: 'smooth' });
  }
}
