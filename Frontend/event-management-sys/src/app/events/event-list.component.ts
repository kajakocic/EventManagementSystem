import { Component } from '@angular/core';

@Component({
  selector: 'pm-events',
  templateUrl: './event-list.component.html',
})
export class EventListComponent {
  naslov: string = 'Aktuelna dešavanja';

  events: any[] = [
    {
      naziv: 'Koncert Gradske Muzike',
      datum: '2024-11-10',
      vreme: '19:00',
      cenaUlaznice: '1000 RSD',
      kategorija: 'Muzika',
      lokacija: 'Trg Republike, Beograd',
      detalji: 'Pogledaj detalje',
      komentari: 'Dodaj komentar',
    },
    {
      naziv: 'Festival hrane i vina',
      datum: '2024-11-15',
      vreme: '12:00',
      cenaUlaznice: '500 RSD',
      kategorija: 'Gastronomija',
      lokacija: 'Kalemegdan, Beograd',
      detalji: 'Pogledaj detalje',
      komentari: 'Dodaj komentar',
    },
    {
      naziv: 'Radionica o digitalnom marketingu',
      datum: '2024-11-20',
      vreme: '10:00',
      cenaUlaznice: '3000 RSD',
      kategorija: 'Obuka',
      lokacija: 'Tehnički fakultet, Beograd',
      detalji: 'Pogledaj detalje',
      komentari: 'Dodaj komentar',
    },
  ];
}
