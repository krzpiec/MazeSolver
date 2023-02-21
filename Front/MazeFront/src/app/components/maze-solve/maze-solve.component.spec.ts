import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeSolveComponent } from './maze-solve.component';

describe('MazeSolveComponent', () => {
  let component: MazeSolveComponent;
  let fixture: ComponentFixture<MazeSolveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeSolveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeSolveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
