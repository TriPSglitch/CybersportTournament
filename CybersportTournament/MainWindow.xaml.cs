using System.Windows;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*if (AuthorizationWindow.user.Role == 1)
            {
                AddItem.Visibility = Visibility.Hidden;
            }*/
        }

        #region Переход на другие окна
        private void AddTeamClick(object sender, RoutedEventArgs e)
        {
            AddTeamWindow atw = new AddTeamWindow();
            atw.Show();
            this.Close();
        }

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
            AddPlayerWindow apw = new AddPlayerWindow();
            apw.Show();
            this.Close();
        }

        private void AddGameClick(object sender, RoutedEventArgs e)
        {
            AddGameWindow agw = new AddGameWindow();
            agw.Show();
            this.Close();
        }

        private void AddMapClick(object sender, RoutedEventArgs e)
        {
            AddMapWindow amw = new AddMapWindow();
            amw.Show();
            this.Close();
        }

        private void AddTournamentClick(object sender, RoutedEventArgs e)
        {
            AddTournamentWindow atw = new AddTournamentWindow();
            atw.Show();
            this.Close();
        }

        private void AddMatchClick(object sender, RoutedEventArgs e)
        {
            AddMatchWindow amw = new AddMatchWindow();
            amw.Show();
            this.Close();
        }
        #endregion
    }
}
