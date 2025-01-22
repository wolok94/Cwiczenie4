import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CurrencyAppComponent } from './currency-app.component';
import { CurrencyService } from '../services/currency.service';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CurrencyTableComponent } from '../currency-table/currency-table.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { of } from 'rxjs';

class MockCurrencyService {
  getCurrencies = jasmine.createSpy('getCurrencies').and.returnValue(of([
    { name: 'USD' },
    { name: 'EUR' }
  ]));

  getCurrenciesByDate = jasmine.createSpy('getCurrenciesByDate').and.returnValue(of({
    items: [
      { currency: 'USD', effectiveDate: '2024-01-01', mid: 3.95 },
      { currency: 'USD', effectiveDate: '2024-01-02', mid: 3.97 }
    ],
    totalPages: 2
  }));
}

describe('CurrencyAppComponent', () => {
  let component: CurrencyAppComponent;
  let fixture: ComponentFixture<CurrencyAppComponent>;
  let currencyService: MockCurrencyService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FormsModule,
        CommonModule,
        NgxChartsModule,
        CurrencyAppComponent,  
        CurrencyTableComponent
      ],
      providers: [
        { provide: CurrencyService, useClass: MockCurrencyService }
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CurrencyAppComponent);
    component = fixture.componentInstance;
    currencyService = TestBed.inject(CurrencyService) as unknown as MockCurrencyService;

    fixture.detectChanges();
  });

  it('powinien utworzyć komponent', () => {
    expect(component).toBeTruthy();
  });

  it('powinien wyświetlić poprawny tytuł', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Kursy walut');
  });

  it('powinien wywołać loadCurrenciesTable po kliknięciu przycisku', () => {
    spyOn(component, 'loadCurrenciesTable');

    const button = fixture.nativeElement.querySelector('button.btn-primary');
    expect(button).toBeTruthy();

    button.click();
    fixture.detectChanges();

    expect(component.loadCurrenciesTable).toHaveBeenCalled();
  });

  it('powinien wywołać metodę getCurrenciesByDate w CurrencyService', () => {
    component.loadCurrenciesTable();
    expect(currencyService.getCurrenciesByDate).toHaveBeenCalled();
  });

  it('powinien załadować listę walut przy inicjalizacji', () => {
    expect(currencyService.getCurrencies).toHaveBeenCalled();
    expect(component.currenciesNames.length).toBeGreaterThan(0);
  });
});
