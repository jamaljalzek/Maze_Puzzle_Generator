using System.Diagnostics;
using System.Windows;

namespace Maze_Puzzle_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void GenerateMazeButtonClickHandler(object sender, RoutedEventArgs e)
        {
            int totalNumberOfRows = selectedMazeHeight.SelectedIndex * 10 + 10;
            int totalNumberOfColumns = selectedMazeWidth.SelectedIndex * 10 + 10;
            DisplayableGridOfMazeCells displayedMazeGrid = new DisplayableGridOfMazeCells(totalNumberOfRows, totalNumberOfColumns);
            this.Content = displayedMazeGrid;
        }


        private void LaunchMSPaintButtonClickHandler(object sender, RoutedEventArgs e)
        {
            Process.Start("mspaint.exe");
        }
    }
}
