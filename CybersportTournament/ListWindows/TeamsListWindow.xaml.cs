using ConnectionClass;
using System.Linq;
using System.Windows;
using CybersportTournament.ElementsWindows;

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

            TeamList.ItemsSource = Connection.db.Teams.ToList();
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
                TeamList.ItemsSource = Connection.db.Teams.Where(item => (item.Name).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                TeamList.ItemsSource = Connection.db.Teams.ToList();
            }
            #endregion
        }

        private void TeamListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Teams)TeamList.SelectedItem).ID;
            TeamWindow tw = new TeamWindow(id);
            tw.Show();
            this.Hide();
        }
    }
}
