using ConnectionClass;
using System.Linq;
using System.Windows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для TeamsListWindow.xaml
    /// </summary>
    public partial class TeamsListWindow : Window
    {
        public TeamsListWindow()
        {
            InitializeComponent();

            var result = (from ID in Connection.db.PlayersList
                         join Team in Connection.db.Teams on ID.IDTeam equals Team.ID
                         join Player in Connection.db.Players on ID.IDPlayer equals Player.ID
                         select new
                         {
                             Name = Team.Name,
                             Player = Player.Nickname,
                             Logo = Team.Logo
                         }).ToList();

            TeamList.ItemsSource = result;
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
                              where Team.Name.Contains(Search.Text) || Player.Nickname.Contains(Search.Text)
                              select new
                              {
                                  Name = Team.Name,
                                  Player = Player.Nickname,
                                  Logo = Team.Logo
                              }).ToList();

                TeamList.ItemsSource = result;
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                var result = (from ID in Connection.db.PlayersList
                              join Team in Connection.db.Teams on ID.IDTeam equals Team.ID
                              join Player in Connection.db.Players on ID.IDPlayer equals Player.ID
                              select new
                              {
                                  Name = Team.Name,
                                  Player = Player.Nickname,
                                  Logo = Team.Logo
                              }).ToList();

                TeamList.ItemsSource = result;
            }
            #endregion
        }
    }
}
