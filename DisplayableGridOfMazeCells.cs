using System.Windows.Controls;
using System.Windows.Media;


namespace Maze_Puzzle_Generator
{
    class DisplayableGridOfMazeCells : Grid
    {
        public static int totalNumberOfRows, totalNumberOfColumns;
        private static MazeCell[,] mazeCellsQuickReference;


        public DisplayableGridOfMazeCells(int totalNumberOfRows, int totalNumberOfColumns)
        {
            InitializeClassFields(totalNumberOfRows, totalNumberOfColumns);
            DefineRowsAndColumns();
            FillMazeGridWithCells();
            AddEntryAndExitPoints();
            RandomMazeGenerator.AddNewRandomMaze(mazeCellsQuickReference[1,0]);
        }


        private void InitializeClassFields(int totalNumberOfRows, int totalNumberOfColumns)
        {
            DisplayableGridOfMazeCells.totalNumberOfRows = totalNumberOfRows;
            DisplayableGridOfMazeCells.totalNumberOfColumns = totalNumberOfColumns;
            mazeCellsQuickReference = new MazeCell[totalNumberOfRows, totalNumberOfColumns];
        }


        private void DefineRowsAndColumns()
        {
            for (int currentRowNumber = 0; currentRowNumber < totalNumberOfRows; ++currentRowNumber)
                RowDefinitions.Add(new RowDefinition());
            for (int currentColumnNumber = 0; currentColumnNumber < totalNumberOfColumns; ++currentColumnNumber)
                ColumnDefinitions.Add(new ColumnDefinition());
        }


        private void FillMazeGridWithCells()
        {
            for (int currentRowNumber = 0; currentRowNumber < totalNumberOfRows; ++currentRowNumber)
                for (int currentColumnNumber = 0; currentColumnNumber < totalNumberOfColumns; ++currentColumnNumber)
                    AddNewCell(currentRowNumber, currentColumnNumber);
        }


        private void AddNewCell(int currentRowNumber, int currentColumnNumber)
        {
            MazeCell currentCell = new MazeCell(currentRowNumber, currentColumnNumber);
            this.Children.Add(currentCell);
            Grid.SetRow(currentCell, currentRowNumber);
            Grid.SetColumn(currentCell, currentColumnNumber);
            mazeCellsQuickReference[currentRowNumber, currentColumnNumber] = currentCell;
        }


        private void AddEntryAndExitPoints()
        {
            MazeCell mazeEntryPoint = mazeCellsQuickReference[1, 0]; // Create an entry point in the upper left corner of the maze.
            mazeEntryPoint.RemoveLeftWall();
            int lastRowIndex = totalNumberOfRows - 1;
            int lastColumnIndex = totalNumberOfColumns - 1;
            MazeCell mazeExitPoint = mazeCellsQuickReference[lastRowIndex - 1, lastColumnIndex]; // Create an exit point in the lower right corner of the maze.
            mazeExitPoint.Background = Brushes.Red;
            mazeExitPoint.RemoveRightWall();
        }


        public static MazeCell GetCellAtGivenRowAndColumnIndex(int rowIndex, int columnIndex)
        {
            return mazeCellsQuickReference[rowIndex, columnIndex];
        }
    }
}
