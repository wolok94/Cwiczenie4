import { Component, NgModule } from '@angular/core';
import { CurrencyTableComponent } from "../currency-table/currency-table.component";
import { FormsModule, NgModel } from '@angular/forms';
import { Currency } from '../models/currency-model';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { CommonModule } from '@angular/common';
import { CurrencyService } from '../services/currency.service';
import { CurrencyName } from '../models/currencyName-model';
import { Color, NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { ChartModel } from '../models/chart-model';

@Component({
  standalone: true,
  selector: 'app-currency-app',
  imports: [CurrencyTableComponent, FormsModule, CommonModule, NgxChartsModule],
  templateUrl: './currency-app.component.html',
  styleUrl: './currency-app.component.css',
  
})
export class CurrencyAppComponent {

  view: [number, number] = [700, 400]; 

  colorScheme: Color = {
    name: 'custom',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  showLegend: boolean = true;

  showLabels: boolean = true;

  animations: boolean = true;


  constructor(private currencyService : CurrencyService) {
    
  }

  currencies: Currency[] = [];
  currenciesNames: CurrencyName[] = [];
  chartModel : ChartModel[] = [];

  currencyFilterParams: CurrencyFilterParams = {
    startDate: new Date().toISOString().split('T')[0],
    endDate: new Date().toISOString().split('T')[0],
    pageNumber: 1,
    pageSize: 10,
    searchPhrase: ''
  }

  ngOnInit(): void {
    this.loadCurrencies();
    this.loadCurrenciesTable();
  }

  loadCurrenciesTable() {
    this.currencyService.getCurrenciesByDate(this.currencyFilterParams).subscribe(res => {
      let currencies = JSON.parse(JSON.stringify(res));
      this.currencies = currencies.items;
      console.log(this.currencies);
      this.chartModel = [
        {
          name: this.currencies[0].currency, // Nazwa waluty
          series: this.currencies.map((currency: Currency) => ({
            name: currency.effectiveDate, // Data
            value: currency.mid // Wartość
          }))
        }];
        console.log(this.chartModel);
    })
  }

  loadCurrencies(){
    this.currencyService.getCurrencies().subscribe(res => {
      let currencies = JSON.parse(JSON.stringify(res));
      this.currenciesNames = currencies;
    })
  }
}
