import { Component, Input, OnInit } from '@angular/core';
import { CurrencyService } from '../services/currency.service';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { CommonModule, DatePipe } from '@angular/common';
import { Currency } from '../models/currency-model';

@Component({
  standalone: true,
  selector: 'app-currency-table',
  imports: [DatePipe, CommonModule],
  templateUrl: './currency-table.component.html',
  styleUrl: './currency-table.component.css'
})
export class CurrencyTableComponent{

  @Input() currencies: Currency[] = []; 

  constructor() {
    
  }

}
