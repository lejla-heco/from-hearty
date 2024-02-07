import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditVideoComponent } from './create-edit-video.component';

describe('CreateEditVideoComponent', () => {
  let component: CreateEditVideoComponent;
  let fixture: ComponentFixture<CreateEditVideoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateEditVideoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateEditVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
