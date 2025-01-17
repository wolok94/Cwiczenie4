import { Component, NgModule } from '@angular/core';
import { CurrencyTableComponent } from "../currency-table/currency-table.component";
import { FormsModule, NgModel } from '@angular/forms';
import { Currency } from '../models/currency-model';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { CommonModule } from '@angular/common';
import { CurrencyService } from '../services/currency.service';

@Component({
  selector: 'app-currency-app',
  imports: [CurrencyTableComponent, FormsModule, CommonModule],
  templateUrl: './currency-app.component.html',
  styleUrl: './currency-app.component.css',
  
})
export class CurrencyAppComponent {


  constructor(private currencyService : CurrencyService) {
    
  }

  currencies: Currency[] = [];

  currencyFilterParams: CurrencyFilterParams = {
    startDate: "2025-01-17",
    endDate: "2025-01-17",
    pageNumber: 1,
    pageSize: 10,
  }

  ngOnInit(): void {
    this.loadCurrencies();
  }

  loadCurrencies() {
    this.currencyService.getCurrenciesByDate(this.currencyFilterParams).subscribe(res => {
      let currencies = JSON.parse(JSON.stringify(res));
      this.currencies = currencies.items;
      console.log(this.currencies);
    })
  }
}
