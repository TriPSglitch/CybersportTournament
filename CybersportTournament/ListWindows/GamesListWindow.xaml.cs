using ConnectionClass;
using System.Linq;
using System.Windows;
using CybersportTournament.ElementsWindows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для GamesListWindow.xaml
    /// </summary>
    public partial class GamesListWindow : Window
    {
        public GamesListWindow()
        {
            InitializeComponent();

            GamesList.ItemsSource = Connection.db.Games.ToList();
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
                GamesList.ItemsSource = Connection.db.Games.Where(item => item.Name.Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                GamesList.ItemsSource = Connection.db.Games.ToList();
            }
            #endregion
        }

        private void PlayersListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Games)GamesList.SelectedItem).ID;
            GameWindow gw = new GameWindow(id);
            gw.Show();
            this.Hide();
        }
    }
}
