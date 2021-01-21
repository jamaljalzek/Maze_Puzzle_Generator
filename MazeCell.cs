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
        private static SolidColorBrush pathColorBrush = Brushes.White;
        private static Random randomNumberGenerator = new Random();


        public MazeCell(int cellRowIndex, int cellColumnIndex)
        {
            this.cellRowIndex = cellRowIndex;
            this.cellColumnIndex = cellColumnIndex;
            hasThisCellBeenVisited = false;
            thisCellsBorderThickness = new Thickness(cellBorderThicknessFactor);
            this.BorderThickness = thisCellsBorderThickness;
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
            if (cellRowIndex == 0)
                return true;
            MazeCell cellAboveThisCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex - 1, cellColumnIndex);
            return cellAboveThisCell.hasThisCellBeenVisited;
        }


        private bool HasCellBelowThisCellBeenAlreadyVisited()
        {
            if (cellRowIndex == DisplayableGridOfMazeCells.totalNumberOfRows - 1)
                return true;
            MazeCell cellBelowThisCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex + 1, cellColumnIndex);
            return cellBelowThisCell.hasThisCellBeenVisited;
        }


        private bool HasCellLeftOfThisCellBeenAlreadyVisited()
        {
            if (cellColumnIndex == 0)
                return true;
            MazeCell cellToTheLeftOfThisCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex - 1);
            return cellToTheLeftOfThisCell.hasThisCellBeenVisited;
        }


        private bool HasCellRightOfThisCellBeenAlreadyVisited()
        {
            if (cellColumnIndex == DisplayableGridOfMazeCells.totalNumberOfColumns - 1)
                return true;
            MazeCell cellToTheRightOfThisCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex + 1);
            return cellToTheRightOfThisCell.hasThisCellBeenVisited;
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
            return null;
        }


        private MazeCell ReturnCellAboveCurrentCellAfterRemovingAdjacentWalls()
        {
            MazeCell cellAboveCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex - 1, cellColumnIndex);
            RemoveTopWall();
            cellAboveCurrentCell.RemoveBottomWall();
            return cellAboveCurrentCell;
        }


        private MazeCell ReturnCellBelowCurrentCellAfterRemovingAdjacentWalls()
        {
            MazeCell cellBelowCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex + 1, cellColumnIndex);
            RemoveBottomWall();
            cellBelowCurrentCell.RemoveTopWall();
            return cellBelowCurrentCell;
        }


        private MazeCell ReturnCellLeftOfCurrentCellAfterRemovingAdjacentWalls()
        {
            MazeCell cellLeftOfCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex - 1);
            RemoveLeftWall();
            cellLeftOfCurrentCell.RemoveRightWall();
            return cellLeftOfCurrentCell;
        }


        private MazeCell ReturnCellRightOfCurrentCellAfterRemovingAdjacentWalls()
        {
            MazeCell cellRightOfCurrentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(cellRowIndex, cellColumnIndex + 1);
            RemoveRightWall();
            cellRightOfCurrentCell.RemoveLeftWall();
            return cellRightOfCurrentCell;
        }
    }
}
