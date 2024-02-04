import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AiPredictionButtonComponent } from './ai-prediction-button.component';

describe('AiPredictionButtonComponent', () => {
  let component: AiPredictionButtonComponent;
  let fixture: ComponentFixture<AiPredictionButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AiPredictionButtonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AiPredictionButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
