import {
  Component,
  ElementRef,
  EventEmitter,
  input,
  output,
  Output,
  signal,
  ViewChild,
} from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  imports: [FormsModule],
  templateUrl: './file-upload.html',
  styleUrl: './file-upload.css',
})
export class FileUpload {
  protected fileScr = signal<string | ArrayBuffer | null | undefined>(null);
  protected isDragging = false;
  private fileToUpload: File | null = null;
  @Output() cancel = new EventEmitter<void>();
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;
  uploadFile = output<File>();
  loading = input<boolean>(false);
  protected fileName: string = '';

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;

    if (event.dataTransfer?.files.length) {
      const file = event.dataTransfer.files[0];
      this.fileToUpload = file;
      this.preview(file);
    }
  }

  onUploadFile() {
    if (this.fileToUpload) {
      const name = this.fileName?.trim() || this.fileToUpload.name;

      const renamedFile = new File([this.fileToUpload], name, {
        type: this.fileToUpload.type,
      });

      this.uploadFile.emit(renamedFile);
    }
  }

  onCancel() {
    this.cancel.emit();
    this.fileToUpload = null;
    this.fileScr.set(null);
  }

  private preview(file: File) {
    const reader = new FileReader();
    reader.onload = (e) => {
      this.fileScr.set(e.target?.result);
    };
    reader.readAsDataURL(file);
  }

  triggerFileSelect() {
    this.fileInput.nativeElement.click();
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      this.fileToUpload = file;
      this.preview(file);
    }
    input.value = '';
  }
}
