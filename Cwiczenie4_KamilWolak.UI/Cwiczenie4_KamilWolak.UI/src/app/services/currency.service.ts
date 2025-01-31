import { HttpClient, HttpParams } from '@angular/common/http';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { Observable } from 'rxjs';
import { Currency } from '../models/currency-model';
import { CurrencyName } from '../models/currencyName-model';
import { environment } from '../../environments/environment.development';
import { Injectable } from '@angular/core';
import { FetchCurrencies } from '../models/fetchCurrencies-model';
import { Pagination } from '../models/pagination-model';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  private apiUrl : string = environment.apiUrl + "/currencies"

  constructor(private httpClient: HttpClient) { }
  
  getCurrenciesByDate(currencyFilterParams: CurrencyFilterParams): Observable<Pagination<Currency>> {
    let params = new HttpParams()
    .set('PageNumber', currencyFilterParams.pageNumber.toString())
    .set('PageSize', currencyFilterParams.pageSize.toString());

  if (currencyFilterParams.searchPhrase) {
    params = params.set('SearchPhrase', currencyFilterParams.searchPhrase);
  }
    return this.httpClient.get<Pagination<Currency>>(this.apiUrl + `/${currencyFilterParams.startDate}/${currencyFilterParams.endDate}`, {params})
  }

  getCurrencies(): Observable<CurrencyName[]>{
    return this.httpClient.get<CurrencyName[]>(this.apiUrl);
  }

  loadDatabase(fetchCurrencies : FetchCurrencies): Observable<void>{
    return this.httpClient.post<void>(this.apiUrl + "/fetch", fetchCurrencies);
  }
}
