import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HouseDoctorDashboardComponent } from './house-doctor-dashboard.component';

describe('HouseDoctorDashboardComponent', () => {
  let component: HouseDoctorDashboardComponent;
  let fixture: ComponentFixture<HouseDoctorDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HouseDoctorDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HouseDoctorDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
