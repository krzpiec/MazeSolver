import { Position } from "./Position";
import { Algorithm } from "./Algorithm";

export interface MazeSolveRequest
{
    id: number,
    algorithm: number,
    start: Position,
    end: Position
}