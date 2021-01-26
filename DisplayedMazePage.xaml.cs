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
            DisplayNewRandomlyGeneratedMaze(totalNumberOfRows, totalNumberOfColumns);
            UserInteractionWithMazeHandler.InitializeStartingCell();
            this.Focus(); // We must focus on something in order to fire the KeyDown event to call the Page_KeyDown function.
            MainWindow.ResizeMainWindowToContent(); // When the user navigates back to the main menu, and chooses a different maze size, we want the MainWindow to resize itself automatically to the new content.
        }


        private void DisplayNewRandomlyGeneratedMaze(int totalNumberOfRows, int totalNumberOfColumns)
        {
            DisplayableGridOfMazeCells displayedMazeGrid = new DisplayableGridOfMazeCells(totalNumberOfRows, totalNumberOfColumns);
            MainGrid.Children.Add(displayedMazeGrid);
            Grid.SetRow(displayedMazeGrid, 0);
        }


        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            UserInteractionWithMazeHandler.AttemptToUpdateMazeWithUserInput(e.Key);
        }


        public static void UpdateDisplayedTotalNumberOfUserMovesMade(int totalNumberOfUserMovesMade)
        {
            currentDisplayedMazePage.TotalNumberOfUserMovesMadeLabel.Content = "Moves made: " + totalNumberOfUserMovesMade;
        }


        private void ViewControlsButtonClickHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            UserControlsInformationPage userControlsInformationPage = new UserControlsInformationPage();
            this.NavigationService.Navigate(userControlsInformationPage);
        }
    }
}
