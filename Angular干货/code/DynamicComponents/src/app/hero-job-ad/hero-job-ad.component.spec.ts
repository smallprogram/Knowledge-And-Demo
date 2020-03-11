import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroJobAdComponent } from './hero-job-ad.component';

describe('HeroJobAdComponent', () => {
  let component: HeroJobAdComponent;
  let fixture: ComponentFixture<HeroJobAdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroJobAdComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroJobAdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
