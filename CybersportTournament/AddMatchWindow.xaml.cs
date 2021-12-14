using ConnectionClass;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для AddMatchWindow.xaml
    /// </summary>
    public partial class AddMatchWindow : Window
    {
        static int TeamOneID, TeamTwoID;
        public AddMatchWindow()
        {
            InitializeComponent();
            TeamOneBox.ItemsSource = Connection.db.Teams.Select(item => item.Name).ToList();
            TeamTwoBox.ItemsSource = Connection.db.Teams.Select(item => item.Name).ToList();
            TournamentBox.ItemsSource = Connection.db.Tournaments.Select(item => item.Name).ToList();
        }

        private void TeamOneBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region Выбор команд
            TeamOneID = Connection.db.Teams.Where(item => item.Name == TeamOneBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault();

            TeamTwoBox.ItemsSource = Connection.db.Teams.Where(item => item.ID != TeamOneID).Select(item => item.Name).ToList();

            if (Connection.db.Teams.Where(item => item.ID == TeamOneID).Select(item => item.Logo).SingleOrDefault() == null)
            {
                TeamOneLogo.Source = null;
            }
            else
            {
                TeamOneLogo.Source = (NewImage(TeamOneID));
            }
        }

        private void TeamTwoBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamTwoID = Connection.db.Teams.Where(item => item.Name == TeamTwoBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault();

            TeamOneBox.ItemsSource = Connection.db.Teams.Where(item => item.ID != TeamTwoID).Select(item => item.Name).ToList();

            if (Connection.db.Teams.Where(item => item.ID == TeamTwoID).Select(item => item.Logo).SingleOrDefault() == null)
            {
                TeamTwoLogo.Source = null;
            }
            else
            {
                TeamTwoLogo.Source = (NewImage(TeamTwoID));
            }
            #endregion
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление матча
            if (TournamentBox.SelectedItem == null || TeamOneBox.SelectedItem == null || TeamTwoBox.SelectedItem == null || Date.SelectedDate == null)
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }

            Match match = new Match((DateTime)Date.SelectedDate);
            Connection.db.Match.Add(match);
            Connection.db.SaveChanges();

            int MatchID = Connection.db.Match.Max(item => item.ID);
            int TournamentID = Connection.db.Tournaments.Where(item => item.Name == TournamentBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault();
            MatchList matchList = new MatchList(MatchID, TournamentID);
            Connection.db.MatchList.Add(matchList);
            Connection.db.SaveChanges();

            int NumberTeamList;
            if (Connection.db.TeamsList.Count() == 0)
            {
                NumberTeamList = 1;
            }
            else
            {
                NumberTeamList = Convert.ToInt32(Connection.db.TeamsList.Max(item => item.NumberTeamList).ToString()) + 1;
            }

            TeamsList teamsListOne = new TeamsList(TournamentID, Connection.db.Teams.Where(item => item.Name == TeamOneBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault(), NumberTeamList);
            TeamsList teamsListTwo = new TeamsList(TournamentID, Connection.db.Teams.Where(item => item.Name == TeamTwoBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault(), NumberTeamList);

            Connection.db.TeamsList.Add(teamsListOne);
            Connection.db.TeamsList.Add(teamsListTwo);

            Connection.db.SaveChanges();

            string Name = Connection.db.Tournaments.Where(item => item.ID == TournamentID).Select(item => item.Name).FirstOrDefault();
            int FirstTeamID = Connection.db.TeamsList.Where(item => item.NumberTeamList == NumberTeamList).Select(item => item.IDTeam).First();
            int SecondTeamID = Connection.db.TeamsList.Where(item => item.NumberTeamList == NumberTeamList && item.IDTeam != FirstTeamID).Select(item => item.IDTeam).First();

            Name += " " + Connection.db.Teams.Where(item => item.ID == FirstTeamID).Select(item => item.Name).FirstOrDefault();
            Name += " " + Connection.db.Teams.Where(item => item.ID == SecondTeamID).Select(item => item.Name).FirstOrDefault();
            Name += " " + Connection.db.Match.Where(item => item.ID == MatchID).Select(item => item.Time).FirstOrDefault();

            var matchUpdate = Connection.db.Match.Where(item => item.ID == MatchID).FirstOrDefault();
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

        private BitmapImage NewImage(int ID)
        {
            #region Декодирование картинки из бд
            MemoryStream ms = new MemoryStream(Connection.db.Teams.Where(item => item.ID == ID).Select(item => item.Logo).SingleOrDefault());
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
            #endregion
        }
    }
}