import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CurrencyAppComponent } from './currency-app.component';
import { CurrencyService } from '../services/currency.service';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CurrencyTableComponent } from '../currency-table/currency-table.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

describe('CurrencyAppComponent', () => {
  let component: CurrencyAppComponent;
  let fixture: ComponentFixture<CurrencyAppComponent>;
  let httpTestingController: HttpTestingController;

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
        provideHttpClientTesting(),  // Nowoczesny sposób na zapewnienie mocka dla HttpClient
        CurrencyService
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CurrencyAppComponent);
    component = fixture.componentInstance;
    httpTestingController = TestBed.inject(HttpTestingController);

    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should call loadCurrenciesTable when button is clicked', () => {
    spyOn(component, 'loadCurrenciesTable'); // Szpiegujemy metodę komponentu

    // Znalezienie przycisku na podstawie klasy CSS
    const button = fixture.nativeElement.querySelector('button.btn-primary');
    expect(button).toBeTruthy();

    // Kliknięcie przycisku
    button.click();
    fixture.detectChanges();

    // Sprawdzenie, czy metoda została wywołana
    expect(component.loadCurrenciesTable).toHaveBeenCalled();
  });

  afterEach(() => {
    httpTestingController.verify(); // Sprawdzenie, czy nie ma oczekujących żądań HTTP
  });
});
