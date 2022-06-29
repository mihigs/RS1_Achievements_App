import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignAchievementModalComponent } from './assign-achievement-modal.component';

describe('AssignAchievementModalComponent', () => {
  let component: AssignAchievementModalComponent;
  let fixture: ComponentFixture<AssignAchievementModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssignAchievementModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignAchievementModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
