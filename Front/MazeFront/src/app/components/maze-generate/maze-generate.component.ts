import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MazeCreateDto } from 'src/app/interfaces/MazeCreateDto.interface';
import { MazeService } from 'src/app/services/maze.service';
import { MazeViewComponent } from '../maze-view/maze-view.component';

@Component({
  selector: 'app-maze-generate',
  templateUrl: './maze-generate.component.html',
  styleUrls: ['./maze-generate.component.css']
})
export class MazeGenerateComponent implements OnInit {

  constructor(private _snackBar: MatSnackBar, private mazeService: MazeService, private dialogRef: MatDialogRef<MazeViewComponent>) { }

  sizeX: number;
  sizeY: number;
  name: string;
  description: string;


  ngOnInit(): void {
  }

  generateMaze(){
    if(!this.vaidateData())
    {
      return;
    }
    this.mazeService.generateMaze(
      {
        sizeX: this.sizeX,
        sizeY: this.sizeY,
        name: this.name,
        description: this.description,
      } as MazeCreateDto
    ) 
    .subscribe(mazeInfo => this.dialogRef.close({data: mazeInfo}));
  }

  private vaidateData(): boolean {
    if(this.name == undefined || this.description == undefined || this.sizeX == undefined || this.sizeY == undefined)
    {
      this._snackBar.open("Złe dane!");
      return false;
    }

    if(this.sizeX <= 0 || this.sizeY <= 0)
    {
      this._snackBar.open("Rozmiar musi być większy od 0!");
      return false;
    }

    return true;
  }
}
