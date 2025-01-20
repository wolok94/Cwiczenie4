import { Component } from '@angular/core';
import { CurrencyService } from '../services/currency.service';
import { MessageService } from '../services/message.service';
import { FormsModule } from '@angular/forms';
declare var bootstrap: any;
@Component({
  selector: 'app-load-db-modal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './load-db-modal.component.html',
  styleUrl: './load-db-modal.component.css'
})
export class LoadDbModalComponent {

  startDate!: string;
  endDate!: string;

  ngOnInit(): void {
    const modalElement = document.getElementById('dateModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement); // Inicjalizacja modalu
      modal.show(); // Automatyczne otwarcie modalu
    }
  }

  constructor(private currencyService : CurrencyService, private messageService : MessageService) {
    
  }

  loadData() {
    this.currencyService.loadDatabase().subscribe({
      next: () => {
        this.messageService.showMessage("Pomyślnie załadowano dane.", "success");
      }, error: (err) => {
        this.messageService.showMessage("Wystąpił błąd.", "error");
      }
    })
  }

}
