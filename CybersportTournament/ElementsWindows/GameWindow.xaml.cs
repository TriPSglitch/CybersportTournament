using System.Windows;
using CybersportTournament.ListWindows;
using ConnectionClass;
using System.Linq;
using System.Diagnostics;

namespace CybersportTournament.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Games game;
        public GameWindow(int id)
        {
            InitializeComponent();
            game = Connection.db.Games.Where(item => item.ID == id).FirstOrDefault();
            Name.Content = game.Name;
            Logo.Source = ImagesManip.NewImage(game);
            TournamentsList.ItemsSource = Connection.db.Tournaments.Where(item => item.IDGame == id).ToList();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            GamesListWindow glw = new GamesListWindow();
            glw.Show();
            this.Close();
        }

        private void TournamentsListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Tournaments)TournamentsList.SelectedItem).ID;
            TournamentWindow tw = new TournamentWindow(id);
            tw.Show();
            this.Hide();
        }

        private void LinkClick(object sender, RoutedEventArgs e)
        {
            Process.Start(game.Link.ToString());
        }
    }
}