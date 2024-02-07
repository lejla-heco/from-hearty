import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HouseDoctorsComponent } from './house-doctors.component';

describe('HouseDoctorsComponent', () => {
  let component: HouseDoctorsComponent;
  let fixture: ComponentFixture<HouseDoctorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HouseDoctorsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HouseDoctorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
