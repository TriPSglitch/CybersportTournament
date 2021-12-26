using ConnectionClass;
using System.Linq;
using System.Windows;
using CybersportTournament.ElementsWindows;

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

            PlayersList.ItemsSource = Connection.db.PlayersList.ToList();
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
                PlayersList.ItemsSource = Connection.db.PlayersList.Where(item => (item.Players.Persons.FirstName + " " + item.Players.Persons.SecondName + " "
                                        + item.Players.Persons.MiddleName + " " + item.Players.Nickname + " " + item.Teams.Name).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                Connection.db.PlayersList.ToList();
            }
            #endregion
        }

        private void PlayersListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((PlayersList)PlayersList.SelectedItem).IDPlayer;
            PlayerWindow pw = new PlayerWindow(id);
            pw.Show();
            this.Hide();
        }
    }
}