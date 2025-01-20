import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadDbModalComponent } from './load-db-modal.component';

describe('LoadDbModalComponent', () => {
  let component: LoadDbModalComponent;
  let fixture: ComponentFixture<LoadDbModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadDbModalComponent]
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
