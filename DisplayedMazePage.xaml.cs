using System.Windows.Controls;
using System.Windows.Input;


namespace Maze_Puzzle_Generator
{
    /// <summary>
    /// Interaction logic for DisplayedMazePage.xaml
    /// </summary>
    public partial class DisplayedMazePage : Page
    {
        private static DisplayedMazePage currentDisplayedMazePage;


        public DisplayedMazePage(int totalNumberOfRows, int totalNumberOfColumns)
        {
            InitializeComponent();
            currentDisplayedMazePage = this;
            DisplayableGridOfMazeCells displayedMazeGrid = new DisplayableGridOfMazeCells(totalNumberOfRows, totalNumberOfColumns);
            displayedMazeGrid.MinHeight = totalNumberOfRows * 15;
            displayedMazeGrid.MinWidth = totalNumberOfColumns * 15;
            MainStackPanel.Children.Insert(0, displayedMazeGrid);
            UserInteractionWithMazeHandler.InitializeStartingCell();
            this.Focus(); // We must focus on something in order to fire the KeyDown event to call the Page_KeyDown function.
        }


        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            UserInteractionWithMazeHandler.AttemptToUpdateMazeWithUserInput(e.Key);
        }


        public static void UpdateDisplayedTotalNumberOfUserMovesMade(int totalNumberOfUserMovesMade)
        {
            currentDisplayedMazePage.TotalNumberOfUserMovesMade.Content = "Total moves made: " + totalNumberOfUserMovesMade;
        }
    }
}
