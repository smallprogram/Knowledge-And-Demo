import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DyncmiaFormMainComponent } from './dyncmia-form-main.component';

describe('DyncmiaFormMainComponent', () => {
  let component: DyncmiaFormMainComponent;
  let fixture: ComponentFixture<DyncmiaFormMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DyncmiaFormMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DyncmiaFormMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
