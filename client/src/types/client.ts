import { Destiny } from './destiny';
import { Document } from './document';

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
  documents: Document[];
  destinies: Destiny[];
};
