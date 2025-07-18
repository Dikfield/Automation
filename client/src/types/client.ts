import { Destiny } from './destiny';
import { ClientFiles } from './clientFiles';

export type Client = {
  id: string;
  name: string;
  email: string;
  telephone: string;
  cpf: string;
  code: string;
  BirthDate: string;
  createdAt: string;
  passNumber: string;
  token: string;
  clientFiles: ClientFiles[];
  destinies: Destiny[];
};
