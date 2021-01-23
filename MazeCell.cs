using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maze_Puzzle_Generator
{
    class MazeCell : Border
    {
        private int cellRowIndex, cellColumnIndex;
        private bool hasThisCellBeenVisited;
        private Thickness thisCellsBorderThickness;

        private static double cellBorderThicknessFactor = 1.0;
        private static SolidColorBrush wallColorBrush = Brushes.Black;
        public static SolidColorBrush pathColorBrush = Brushes.White;
        private static Random randomNumberGenerator = new Random();


        public MazeCell(int cellRowIndex, int cellColumnIndex)
        {
            this.cellRowIndex = cellRowIndex;
            this.cellColumnIndex = cellColumnIndex;
            hasThisCellBeenVisited = false;
            thisCellsBorderThickness = new Thickness(cellBorderThicknessFactor);
            this.BorderThickness = thisCellsBorderThickness; // Changes directly to this.BorderThickness (such as when we want to update one of its walls) throws a compilation error, even when we explicitly initialize it with a new Thickness, due to this.BorderThickness not having a defult value. So we use thisCellsBorderThickness as a workaround.
            this.BorderBrush = wallColorBrush;
            this.Background = pathColorBrush;
        }


        public void RemoveTopWall()
        {
            thisCellsBorderThickness.Top = 0.0;
            UpdateDisplayedBorderThickness();
        }


        private void UpdateDisplayedBorderThickness()
        {
            this.BorderThickness = thisCellsBorderThickness; // We perform a reassignment statement to trigger an update effect, which repaints this border and visually shows any removed wall.
        }


        public void RemoveBottomWall()
        {
            thisCellsBorderThickness.Bottom = 0.0;
            UpdateDisplayedBorderThickness();
        }


        public void RemoveLeftWall()
        {
            thisCellsBorderThickness.Left = 0.0;
            UpdateDisplayedBorderThickness();
        }


        public void RemoveRightWall()
        {
            thisCellsBorderThickness.Right = 0.0;
            UpdateDisplayedBorderThickness();
        }


        public void MarkThisCellAsVisited()
        {
            hasThisCellBeenVisited = true;
        }


        public bool DoesThisCellHaveAnyUnvisitedNeighbors()
        {
            return (!HasCellAboveThisCellBeenAlreadyVisited() || !HasCellBelowThisCellBeenAlreadyVisited() ||
                    !HasCellLeftOfThisCellBeenAlreadyVisited() || !HasCellRightOfThisCellBeenAlreadyVisited());
        }


        private bool HasCellAboveThisCellBeenAlreadyVisited()
        {
            if (cellRowIndex == 0) // If this cell is in the topmost row, then we avoid moving up which would go out of the bounds of the grid.
                return true;
            return GetCellAboveThisCell().hasThisCellBeenVisited;
        }


        public MazeCell GetCellAboveThisCell()
        {
            return DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex - 1, cellColumnIndex);
        }


        private bool HasCellBelowThisCellBeenAlreadyVisited()
        {
            if (cellRowIndex == DisplayableGridOfMazeCells.totalNumberOfRows - 1) // If this cell is in the bottommost row, then we avoid moving down which would go out of the bounds of the grid.
                return true;
            return GetCellBelowThisCell().hasThisCellBeenVisited;
        }


        public MazeCell GetCellBelowThisCell()
        {
            return DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex + 1, cellColumnIndex);
        }


        private bool HasCellLeftOfThisCellBeenAlreadyVisited()
        {
            if (cellColumnIndex == 0) // If this cell is in the leftmost column, then we avoid moving left which would go out of the bounds of the grid.
                return true;
            return GetCellLeftOfThisCell().hasThisCellBeenVisited;
        }


        public MazeCell GetCellLeftOfThisCell()
        {
            return DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex - 1);
        }


        private bool HasCellRightOfThisCellBeenAlreadyVisited()
        {
            if (cellColumnIndex == DisplayableGridOfMazeCells.totalNumberOfColumns - 1) // If this cell is in the rightmost column, then we avoid moving right which would go out of the bounds of the grid.
                return true;
            return GetCellRightOfThisCell().hasThisCellBeenVisited;
        }


        public MazeCell GetCellRightOfThisCell()
        {
            return DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex + 1);
        }


        public MazeCell ChooseRandomUnvisitedNeighborCellAndRemoveAdjacentWalls()
        {
            while (true)
            {
                int nextCellToVisit = randomNumberGenerator.Next(4);
                if (nextCellToVisit == 0 && !HasCellAboveThisCellBeenAlreadyVisited())
                    return ReturnCellAboveCurrentCellAfterRemovingAdjacentWalls();
                if (nextCellToVisit == 1 && !HasCellBelowThisCellBeenAlreadyVisited())
                    return ReturnCellBelowCurrentCellAfterRemovingAdjacentWalls();
                if (nextCellToVisit == 2 && !HasCellLeftOfThisCellBeenAlreadyVisited())
                    return ReturnCellLeftOfCurrentCellAfterRemovingAdjacentWalls();
                if (nextCellToVisit == 3 && !HasCellRightOfThisCellBeenAlreadyVisited())
                    return ReturnCellRightOfCurrentCellAfterRemovingAdjacentWalls();
            }
        }


        private MazeCell ReturnCellAboveCurrentCellAfterRemovingAdjacentWalls()
        {
            RemoveTopWall();
            MazeCell cellAboveCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex - 1, cellColumnIndex);
            cellAboveCurrentCell.RemoveBottomWall();
            return cellAboveCurrentCell;
        }


        private MazeCell ReturnCellBelowCurrentCellAfterRemovingAdjacentWalls()
        {
            RemoveBottomWall();
            MazeCell cellBelowCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex + 1, cellColumnIndex);
            cellBelowCurrentCell.RemoveTopWall();
            return cellBelowCurrentCell;
        }


        private MazeCell ReturnCellLeftOfCurrentCellAfterRemovingAdjacentWalls()
        {
            RemoveLeftWall();
            MazeCell cellLeftOfCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex - 1);
            cellLeftOfCurrentCell.RemoveRightWall();
            return cellLeftOfCurrentCell;
        }


        private MazeCell ReturnCellRightOfCurrentCellAfterRemovingAdjacentWalls()
        {
            RemoveRightWall();
            MazeCell cellRightOfCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex + 1);
            cellRightOfCurrentCell.RemoveLeftWall();
            return cellRightOfCurrentCell;
        }


        public bool CanWeMoveToCellAboveThisCell()
        {
            return cellRowIndex != 0 && !HasTopWall() && !GetCellAboveThisCell().HasBottomWall();
        }


        private bool HasTopWall()
        {
            return this.BorderThickness.Top != 0.0;
        }


        private bool HasBottomWall()
        {
            return this.BorderThickness.Bottom != 0.0;
        }


        public bool CanWeMoveToCellBelowThisCell()
        {
            return cellRowIndex != (DisplayableGridOfMazeCells.totalNumberOfRows - 1) && !HasBottomWall() && !GetCellBelowThisCell().HasTopWall();
        }


        public bool CanWeMoveToCellLeftOfThisCell()
        {
            return cellColumnIndex != 0 && !HasLeftWall() && !GetCellLeftOfThisCell().HasRightWall();
        }


        private bool HasLeftWall()
        {
            return this.BorderThickness.Left != 0.0;
        }


        private bool HasRightWall()
        {
            return this.BorderThickness.Right != 0.0;
        }


        public bool CanWeMoveToCellRightOfThisCell()
        {
            return cellColumnIndex != (DisplayableGridOfMazeCells.totalNumberOfColumns - 1) && !HasRightWall() && !GetCellRightOfThisCell().HasLeftWall();
        }
    }
}
