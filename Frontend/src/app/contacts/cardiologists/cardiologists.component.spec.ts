import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardiologistsComponent } from './cardiologists.component';

describe('CardiologistsComponent', () => {
  let component: CardiologistsComponent;
  let fixture: ComponentFixture<CardiologistsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CardiologistsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CardiologistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
