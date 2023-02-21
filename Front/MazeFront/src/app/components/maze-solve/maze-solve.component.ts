import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Algorithm } from 'src/app/interfaces/Algorithm';
import { Coordinate } from 'src/app/interfaces/Coordinate.interface';
import { MazeReadDto } from 'src/app/interfaces/MazeReadDto.interface';
import { MazeService } from 'src/app/services/maze.service';
import { MazeViewComponent } from '../maze-view/maze-view.component';

@Component({
  selector: 'app-maze-solve',
  templateUrl: './maze-solve.component.html',
  styleUrls: ['./maze-solve.component.css']
})
export class MazeSolveComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private mazeService: MazeService, private dialogRef: MatDialogRef<MazeViewComponent>) { }
  algorithms: Algorithm[] = [
    Algorithm.BFS, Algorithm.Dijkstra, Algorithm.Astar
  ]
  selectedValue: Algorithm;

  ngOnInit(): void {
  }

  solveMaze() {
    console.log(this.data);
    console.log(this.selectedValue);
    this.mazeService.solveMaze({
      id: this.data.id,
      algorithm: Object.keys(Algorithm).indexOf(this.selectedValue),
      start: this.data.start,
      end: this.data.end
    }).subscribe(response =>
      this.dialogRef.close({
        data: {
          status: response.status,
          path: response.path
        }
      }));
  }
}
