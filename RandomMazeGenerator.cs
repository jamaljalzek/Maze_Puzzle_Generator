namespace Maze_Puzzle_Generator
{
    static class RandomMazeGenerator
    {
        public static void AddNewRandomMaze(MazeCell startingCell)
        {
            GenerateNewRandomMazeByRandomizedRecursiveBacktracking(startingCell);
        }


        private static void GenerateNewRandomMazeByRandomizedRecursiveBacktracking(MazeCell currentCell)
        {
            currentCell.MarkThisCellAsVisited();
            while (currentCell.DoesThisCellHaveAnyUnvisitedNeighbors())
            {
                MazeCell nextNeighborCellToMoveTo = currentCell.ChooseRandomUnvisitedNeighborCellAndRemoveAdjacentWalls();
                GenerateNewRandomMazeByRandomizedRecursiveBacktracking(nextNeighborCellToMoveTo);
            }
        }
    }
}
