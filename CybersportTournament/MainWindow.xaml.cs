using CybersportTournament.ListWindows;
using System.Windows;
using CybersportTournament.AddWindows;

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
            if (User.Role != 1)
            {
                AddItem.Visibility = Visibility.Hidden;
            }
        }

        #region Переход на другие окна

        #region Окна добавления
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

        private void AddTournamentClick(object sender, RoutedEventArgs e)
        {
            AddTournamentWindow atw = new AddTournamentWindow();
            atw.Show();
            this.Close();
        }
        #endregion

        #region Окна списков
        private void TeamsListClick(object sender, RoutedEventArgs e)
        {
            TeamsListWindow tlw = new TeamsListWindow();
            tlw.Show();
            this.Close();
        }

        private void PlayersListClick(object sender, RoutedEventArgs e)
        {
            PlayersListWindow plw = new PlayersListWindow();
            plw.Show();
            this.Close();
        }

        private void GamesListClick(object sender, RoutedEventArgs e)
        {
            GamesListWindow glw = new GamesListWindow();
            glw.Show();
            this.Close();
        }

        private void TournamrntsListClick(object sender, RoutedEventArgs e)
        {
            TournamentsListWindow tlw = new TournamentsListWindow();
            tlw.Show();
            this.Close();
        }

        private void MatchesListClick(object sender, RoutedEventArgs e)
        {
            MatchesListWindow mlw = new MatchesListWindow();
            mlw.Show();
            this.Close();
        }
        #endregion

        #endregion

    }
}