import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredictionTabComponent } from './prediction-tab.component';

describe('PredictionTabComponent', () => {
  let component: PredictionTabComponent;
  let fixture: ComponentFixture<PredictionTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PredictionTabComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PredictionTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
