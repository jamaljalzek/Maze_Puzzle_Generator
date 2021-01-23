using System.Windows;
using System.Windows.Controls;


namespace Maze_Puzzle_Generator
{
    /// <summary>
    /// Interaction logic for StartingMenuPage.xaml
    /// </summary>
    public partial class StartingMenuPage : Page
    {
        public StartingMenuPage()
        {
            InitializeComponent();
        }


        private void GenerateMazeButtonClickHandler(object sender, RoutedEventArgs e)
        {
            int totalNumberOfRows = SelectedMazeHeight.SelectedIndex * 10 + 10;
            int totalNumberOfColumns = SelectedMazeWidth.SelectedIndex * 10 + 10;
            DisplayedMazePage displayedMazePage = new DisplayedMazePage(totalNumberOfRows, totalNumberOfColumns);
            this.NavigationService.Navigate(displayedMazePage);
        }
    }
}
