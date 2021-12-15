using ConnectionClass;
using System.Linq;
using System.Windows;

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

            var result = (from Game in Connection.db.Games select new { Name = Game.Name, Logo = Game.Logo }).ToList();
            GamesList.ItemsSource = result;
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
                var result = (from Game in Connection.db.Games where Game.Name.Contains(Search.Text) select new { Name = Game.Name, Logo = Game.Logo }).ToList();

                GamesList.ItemsSource = result;
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                var result = (from Game in Connection.db.Games select new { Name = Game.Name, Logo = Game.Logo }).ToList();

                GamesList.ItemsSource = result;
            }
            #endregion
        }
    }
}
