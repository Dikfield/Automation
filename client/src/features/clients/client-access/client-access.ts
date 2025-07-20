import { ClientService } from './../../../core/services/client-service';
import { Component, inject } from '@angular/core';
import { TextInput } from '../../../shared/text-input/text-input';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CpfDto } from '../../../dtos/cpfDto';
import { Router } from '@angular/router';
import { ToastService } from '../../../core/services/toast-service';

@Component({
  selector: 'app-client-access',
  imports: [TextInput, ReactiveFormsModule],
  templateUrl: './client-access.html',
  styleUrl: './client-access.css',
})
export class ClientAccess {
  protected clientService = inject(ClientService);
  protected cpfForm: FormGroup;
  private fb = inject(FormBuilder);
  protected validationErrors: string[] = [];
  private router = inject(Router);
  private toast = inject(ToastService);

  constructor() {
    this.cpfForm = this.fb.group({
      cpf: ['', Validators.required],
    });
  }

  checkCpf() {
    if (this.cpfForm.valid) {
      const clientCpf: CpfDto = {
        cpf: this.cpfForm.value.cpf,
      };

      this.clientService.getClientByCpf(clientCpf).subscribe({
        next: () => {
          var clientId = this.clientService.currentClient()?.id;
          this.router.navigateByUrl('/clientdocument');
          this.toast.success('Login successfully');
        },
        error: (err) => {
          this.toast.error(err.error);
        },
      });
    }
  }
}
