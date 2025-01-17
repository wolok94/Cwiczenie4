import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { Observable } from 'rxjs';
import { Currency } from '../models/currency-model';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  private apiUrl : string = environment.apiUrl + "currencies"

  constructor(private httpClient: HttpClient) { }
  
  getCurrenciesByDate(currencyFilterParams: CurrencyFilterParams): Observable<Currency[]> {
    let params = new HttpParams()
    .set('PageNumber', currencyFilterParams.pageNumber.toString())
    .set('PageSize', currencyFilterParams.pageSize.toString());

  if (currencyFilterParams.searchPhrase) {
    params = params.set('SearchPhrase', currencyFilterParams.searchPhrase);
  }
    return this.httpClient.get<Currency[]>(this.apiUrl + `/${currencyFilterParams.startDate}/${currencyFilterParams.endDate}`, {params})
  }
}
