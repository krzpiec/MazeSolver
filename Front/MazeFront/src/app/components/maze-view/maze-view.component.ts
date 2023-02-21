import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CellWalls } from 'src/app/interfaces/CellWalls.interace';
import { Coordinate } from 'src/app/interfaces/Coordinate.interface';
import { CellDto, MazeDto } from 'src/app/interfaces/MazeDto.interface';
import { MazeReadDto } from 'src/app/interfaces/MazeReadDto.interface';
import { Position } from 'src/app/interfaces/Position';
import { MazeService } from 'src/app/services/maze.service';
import { MazeGenerateComponent } from '../maze-generate/maze-generate.component';
import { MazeSolveComponent } from '../maze-solve/maze-solve.component';

@Component({
  selector: 'app-maze-view',
  templateUrl: './maze-view.component.html',
  styleUrls: ['./maze-view.component.css']
})
export class MazeViewComponent implements OnInit {

  @ViewChild('mazeCanvas', { static: true })
  mazeCanvas!: ElementRef;

  maze: MazeDto;
  mazeInfo: MazeReadDto;
  mazeContext: CanvasRenderingContext2D;
  mazeSolvePath: Coordinate[];
  startX: number;
  startY: number;
  endX: number;
  endY: number;

  private _mazeWidth = 1500;
  private _mazeHeight = 600;
  private start: Position;
  private end: Position;
  private cellSize: number;
  private drawingOffset: Coordinate;
  private readonly mazeColor = " #000000";
  private readonly pathColor = " #ff0000";
  private readonly startColor = "#00ff00";
  private readonly endColor = "#0000ff";
  constructor(private mazeService: MazeService, private dialogRef: MatDialog, private _snackBar: MatSnackBar) { }


  ngOnInit(): void {
    const mazeCanvas: HTMLCanvasElement = this.mazeCanvas.nativeElement;
    const mazeContext = mazeCanvas.getContext('2d');
    if (mazeContext) {
      this.mazeContext = mazeContext;
    }
  }

  setPositions() {
    if (!this.startX || !this.startY || !this.endX || !this.endY) {
      this.displaySnackbar("Set all values");
    }
    this.clearStartEndPositions();
    this.start = { x: this.startX, y: this.startY } as Position;
    this.end = { x: this.endX, y: this.endY } as Position;
    this.displaySnackbar("Positions set!");
    this.displayStartEndPostions();
  }

  displayStartEndPostions() {
    this.calculateDrawingOffset();
    var drawingPosition: Coordinate = {
      x: this.drawingOffset.x + this.cellSize / 2,
      y: this.drawingOffset.y + this.cellSize / 2
    };
    var acutalColor = this.mazeContext.fillStyle;

    this.mazeContext.beginPath();
    this.mazeContext.arc(drawingPosition.x + this.startY * this.cellSize, drawingPosition.y + this.startX * this.cellSize, this.cellSize / 4, 0, 2 * Math.PI);
    this.mazeContext.fillStyle = this.startColor;
    this.mazeContext.fill();

    this.mazeContext.beginPath();
    this.mazeContext.arc(drawingPosition.x + this.endY * this.cellSize, drawingPosition.y + this.endX * this.cellSize, this.cellSize / 4, 0, 2 * Math.PI);
    this.mazeContext.fillStyle = this.endColor;
    this.mazeContext.fill();

    this.mazeContext.fillStyle = acutalColor;
  }


  clearStartEndPositions() {
    if (!this.start || !this.end) {
      return;
    }

    this.calculateDrawingOffset();
    var drawingPosition: Coordinate = {
      x: this.drawingOffset.x + this.cellSize / 2,
      y: this.drawingOffset.y + this.cellSize / 2
    };
    var acutalColorFill = this.mazeContext.fillStyle;
    var acutalColorStroke = this.mazeContext.strokeStyle;

    this.mazeContext.beginPath();
    this.mazeContext.arc(drawingPosition.x + this.start.y * this.cellSize, drawingPosition.y + this.start.x * this.cellSize, this.cellSize / 4, 0, 2 * Math.PI);
    this.mazeContext.arc(drawingPosition.x + this.end.y * this.cellSize, drawingPosition.y + this.end.x * this.cellSize, this.cellSize / 4, 0, 2 * Math.PI);
    this.mazeContext.fillStyle = "#ffffff";
    this.mazeContext.strokeStyle = "#ffffff";
    this.mazeContext.fill();
    this.mazeContext.stroke();
    this.mazeContext.fillStyle = acutalColorFill;
    this.mazeContext.strokeStyle = acutalColorStroke;
  }

  openSolveMazeModal() {
    if (!this.mazeInfo) {
      this.displaySnackbar("First generate maze!");
      return;
    }
    this.dialogRef.open(MazeSolveComponent, {
      data: {
        id: this.mazeInfo.id,
        start: this.start,
        end: this.end
      }
    })
      .afterClosed().subscribe(response => {
        if (response.status == 0) {
          this.displaySnackbar("Nie znaleziono ścieżki");
          return;
        }
        this.mazeSolvePath = response.data.path;
        this.displaySolvePath();
      });
  }

  openMazeGenerationModal() {
    this.dialogRef.open(MazeGenerateComponent).afterClosed().subscribe(response => {
      this.mazeInfo = response.data;
      this.displaySnackbar("Maze created!");
      this.mazeService.fetchMaze(this.mazeInfo.id).subscribe(maze => {
        this.maze = maze;
        this.displayMaze();
      });
    });
  }

  private displaySnackbar(message: string) {
    this._snackBar.open(message, "OK");
  }

  private displayMaze() {
    this.clearCanvas();
    var cellSizeX = Math.floor(this._mazeWidth / this.maze.cells.length);
    var cellSizeY = Math.floor(this._mazeHeight / this.maze.cells[0].length);
    this.cellSize = cellSizeX < cellSizeY ? cellSizeX : cellSizeY;

    this.calculateDrawingOffset();

    for (var row = 0; row < this.maze.cells.length; row++) {
      for (var column = 0; column < this.maze.cells[row].length; column++) {
        this.displayCell(row, column)
      }
    }
  }

  private displaySolvePath() {
    this.displayMaze();
    this.mazeContext.strokeStyle = this.pathColor;
    var drawingPosition: Coordinate = {
      x: this.drawingOffset.x + this.cellSize / 2,
      y: this.drawingOffset.y + this.cellSize / 2
    };
    this.mazeContext.beginPath();
    this.mazeContext.moveTo(drawingPosition.x + this.startY * this.cellSize, drawingPosition.y + this.startX * this.cellSize);
    for (var i = 0; i < this.mazeSolvePath.length; i++) {
      this.mazeContext.lineTo(this.mazeSolvePath[i].y * this.cellSize + drawingPosition.x, this.mazeSolvePath[i].x * this.cellSize + drawingPosition.y);
    }
    this.mazeContext.stroke();
    this.mazeContext.strokeStyle = this.mazeColor;
  }

  private displayCell(row: number, column: number) {
    var drawingPosition: Coordinate = {
      x: this.drawingOffset.x + column * this.cellSize,
      y: this.drawingOffset.y + row * this.cellSize
    };
    var cell: CellDto = this.maze.cells[row][column];
    var cellWalls: CellWalls = this.getCellWalls(cell.walls);



    this.mazeContext.beginPath();
    this.mazeContext.moveTo(drawingPosition.x, drawingPosition.y);

    if (cellWalls.up) {
      this.mazeContext.lineTo(drawingPosition.x + this.cellSize, drawingPosition.y);
    }
    else {
      this.mazeContext.moveTo(drawingPosition.x + this.cellSize, drawingPosition.y);
    }


    if (cellWalls.right) {
      this.mazeContext.lineTo(drawingPosition.x + this.cellSize, drawingPosition.y + this.cellSize);
    }
    else {
      this.mazeContext.moveTo(drawingPosition.x + this.cellSize, drawingPosition.y + this.cellSize);
    }


    if (cellWalls.bottom) {
      this.mazeContext.lineTo(drawingPosition.x, drawingPosition.y + this.cellSize);
    }
    else {
      this.mazeContext.moveTo(drawingPosition.x, drawingPosition.y + this.cellSize);
    }

    if (cellWalls.left) {
      this.mazeContext.lineTo(drawingPosition.x, drawingPosition.y);
    }
    else {
      this.mazeContext.moveTo(drawingPosition.x, drawingPosition.y);
    }

    this.mazeContext.stroke();
    this.displayStartEndPostions();
  }


  private calculateDrawingOffset() {
    var positionOffsetX = (this._mazeWidth - this.maze.cells.length * this.cellSize) / 2;
    var positionOffsetY = (this._mazeHeight - this.maze.cells[0].length * this.cellSize) / 2;
    this.drawingOffset = {
      x: positionOffsetX,
      y: positionOffsetY
    } as Coordinate
  }

  private getCellWalls(walls: number): CellWalls {
    var test = walls.toString();
    var wallsString = "";
    for (var i = 0; i < 4 - test.length; i++) {
      wallsString += "0"
    }
    wallsString += test;

    var up = Boolean(Number(wallsString[0]));
    var right = Boolean(Number(wallsString[1]));
    var down = Boolean(Number(wallsString[2]));
    var left = Boolean(Number(wallsString[3]));
    return {
      up: up,
      right: right,
      bottom: down,
      left: left
    } as CellWalls
  }

  private clearCanvas() {
    this.mazeContext.clearRect(0, 0, this._mazeWidth, this._mazeHeight);
  }



  private resolveGridPostition(mouseX: number, mouseY: number): Position {
    return {
      x: Math.floor(mouseX / this.cellSize),
      y: Math.floor(mouseY / this.cellSize)
    }
  }
}

// function handleMoseMove(e : MouseEvent, cellSize: number )
// {
//   if(!this.mazeInfo)
//   {
//     return;
//   }
//   var pos = resolveGridPostition(e.clientX, e.clientY, cellSize);
//   console.log(pos);
// }

// function resolveGridPostition(mouseX: number, mouseY: number, cellSize:number): Position
//   {
//       return {
//         x: Math.floor(mouseX/cellSize),
//         y: Math.floor(mouseY/cellSize)
//       }
//   }
