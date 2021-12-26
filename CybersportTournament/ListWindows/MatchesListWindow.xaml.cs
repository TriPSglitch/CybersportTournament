using ConnectionClass;
using System.Linq;
using System.Windows;
using CybersportTournament.ElementsWindows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для MatchesListWindow.xaml
    /// </summary>
    public partial class MatchesListWindow : Window
    {
        public MatchesListWindow()
        {
            InitializeComponent();

            MatchesList.ItemsSource = Connection.db.Match.ToList();
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
                MatchesList.ItemsSource = Connection.db.Match.Where(item => (item.Name + " " + item.Time + " " + item.Period + " " + item.Result).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                MatchesList.ItemsSource = Connection.db.Match.ToList();
            }
            #endregion
        }

        private void MatchesListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Match)MatchesList.SelectedItem).ID;
            MatchWindow mw = new MatchWindow(id);
            mw.Show();
            this.Hide();
        }
    }
}