using ConnectionClass;
using System.Linq;
using System.Windows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для PlayersListWindow.xaml
    /// </summary>
    public partial class PlayersListWindow : Window
    {
        public PlayersListWindow()
        {
            InitializeComponent();

            var result = (from ID in Connection.db.PlayersList
                          join Team in Connection.db.Teams on ID.IDTeam equals Team.ID
                          join Player in Connection.db.Players on ID.IDPlayer equals Player.ID
                          //join Person in Connection.db.Persons on Connection.db.Players.Select(item => item.IDPerson).FirstOrDefault() equals Person.ID
                          from Players in Connection.db.Players
                          join Person in Connection.db.Persons on Player.IDPerson equals Person.ID
                          join PlayerID in Connection.db.PlayersList on Player.ID equals PlayerID.IDPlayer
                          select new
                          {
                              FirstName = Person.FirstName,
                              SecondName = Person.SecondName,
                              MiddleName = Person.MiddleName,
                              Nickname = Player.Nickname,
                              Team = Team.Name,
                              Photo = Player.Photo
                          }).ToList();

            PlayersList.ItemsSource = result;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void SearchPreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            #region Поиск
            if (Search.Text == "Поиск")
                Search.Text = "";
        }

        private void SearchLostFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "")
                Search.Text = "Поиск";
        }

        private void SearchTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Search.Text != "" && Search.Text != "Поиск")
            {
                var result = (from ID in Connection.db.PlayersList
                              join Team in Connection.db.Teams on ID.IDTeam equals Team.ID
                              join Player in Connection.db.Players on ID.IDPlayer equals Player.ID
                              from Players in Connection.db.Players
                              join Person in Connection.db.Persons on Player.IDPerson equals Person.ID
                              join PlayerID in Connection.db.PlayersList on Player.ID equals PlayerID.IDPlayer
                              where Person.FirstName.Contains(Search.Text) || Person.SecondName.Contains(Search.Text) || Person.MiddleName.Contains(Search.Text)
                              || Player.Nickname.Contains(Search.Text) || Team.Name.Contains(Search.Text)
                              select new
                              {
                                  FirstName = Person.FirstName,
                                  SecondName = Person.SecondName,
                                  MiddleName = Person.MiddleName,
                                  Nickname = Player.Nickname,
                                  Team = Team.Name,
                                  Photo = Player.Photo
                              }).ToList();

                PlayersList.ItemsSource = result;
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                var result = (from ID in Connection.db.PlayersList
                              join Team in Connection.db.Teams on ID.IDTeam equals Team.ID
                              join Player in Connection.db.Players on ID.IDPlayer equals Player.ID
                              from Players in Connection.db.Players
                              join Person in Connection.db.Persons on Player.IDPerson equals Person.ID
                              join PlayerID in Connection.db.PlayersList on Player.ID equals PlayerID.IDPlayer
                              select new
                              {
                                  FirstName = Person.FirstName,
                                  SecondName = Person.SecondName,
                                  MiddleName = Person.MiddleName,
                                  Nickname = Player.Nickname,
                                  Team = Team.Name,
                                  Photo = Player.Photo
                              }).ToList();

                PlayersList.ItemsSource = result;
            }
            #endregion
        }
    }
}
