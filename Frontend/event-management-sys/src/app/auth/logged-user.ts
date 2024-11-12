export interface LoggedUser {
  ID: number;
  Ime: string;
  Prezime: string;
  Email: string;
  Tip: TipKorisnika;
  Token: string;
}

export enum TipKorisnika {
  Admin,
  Korisnik,
}
