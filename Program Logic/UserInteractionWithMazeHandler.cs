using System.Windows.Input;
using System.Windows.Media;

namespace Maze_Puzzle_Generator
{
    static class UserInteractionWithMazeHandler
    {
        private static MazeCell currentCell;
        private static int totalNumberOfMovesMadeByUser;
        private static Brush currentCellBackgroundColor = Brushes.Green;
        private static Brush visitedCellBackgroundColor = Brushes.LightGreen;


        public static void InitializeStartingCell()
        {
            currentCell = DisplayableGridOfMazeCells.GetCellAtGivenRowAndColumnIndex(1, 0);
            currentCell.Background = currentCellBackgroundColor;
            totalNumberOfMovesMadeByUser = 0;
        }


        public static void AttemptToUpdateMazeWithUserInput(Key userKeyInput)
        {
            if (userKeyInput == Key.W || userKeyInput == Key.I)
                AttemptToMoveUp();
            else if (userKeyInput == Key.S || userKeyInput == Key.K)
                AttemptToMoveDown();
            else if (userKeyInput == Key.A || userKeyInput == Key.J)
                AttemptToMoveLeft();
            else if (userKeyInput == Key.D || userKeyInput == Key.L)
                AttemptToMoveRight();
        }


        private static void AttemptToMoveUp()
        {
            if (currentCell.CanWeMoveToCellAboveThisCell())
            {
                MazeCell cellAboveCurrentCell = currentCell.GetCellAboveThisCell();
                if (cellAboveCurrentCell.Background == visitedCellBackgroundColor) // Check if we are going to backtrack.
                    currentCell.Background = MazeCell.unvisitedCellPathColor;
                else
                    currentCell.Background = visitedCellBackgroundColor;
                currentCell = cellAboveCurrentCell;
                currentCell.Background = currentCellBackgroundColor;
                ++totalNumberOfMovesMadeByUser;
                DisplayedMazePage.UpdateDisplayedTotalNumberOfUserMovesMade(totalNumberOfMovesMadeByUser);
            }
        }


        private static void AttemptToMoveDown()
        {
            if (currentCell.CanWeMoveToCellBelowThisCell())
            {
                MazeCell cellBelowCurrentCell = currentCell.GetCellBelowThisCell();
                if (cellBelowCurrentCell.Background == visitedCellBackgroundColor) // Check if we are going to backtrack.
                    currentCell.Background = MazeCell.unvisitedCellPathColor;
                else
                    currentCell.Background = visitedCellBackgroundColor;
                currentCell = cellBelowCurrentCell;
                currentCell.Background = currentCellBackgroundColor;
                ++totalNumberOfMovesMadeByUser;
                DisplayedMazePage.UpdateDisplayedTotalNumberOfUserMovesMade(totalNumberOfMovesMadeByUser);
            }
        }


        private static void AttemptToMoveLeft()
        {
            if (currentCell.CanWeMoveToCellLeftOfThisCell())
            {
                MazeCell cellLeftOfCurrentCell = currentCell.GetCellLeftOfThisCell();
                if (cellLeftOfCurrentCell.Background == visitedCellBackgroundColor) // Check if we are going to backtrack.
                    currentCell.Background = MazeCell.unvisitedCellPathColor;
                else
                    currentCell.Background = visitedCellBackgroundColor;
                currentCell = cellLeftOfCurrentCell;
                currentCell.Background = currentCellBackgroundColor;
                ++totalNumberOfMovesMadeByUser;
                DisplayedMazePage.UpdateDisplayedTotalNumberOfUserMovesMade(totalNumberOfMovesMadeByUser);
            }
        }


        private static void AttemptToMoveRight()
        {
            if (currentCell.CanWeMoveToCellRightOfThisCell())
            {
                MazeCell cellRightOfCurrentCell = currentCell.GetCellRightOfThisCell();
                if (cellRightOfCurrentCell.Background == visitedCellBackgroundColor) // Check if we are going to backtrack.
                    currentCell.Background = MazeCell.unvisitedCellPathColor;
                else
                    currentCell.Background = visitedCellBackgroundColor;
                currentCell = cellRightOfCurrentCell;
                currentCell.Background = currentCellBackgroundColor;
                ++totalNumberOfMovesMadeByUser;
                DisplayedMazePage.UpdateDisplayedTotalNumberOfUserMovesMade(totalNumberOfMovesMadeByUser);
            }
        }
    }
}
