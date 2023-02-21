import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeViewComponent } from './maze-view.component';

describe('MazeViewComponent', () => {
  let component: MazeViewComponent;
  let fixture: ComponentFixture<MazeViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
