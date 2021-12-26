using System.Linq;
using System.Windows;
using ConnectionClass;
using CybersportTournament.ListWindows;
using CybersportTournament.AddWindows;
using System.Windows.Controls;
using System;

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
            Logo.Source = ImagesManip.NewImage(tournament);
            Label[,] labels = new Label[7, 4] { { FMatchFTeam, FMatchSTeam, FMatchFTeamScore, FMatchSTeamScore }, {SMatchFTeam, SMatchSTeam, SMatchFTeamScore, SMatchSTeamScore },
                                                { TMatchFTeam, TMatchSTeam, TMatchFTeamScore, TMatchSTeamScore },{ FoMatchFTeam, FoMatchSTeam, FoMatchFTeamScore, FoMatchSTeamScore },
                                                { FiMatchFTeam, FiMatchSTeam, FiMatchFTeamScore, FiMatchSTeamScore },{ SiMatchFTeam, SiMatchSTeam, SiMatchFTeamScore, SiMatchSTeamScore },
                                                { SeMatchFTeam, SeMatchSTeam, SeMatchFTeamScore, SeMatchSTeamScore } };
            for (int i = 1; i < 8; i++)
            {
                if (Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID).Select(item => item.NumberTeamList).Contains(i))
                {
                    labels[i - 1, 0].Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == i).Select(item => item.Teams.Name).FirstOrDefault();
                    int firstTeamID = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == i).Select(item => item.IDTeam).FirstOrDefault();
                    labels[i - 1, 1].Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.NumberTeamList == i
                                                                         && item.IDTeam != firstTeamID).Select(item => item.Teams.Name).FirstOrDefault();

                    string matchResult = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == i).Select(item => item.Match.Result).FirstOrDefault();
                    int length = matchResult.IndexOf(':');
                    int firstTeamScore = Convert.ToInt32(matchResult.Substring(0, length));
                    int secondTeamScore = Convert.ToInt32(matchResult.Substring(length + 1, matchResult.Length - length - 1));

                    labels[i - 1, 2].Content = firstTeamScore;
                    labels[i - 1, 3].Content = secondTeamScore;
                }
            }
            Winner.Content = Connection.db.TeamsList.Where(item => item.IDTournament == tournament.ID && item.Tournaments.Winner == item.IDTeam).Select(item => item.Teams.Name).FirstOrDefault();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            TournamentsListWindow mw = new TournamentsListWindow();
            mw.Show();
            this.Close();
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
            else if ((FMatchFTeam.Content.ToString() == "" || FMatchSTeam.Content.ToString() == "") && User.Role == 0)
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID, 1);
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
            else if ((SMatchFTeam.Content.ToString() == "" || SMatchSTeam.Content.ToString() == "") && User.Role == 0)
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID, 2);
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
            else if ((TMatchFTeam.Content.ToString() == "" || TMatchSTeam.Content.ToString() == "") && User.Role == 0)
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID, 3);
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
            else if ((FoMatchFTeam.Content.ToString() == "" || FoMatchFTeam.Content.ToString() == "") && User.Role == 0)
            {
                AddMatchWindow amw = new AddMatchWindow(tournament.ID, 4);
                amw.Show();
                this.Close();
            }
            #endregion
        }

        private void FiMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            #region Переход на матчи полуфинала и финала
            if (FMatchFTeamScore.Content.ToString() == "0" && FMatchSTeamScore.Content.ToString() == "0"
                && SMatchFTeamScore.Content.ToString() == "0" && SMatchSTeamScore.Content.ToString() == "0")
                return;

            if (((FiMatchFTeam.Content.ToString() == "" && FiMatchSTeam.Content.ToString() == "")
                && (Convert.ToInt32(FMatchFTeamScore.Content) != Convert.ToInt32(FMatchSTeamScore.Content)
                && Convert.ToInt32(SMatchFTeamScore.Content) != Convert.ToInt32(SMatchSTeamScore.Content))) && User.Role == 0)
            {
                int FirstTeamNextMatchID = 0, SecondTeamNextMatchID = 0;
                if (Convert.ToInt32(FMatchFTeamScore.Content) > Convert.ToInt32(FMatchSTeamScore.Content))
                    FirstTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == FMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                else if (Convert.ToInt32(FMatchFTeamScore.Content) < Convert.ToInt32(FMatchSTeamScore.Content))
                    FirstTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == FMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();

                if (Convert.ToInt32(SMatchFTeamScore.Content) > Convert.ToInt32(SMatchSTeamScore.Content))
                    SecondTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == SMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                else if (Convert.ToInt32(SMatchFTeamScore.Content) < Convert.ToInt32(SMatchSTeamScore.Content))
                    SecondTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == SMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();

                AddMatchWindow mw = new AddMatchWindow(tournament.ID, FirstTeamNextMatchID, SecondTeamNextMatchID, 5);
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

        private void SiMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (TMatchFTeamScore.Content.ToString() == "0" && TMatchSTeamScore.Content.ToString() == "0"
                && FoMatchFTeamScore.Content.ToString() == "0" && FoMatchSTeamScore.Content.ToString() == "0")
                return;

            if (((SiMatchFTeam.Content.ToString() == "" && SiMatchSTeam.Content.ToString() == "")
                && (Convert.ToInt32(TMatchFTeamScore.Content) != Convert.ToInt32(TMatchSTeamScore.Content)
                && Convert.ToInt32(FoMatchFTeamScore.Content) != Convert.ToInt32(FoMatchSTeamScore.Content))) && User.Role == 0)
            {
                int FirstTeamNextMatchID = 0, SecondTeamNextMatchID = 0;
                if (Convert.ToInt32(TMatchFTeamScore.Content) > Convert.ToInt32(TMatchSTeamScore.Content))
                    FirstTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == TMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                else if (Convert.ToInt32(TMatchFTeamScore.Content) < Convert.ToInt32(TMatchSTeamScore.Content))
                    FirstTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == TMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();

                if (Convert.ToInt32(FoMatchFTeamScore.Content) > Convert.ToInt32(FoMatchSTeamScore.Content))
                    SecondTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == FoMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                else if (Convert.ToInt32(FoMatchFTeamScore.Content) < Convert.ToInt32(FoMatchSTeamScore.Content))
                    SecondTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == FoMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();

                AddMatchWindow mw = new AddMatchWindow(tournament.ID, FirstTeamNextMatchID, SecondTeamNextMatchID, 6);
                mw.Show();
                this.Close();
            }
            else if ((SiMatchFTeam.Content.ToString() != "" && SiMatchSTeam.Content.ToString() != ""))
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 6).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
        }

        private void SeMatchMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((FiMatchFTeamScore.Content.ToString() == "0" && FiMatchSTeamScore.Content.ToString() == "0"
                && SiMatchFTeamScore.Content.ToString() == "0" && SiMatchSTeamScore.Content.ToString() == "0"))
                return;

            if (((SeMatchFTeam.Content.ToString() == "" && SeMatchSTeam.Content.ToString() == "")
                && (Convert.ToInt32(FiMatchFTeamScore.Content) != Convert.ToInt32(FiMatchSTeamScore.Content)
                && Convert.ToInt32(SiMatchFTeamScore.Content) != Convert.ToInt32(SiMatchSTeamScore.Content))) && User.Role == 0)
            {
                int FirstTeamNextMatchID = 0, SecondTeamNextMatchID = 0;
                if (Convert.ToInt32(FiMatchFTeamScore.Content) > Convert.ToInt32(FiMatchSTeamScore.Content))
                    FirstTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == FiMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                else if (Convert.ToInt32(FiMatchFTeamScore.Content) < Convert.ToInt32(FiMatchSTeamScore.Content))
                    FirstTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == FiMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();

                if (Convert.ToInt32(SiMatchFTeamScore.Content) > Convert.ToInt32(SiMatchSTeamScore.Content))
                    SecondTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == SiMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                else if (Convert.ToInt32(SiMatchFTeamScore.Content) < Convert.ToInt32(SiMatchSTeamScore.Content))
                    SecondTeamNextMatchID = Connection.db.Teams.Where(item => item.Name == SiMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();

                AddMatchWindow mw = new AddMatchWindow(tournament.ID, FirstTeamNextMatchID, SecondTeamNextMatchID, 7);
                mw.Show();
                this.Close();
            }
            else if (SeMatchFTeam.Content.ToString() != "" && SeMatchSTeam.Content.ToString() != "")
            {
                int MatchID = Connection.db.MatchList.Where(item => item.Tournaments.ID == tournament.ID && item.Match.Number == 7).Select(item => item.IDMatch).FirstOrDefault();
                MatchWindow mw = new MatchWindow(MatchID);
                mw.Show();
                this.Close();
            }
            #endregion
        }

        private void WinnerMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            #region Выявление победителя
            if ((SeMatchFTeamScore.Content.ToString() == "0" && SeMatchSTeamScore.Content.ToString() == "0") || User.Role != 0)
                return;

            if (Winner.Content.ToString() == "" && Convert.ToInt32(SeMatchFTeamScore.Content) != Convert.ToInt32(SeMatchSTeamScore.Content))
            {
                Tournaments tournamentUpdate = Connection.db.Tournaments.Where(item => item.ID == tournament.ID).FirstOrDefault();
                if (Convert.ToInt32(SeMatchFTeamScore.Content) > Convert.ToInt32(SeMatchSTeamScore.Content))
                {
                    tournament.Winner = Connection.db.Teams.Where(item => item.Name == SeMatchFTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                    Winner.Content = SeMatchFTeam.Content.ToString();
                }
                else if (Convert.ToInt32(FiMatchFTeamScore.Content) < Convert.ToInt32(FiMatchSTeamScore.Content))
                {
                    tournament.Winner = Connection.db.Teams.Where(item => item.Name == SeMatchSTeam.Content.ToString()).Select(item => item.ID).FirstOrDefault();
                    Winner.Content = SeMatchSTeam.Content.ToString();
                }
                Connection.db.SaveChanges();
            }
            #endregion
        }
    }
}