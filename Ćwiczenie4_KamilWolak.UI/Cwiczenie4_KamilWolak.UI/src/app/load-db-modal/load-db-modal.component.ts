import { Component } from '@angular/core';
import { CurrencyService } from '../services/currency.service';
import { MessageService } from '../services/message.service';
import { FormsModule } from '@angular/forms';
import { FetchCurrencies } from '../models/fetchCurrencies-model';
import { LoadingModalComponent } from "../loading-modal/loading-modal.component";
import { CommonModule } from '@angular/common';
import { LoadingService } from '../services/loading.service';
import { Router } from '@angular/router';
declare var bootstrap: any;
@Component({
  selector: 'app-load-db-modal',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './load-db-modal.component.html',
  styleUrl: './load-db-modal.component.css'
})
export class LoadDbModalComponent {

  fetchCurrencies: FetchCurrencies = {
    startDate : new Date().toISOString().split('T')[0],
    endDate :new Date().toISOString().split('T')[0]
  };

   modal: any;



  ngOnInit(): void {

    const modalElement = document.getElementById('dateModal');
    if (modalElement) {
      this.modal = new bootstrap.Modal(modalElement);
      this.modal.show(); 
    }
  }

  constructor(private currencyService : CurrencyService, private messageService : MessageService, private loadingService : LoadingService, private router: Router) {
    
  }

  loadData() {
    this.loadingService.isLoading.next(true);
    this.currencyService.loadDatabase(this.fetchCurrencies).subscribe({
      next: () => {
        this.messageService.showMessage("Pomyślnie załadowano dane.", "success");
        this.loadingService.isLoading.next(false);
        this.modal.hide();
        this.router.navigate(['']);
      }, error: (err) => {
        this.messageService.showMessage("Wystąpił błąd.", "error");
        this.loadingService.isLoading.next(false);
      }
    })

    
  }

}
