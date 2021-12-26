using ConnectionClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CybersportTournament.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddMatchWindow.xaml
    /// </summary>
    public partial class AddMatchWindow : Window
    {
        static int TeamOneID, TeamTwoID;
        List<int> teams;
        List<int> teamsOneID;
        List<int> teamsTwoID;
        int NumberList;
        public AddMatchWindow(int TournamentID, int NumberList)
        {
            InitializeComponent();
            this.NumberList = NumberList;
            teams = Connection.db.TeamsList.Where(item => item.IDTournament == TournamentID).Select(item => item.IDTeam).ToList();
            teamsOneID = Connection.db.Teams.Where(item => !teams.Contains(item.ID)).Select(item => item.ID).ToList();
            teamsTwoID = Connection.db.Teams.Where(item => !teams.Contains(item.ID)).Select(item => item.ID).ToList();

            TeamOneBox.ItemsSource = Connection.db.Teams.Where(item => teamsOneID.Contains(item.ID)).Select(item => item.Name).ToList();
            TeamTwoBox.ItemsSource = Connection.db.Teams.Where(item => teamsTwoID.Contains(item.ID)).Select(item => item.Name).ToList();
            TournamentBox.ItemsSource = Connection.db.Tournaments.Where(item => item.ID == TournamentID).Select(item => item.Name).ToList();
            TournamentBox.SelectedIndex = 0;
        }
        public AddMatchWindow(int TournamentID, int FirstTeamNextMatchID, int SecondTeamNextMatchID, int NumberList)
        {
            InitializeComponent();
            this.NumberList = NumberList;
            TeamOneBox.Visibility = Visibility.Hidden;
            TeamTwoBox.Visibility = Visibility.Hidden;
            TeamOneLabel.Visibility = Visibility.Visible;
            TeamTwoLabel.Visibility = Visibility.Visible;

            Teams teamOne = Connection.db.Teams.Where(item => item.ID == FirstTeamNextMatchID).FirstOrDefault();
            Teams teamTwo = Connection.db.Teams.Where(item => item.ID == SecondTeamNextMatchID).FirstOrDefault();

            TeamOneLogo.Source = (ImagesManip.NewImage(teamOne));
            TeamTwoLogo.Source = (ImagesManip.NewImage(teamTwo));

            TeamOneLabel.Content = Connection.db.Teams.Where(item => item.ID == FirstTeamNextMatchID).Select(item => item.Name).FirstOrDefault();
            TeamTwoLabel.Content = Connection.db.Teams.Where(item => item.ID == SecondTeamNextMatchID).Select(item => item.Name).FirstOrDefault();

            TournamentBox.ItemsSource = Connection.db.Tournaments.Where(item => item.ID == TournamentID).Select(item => item.Name).ToList();
            TournamentBox.SelectedIndex = 0;
        }

        private void TeamOneBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region Выбор команд
            TeamOneID = Connection.db.Teams.Where(item => item.Name == TeamOneBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault();
            Teams teamOne = Connection.db.Teams.Where(item => item.ID == TeamOneID).FirstOrDefault();

            TeamTwoBox.ItemsSource = Connection.db.Teams.Where(item => item.ID != TeamOneID && teamsTwoID.Contains(item.ID)).Select(item => item.Name).ToList();

            if (Connection.db.Teams.Where(item => item.ID == TeamOneID).Select(item => item.Logo).SingleOrDefault() == null)
            {
                TeamOneLogo.Source = null;
            }
            else
            {
                TeamOneLogo.Source = (ImagesManip.NewImage(teamOne));
            }
        }

        private void TeamTwoBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamTwoID = Connection.db.Teams.Where(item => item.Name == TeamTwoBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault();
            Teams teamTwo = Connection.db.Teams.Where(item => item.ID == TeamTwoID).FirstOrDefault();

            TeamOneBox.ItemsSource = Connection.db.Teams.Where(item => item.ID != TeamTwoID && teamsOneID.Contains(item.ID)).Select(item => item.Name).ToList();

            if (Connection.db.Teams.Where(item => item.ID == TeamTwoID).Select(item => item.Logo).SingleOrDefault() == null)
            {
                TeamTwoLogo.Source = null;
            }
            else
            {
                TeamTwoLogo.Source = (ImagesManip.NewImage(teamTwo));
            }
            #endregion
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление матча
            if (TeamOneLabel.Visibility == Visibility.Hidden || TeamTwoLabel.Visibility == Visibility.Hidden)
            {
                if (TeamOneBox.SelectedItem == null || TeamTwoBox.SelectedItem == null)
                {
                    ErrorWindow ew = new ErrorWindow("пустые поля");
                    ew.Show();
                    return;
                }
            }
            if (TournamentBox.SelectedItem == null || Date.SelectedDate == null)
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }

            Match match = new Match()
            {
                Time = (DateTime)Date.SelectedDate,
                Period = new TimeSpan(0, 0, 0)
            };
            match.Result = "0:0";
            Connection.db.Match.Add(match);
            Connection.db.SaveChanges();

            int MatchID = Connection.db.Match.Max(item => item.ID);
            int TournamentID = Connection.db.Tournaments.Where(item => item.Name == TournamentBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault();
            MatchList matchList = new MatchList()
            {
                IDMatch = MatchID,
                IDTournament = TournamentID
            };
            Connection.db.MatchList.Add(matchList);
            Connection.db.SaveChanges();

            string teamOneName;
            if (TeamOneBox.Visibility != Visibility.Hidden)
                teamOneName = TeamOneBox.SelectedItem.ToString();
            else
                teamOneName = TeamOneLabel.Content.ToString();

            string teamTwoName;
            if (TeamTwoBox.Visibility != Visibility.Hidden)
                teamTwoName = TeamTwoBox.SelectedItem.ToString();
            else
                teamTwoName = TeamTwoLabel.Content.ToString();

            TeamsList teamsListOne = new TeamsList()
            {
                IDTournament = TournamentID,
                IDTeam = Connection.db.Teams.Where(item => item.Name == teamOneName).Select(item => item.ID).FirstOrDefault(),
                NumberTeamList = NumberList
            };
            TeamsList teamsListTwo = new TeamsList()
            {
                IDTournament = TournamentID,
                IDTeam = Connection.db.Teams.Where(item => item.Name == teamTwoName).Select(item => item.ID).FirstOrDefault(),
                NumberTeamList = NumberList
            };

            Connection.db.TeamsList.Add(teamsListOne);
            Connection.db.TeamsList.Add(teamsListTwo);

            Connection.db.SaveChanges();

            string Name = Connection.db.Tournaments.Where(item => item.ID == TournamentID).Select(item => item.Name).FirstOrDefault();
            int FirstTeamID = Connection.db.TeamsList.Where(item => item.NumberTeamList == NumberList).Select(item => item.IDTeam).First();
            int SecondTeamID = Connection.db.TeamsList.Where(item => item.NumberTeamList == NumberList && item.IDTeam != FirstTeamID).Select(item => item.IDTeam).First();

            Name += " " + Connection.db.Teams.Where(item => item.ID == FirstTeamID).Select(item => item.Name).FirstOrDefault();
            Name += " " + Connection.db.Teams.Where(item => item.ID == SecondTeamID).Select(item => item.Name).FirstOrDefault();
            Name += " " + Connection.db.Match.Where(item => item.ID == MatchID).Select(item => item.Time).FirstOrDefault();

            var matchUpdate = Connection.db.Match.Where(item => item.ID == MatchID).FirstOrDefault();
            matchUpdate.Number = NumberList;
            matchUpdate.Name = Name;
            Connection.db.SaveChanges();


            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}