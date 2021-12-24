using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using ConnectionClass;
using CybersportTournament.ListWindows;
using CybersportTournament.AddWindows;
using System.Windows.Controls;
using System.Collections.Generic;

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
            Label[,] labels = new Label[8, 2] { { FMatchFTeam, FMatchSTeam }, {SMatchFTeam, SMatchSTeam }, { TMatchFTeam, TMatchSTeam },
                { FoMatchFTeam, FoMatchSTeam }, { FiMatchFTeam, FiMatchSTeam }, { SiMatchFTeam, SiMatchSTeam }, { SeMatchFTeam, SeMatchSTeam }, { Winner, Winner } };
            for (int i = 1; i < 8; i++)
            {
                if (Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID).Select(item => item.NumberTeamList).Contains(i))
                {
                    labels[i - 1, 0].Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == i).Select(item => item.Teams.Name).FirstOrDefault();
                    int firstTeamID = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == i).Select(item => item.IDTeam).FirstOrDefault();
                    labels[i - 1, 1].Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == i
                                                                         && item.IDTeam != firstTeamID).Select(item => item.Teams.Name).FirstOrDefault();

                }
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

        private void FMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            #region Переход на матчи четвертьфинала
            if (FMatchFTeam.Content.ToString() != "" && FMatchSTeam.Content.ToString() != "")
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 1).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
            else if (FMatchFTeam.Content.ToString() == "" || FMatchSTeam.Content.ToString() == "")
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID);
                amw.Show();
                this.Close();
            }
        }

        private void SMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SMatchFTeam.Content.ToString() != "" && SMatchSTeam.Content.ToString() != "")
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 2).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
            else if (SMatchFTeam.Content.ToString() == "" || SMatchSTeam.Content.ToString() == "")
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID);
                amw.Show();
                this.Close();
            }
        }

        private void TMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (TMatchFTeam.Content.ToString() != "" && TMatchSTeam.Content.ToString() != "")
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 3).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
            else if (TMatchFTeam.Content.ToString() == "" || TMatchSTeam.Content.ToString() == "")
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID);
                amw.Show();
                this.Close();
            }
        }

        private void FoMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (FoMatchFTeam.Content.ToString() != "" && FoMatchFTeam.Content.ToString() != "")
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 4).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
            else if (FoMatchFTeam.Content.ToString() == "" || FoMatchFTeam.Content.ToString() == "")
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID);
                amw.Show();
                this.Close();
            }
            #endregion
        }

        private void FiMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((FMatchFTeam.Content.ToString() != "" && FMatchSTeam.Content.ToString() != "" 
                && SMatchFTeam.Content.ToString() !=  "" && SMatchSTeam.Content.ToString() != "") 
                && (FiMatchFTeam.Content.ToString() == "" && FiMatchSTeam.Content.ToString() == ""))
            {
                List<int> teams = new List<int>();
                teams.Add(Connection.db.Teams.Where(item => item.Name == FMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault());
                teams.Add(Connection.db.Teams.Where(item => item.Name == FMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault());
                teams.Add(Connection.db.Teams.Where(item => item.Name == SMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault());
                teams.Add(Connection.db.Teams.Where(item => item.Name == SMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault());
                AddMatchWindow mw = new AddMatchWindow(tournament.ID, teams);
                mw.Show();
                this.Close();
            }
            else if (FiMatchFTeam.Content.ToString() != "" && FiMatchSTeam.Content.ToString() != "")
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 5).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
        }
    }
}