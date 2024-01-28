import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardiologistCalendarComponent } from './cardiologist-calendar.component';

describe('CardiologistCalendarComponent', () => {
  let component: CardiologistCalendarComponent;
  let fixture: ComponentFixture<CardiologistCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CardiologistCalendarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CardiologistCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
