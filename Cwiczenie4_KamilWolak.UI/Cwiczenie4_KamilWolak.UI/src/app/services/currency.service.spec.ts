import { TestBed } from '@angular/core/testing';
import { CurrencyService } from './currency.service';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { Currency } from '../models/currency-model';
import { CurrencyName } from '../models/currencyName-model';
import { Pagination } from '../models/pagination-model';
import { CurrencyFilterParams } from '../models/currencyFilterParams-model';
import { FetchCurrencies } from '../models/fetchCurrencies-model';

describe('CurrencyService', () => {
  let service: CurrencyService;
  let httpTestingController: HttpTestingController;
  const apiUrl = 'http://localhost:5000/currencies';  // URL backendu

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CurrencyService, provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(CurrencyService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();  // Sprawdza, czy nie ma oczekujących żądań HTTP
  });

  it('powinien utworzyć serwis', () => {
    expect(service).toBeTruthy();
  });

  it('powinien pobrać dane walut dla określonego zakresu dat', () => {
    const filterParams: CurrencyFilterParams = {
      startDate: '2024-01-01',
      endDate: '2024-01-31',
      pageNumber: 1,
      pageSize: 10,
      searchPhrase: ''
    };

    const mockResponse: Pagination<Currency> = {
      items: [
        { id: '1', currency: 'Dollar', code: 'USD', mid: 3.95, effectiveDate: '2024-01-01' },
        { id: '2', currency: 'Euro', code: 'EUR', mid: 4.50, effectiveDate: '2024-01-01' }
      ],
      totalItemsCount: 2,
      totalPages: 1,
      itemsFrom: 1,
      itemsTo: 10
    };

    service.getCurrenciesByDate(filterParams).subscribe((data) => {
      expect(data).toEqual(mockResponse);
    });

    const req = httpTestingController.expectOne(
      `${apiUrl}/${filterParams.startDate}/${filterParams.endDate}?PageNumber=${filterParams.pageNumber}&PageSize=${filterParams.pageSize}`
    );

    expect(req.request.method).toEqual('GET');
    req.flush(mockResponse);
  });

  it('powinien pobrać listę dostępnych walut', () => {
    const mockCurrencyNames: CurrencyName[] = [
      { name: 'USD' },
      { name: 'EUR' }
    ];

    service.getCurrencies().subscribe((data) => {
      expect(data).toEqual(mockCurrencyNames);
    });

    const req = httpTestingController.expectOne(apiUrl);
    expect(req.request.method).toEqual('GET');

    req.flush(mockCurrencyNames);
  });

  it('powinien wysłać żądanie do załadowania danych walut do bazy', () => {
    const fetchRequest: FetchCurrencies = {
      startDate: '2024-01-01',
      endDate: '2024-12-31'
    };

    service.loadDatabase(fetchRequest).subscribe((response) => {
      expect(response).toBeNull();  // Sprawdzenie dla zwrócenia typu void
    });

    const req = httpTestingController.expectOne(`${apiUrl}/fetch`);
    expect(req.request.method).toEqual('POST');
    expect(req.request.body).toEqual(fetchRequest);

    req.flush(null);  // Odpowiedź bez zawartości (void)
  });

  it('powinien obsłużyć błąd HTTP podczas pobierania kursów walut', () => {
    const filterParams: CurrencyFilterParams = {
      startDate: '2024-01-01',
      endDate: '2024-01-31',
      pageNumber: 1,
      pageSize: 10,
      searchPhrase: ''
    };

    service.getCurrenciesByDate(filterParams).subscribe({
      next: () => fail('Oczekiwano błędu, ale zwrócono odpowiedź'),
      error: (error) => {
        expect(error.status).toBe(404);
      }
    });

    const req = httpTestingController.expectOne(
      `${apiUrl}/${filterParams.startDate}/${filterParams.endDate}?PageNumber=${filterParams.pageNumber}&PageSize=${filterParams.pageSize}`
    );

    req.flush('Nie znaleziono', { status: 404, statusText: 'Not Found' });
  });

});
