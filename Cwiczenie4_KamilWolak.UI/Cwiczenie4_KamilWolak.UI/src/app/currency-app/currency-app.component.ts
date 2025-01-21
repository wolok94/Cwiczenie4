import { CurrencyTableComponent } from "../currency-table/currency-table.component";
import { FormsModule, NgModel } from '@angular/forms';
import { Currency } from '../models/currency-model';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { CommonModule } from '@angular/common';
import { CurrencyService } from '../services/currency.service';
import { CurrencyName } from '../models/currencyName-model';
import { Color, NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { ChartModel } from '../models/chart-model';
import { Component } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from "@angular/router";
import { LoadingService } from "../services/loading.service";
import { Pagination } from "../models/pagination-model";

@Component({
  standalone: true,
  selector: 'app-currency-app',
  imports: [CurrencyTableComponent, FormsModule, CommonModule, NgxChartsModule, RouterModule],
  templateUrl: './currency-app.component.html',
  styleUrl: './currency-app.component.css',
  
})
export class CurrencyAppComponent {

  view: [number, number] = [window.innerWidth * 0.7 , 400]; 

  colorScheme: Color = {
    name: 'custom',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  showLegend: boolean = true;

  showLabels: boolean = true;

  animations: boolean = true;


  constructor(private currencyService : CurrencyService, private router : Router, private route: ActivatedRoute) {
    
  }

  currencies: Currency[] = [];
  currenciesNames: CurrencyName[] = [];
  chartModel: ChartModel[] = [];
  pagination!: Pagination<Currency>;

  currencyFilterParams: CurrencyFilterParams = {
    startDate: new Date().toISOString().split('T')[0],
    endDate: new Date().toISOString().split('T')[0],
    pageNumber: 1,
    pageSize: 10,
    searchPhrase: ''
  }

  ngOnInit(): void {
    this.loadCurrencies();

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.loadCurrencies();
      }
    });

  }

  loadCurrenciesTable() {
    this.currencyService.getCurrenciesByDate(this.currencyFilterParams).subscribe(res => {
      let currencies = JSON.parse(JSON.stringify(res));
      this.currencies = currencies.items;
      this.pagination = currencies;
      console.log(this.pagination);
      console.log(this.currencies);
      this.chartModel = [
        {
          name: this.currencies[0].currency, 
          series: this.currencies.map((currency: Currency) => ({
            name: new Date(currency.effectiveDate).toLocaleDateString('pl-PL'),
            value: currency.mid 
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

  loadDatabase() {
    this.router.navigate(['/loadDatabase'])
  }

  changePage(page: number) {
    if (page >= 1 && page <= this.pagination.totalPages) {
      this.currencyFilterParams.pageNumber = page;
      this.loadCurrenciesTable();
    }
  }

  getPagesArray(totalPages: number): number[] {
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  changePageSize() {
    this.currencyFilterParams.pageNumber = 1;
    this.loadCurrenciesTable();
  }
}
