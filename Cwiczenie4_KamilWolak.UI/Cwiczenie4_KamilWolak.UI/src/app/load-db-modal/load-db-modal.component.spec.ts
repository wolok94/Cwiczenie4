import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadDbModalComponent } from './load-db-modal.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

describe('LoadDbModalComponent', () => {
  let component: LoadDbModalComponent;
  let fixture: ComponentFixture<LoadDbModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadDbModalComponent],
      providers: [provideHttpClient(), provideHttpClientTesting()]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadDbModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
