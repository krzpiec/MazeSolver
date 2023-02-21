import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeMainComponent } from './maze-main.component';

describe('MazeMainComponent', () => {
  let component: MazeMainComponent;
  let fixture: ComponentFixture<MazeMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeMainComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
