using System.Windows;
using System.Windows.Navigation;


namespace Maze_Puzzle_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private static MainWindow mainWindow;


        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
        }


        public static void ResizeMainWindowToContent()
        {
            mainWindow.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
