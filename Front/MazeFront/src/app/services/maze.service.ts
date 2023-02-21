import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { MazeCreateDto } from "../interfaces/MazeCreateDto.interface";
import { MazeDto } from "../interfaces/MazeDto.interface";
import { MazePath } from "../interfaces/MazePath";
import { MazeReadDto } from "../interfaces/MazeReadDto.interface";
import { MazeSolveRequest } from "../interfaces/MazeSolveRequest";

@Injectable({
    providedIn: 'root',
  })
  export class MazeService {

    constructor(private htpp: HttpClient)
    {
        
    }

    public generateMaze(request: MazeCreateDto): Observable<MazeReadDto>
    {
        return this.htpp.post<MazeReadDto>("api/mazes/create", request);
    }

    public solveMaze(request: MazeSolveRequest): Observable<MazePath>
    {
      return this.htpp.post<MazePath>("api/mazes/solve", request);
    }

    public fetchMaze(id: number): Observable<MazeDto>
    {
        return this.htpp.get<MazeDto>("api/mazes/blob/" + id);
    }
  }