import { Component, EventEmitter, inject, Output } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClientService } from '../../../core/services/client-service';
import { Client } from '../../../types/client';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInput } from '../../../shared/text-input/text-input';

@Component({
  selector: 'app-register-client',
  imports: [ReactiveFormsModule, TextInput],
  templateUrl: './register-client.html',
  styleUrl: './register-client.css',
})
export class RegisterClient {
  private fb = inject(FormBuilder);
  private clientService = inject(ClientService);
  private router = inject(Router);
  @Output() cancelRegister = new EventEmitter<boolean>();

  protected clientForm: FormGroup;
  protected validationErrors: string[] = [];

  constructor() {
    this.clientForm = this.fb.group({
      name: ['', Validators.required],
      cpf: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telephone: [''],
      code: [''],
      BirthDate: [''],
      passNumber: [''],
    });
  }

  cancel() {
    this.cancelRegister.emit();
  }

  registerClient() {
    if (this.clientForm.valid) {
      const client: Client = {
        id: '', // será gerado no backend
        name: this.clientForm.value.name,
        email: this.clientForm.value.email,
        telephone: this.clientForm.value.telephone || '',
        cpf: this.clientForm.value.cpf,
        code: this.clientForm.value.cpf || '',
        BirthDate: this.clientForm.value.BirthDate || '',
        createdAt: '', // será gerado no backend
        passNumber: this.clientForm.value.passNumber || '',
        token: '',
        clientFiles: [],
        destinies: [],
      };

      this.clientService.registerClient(client).subscribe({
        next: () => {
          this.cancel();
        },
        error: (err) => {
          this.validationErrors = err;
        },
      });
    }
  }
}
