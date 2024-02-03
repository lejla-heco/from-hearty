import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchivesButtonComponent } from './archives-button.component';

describe('ArchivesButtonComponent', () => {
  let component: ArchivesButtonComponent;
  let fixture: ComponentFixture<ArchivesButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ArchivesButtonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ArchivesButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
