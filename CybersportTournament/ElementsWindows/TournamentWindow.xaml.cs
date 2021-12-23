using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using ConnectionClass;
using CybersportTournament.ListWindows;

namespace CybersportTournament.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для TournamentWindow.xaml
    /// </summary>
    public partial class TournamentWindow : Window
    {
        Tournaments tournament;
        public TournamentWindow(int id)
        {
            InitializeComponent();
            tournament = Connection.db.Tournaments.Where(item => item.ID == id).FirstOrDefault();
            Name.Content = tournament.Name;
            Game.Content = tournament.Games.Name;
            PrizeFund.Content = tournament.PrizeFund;
            Logo.Source = NewImage(tournament);
            if (Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID).Select(item => item.NumberTeamList).Contains(1))
            {
                FMatchFTeam.Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == 1).Select(item => item.Teams.Name).FirstOrDefault();
                int firstTeamID = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == 1).Select(item => item.IDTeam).FirstOrDefault();
                FMatchSTeam.Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == 1 
                                                                    && item.IDTeam != firstTeamID).Select(item => item.Teams.Name).FirstOrDefault();

            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            TournamentsListWindow mw = new TournamentsListWindow();
            mw.Show();
            this.Close();
        }

        private BitmapImage NewImage(Tournaments tournament)
        {
            #region Декодирование картинки из бд
            MemoryStream ms = new MemoryStream(tournament.Logo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
            #endregion
        }
    }
}
