import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAchievementModalComponent } from './create-achievement-modal.component';

describe('CreateAchievementModalComponent', () => {
  let component: CreateAchievementModalComponent;
  let fixture: ComponentFixture<CreateAchievementModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateAchievementModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateAchievementModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
