import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client } from '../../../types/client';
import { FileUpload } from '../../../shared/file-upload/file-upload';
import { ClientService } from '../../../core/services/client-service';
import { ClientFiles } from '../../../types/clientFiles';
import { DeleteFileDto } from '../../../dtos/deleteFileDto';
import { DeleteButton } from '../../../shared/delete-button/delete-button';
import { subscribeOn } from 'rxjs';

@Component({
  selector: 'app-client-documents',
  imports: [FileUpload, DeleteButton],
  templateUrl: './client-documents.html',
  styleUrl: './client-documents.css',
})
export class ClientDocuments implements OnInit {
  private route = inject(ActivatedRoute);
  protected clientService = inject(ClientService);
  protected client = signal<Client | undefined>(undefined);
  protected showForm = false;
  protected clientFiles = signal<ClientFiles[]>([]);
  protected loading = signal(false);
  private clientId: string | null | undefined = '';

  ngOnInit(): void {
    this.clientId = this.route.parent?.snapshot.paramMap.get('id');
    if (this.clientId) {
      this.clientService.getClientDocuments(this.clientId).subscribe({
        next: (documents) => this.clientFiles.set(documents),
      });
    }
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }

  addFile() {
    this.showForm = false;
  }

  onUploadFile(file: File) {
    this.loading.set(true);
    if (this.clientId) {
      this.clientService.uploadFile(file, this.clientId).subscribe({
        next: (file) => {
          this.clientService.editMode.set(false);
          this.loading.set(false);
          this.clientFiles.update((documents) => [...documents, file]);
          this.toggleForm();
        },
        error: (error) => {
          console.log('Error uploading file: ', error);
          this.loading.set(false);
        },
      });
    }
  }

  deleteFile(fileId: number) {
    if (this.clientId != null) {
      const dto: DeleteFileDto = {
        Id: fileId,
        clientId: this.clientId,
      };
      this.clientService.deleteFile(dto).subscribe({
        next: () => {
          this.clientFiles.update((files) =>
            files.filter((x) => x.id !== fileId)
          );
        },
      });
    }
  }

  downloadFile(fileId: number) {
    this.clientService.downloadDocuments(fileId).subscribe({
      next: (response) => {
        const blob = new Blob([response.body!], {
          type: response.body?.type || 'application/octet-stream',
        });
        const url = window.URL.createObjectURL(blob);
        window.open(url, '_blank');
      },
      error: (err) => console.error('Erro ao visualizar arquivo', err),
    });
  }
}
