import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardiologistDashboardComponent } from './cardiologist-dashboard.component';

describe('CardiologistDashboardComponent', () => {
  let component: CardiologistDashboardComponent;
  let fixture: ComponentFixture<CardiologistDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CardiologistDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CardiologistDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
