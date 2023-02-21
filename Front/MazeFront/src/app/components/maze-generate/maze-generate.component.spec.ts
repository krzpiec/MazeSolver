import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MazeGenerateComponent } from './maze-generate.component';

describe('MazeGenerateComponent', () => {
  let component: MazeGenerateComponent;
  let fixture: ComponentFixture<MazeGenerateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MazeGenerateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MazeGenerateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
