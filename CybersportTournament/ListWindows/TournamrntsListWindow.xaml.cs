using ConnectionClass;
using CybersportTournament.ElementsWindows;
using System.Linq;
using System.Windows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для TournamrntsListWindow.xaml
    /// </summary>
    public partial class TournamentsListWindow : Window
    {
        public TournamentsListWindow()
        {
            InitializeComponent();

            TournamrntsList.ItemsSource = Connection.db.Tournaments.ToList();
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
                TournamrntsList.ItemsSource = Connection.db.Tournaments.Where(item => (item.Name + " " + item.Games.Name + " " + item.PrizeFund).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                TournamrntsList.ItemsSource = Connection.db.Tournaments.ToList();
            }
            #endregion
        }

        private void TournamentsListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Tournaments)TournamrntsList.SelectedItem).ID;
            TournamentWindow tw = new TournamentWindow(id);
            tw.Show();
            this.Hide();
        }
    }
}