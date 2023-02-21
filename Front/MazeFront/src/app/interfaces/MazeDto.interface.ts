export interface MazeDto {
    cells: Array<Array<CellDto>>;
}

export interface CellDto{
    walls: number;
}