using System.Linq;
using System.Windows;
using ConnectionClass;
using CybersportTournament.ListWindows;

namespace CybersportTournament.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        Teams team;
        public TeamWindow(int id)
        {
            InitializeComponent();
            team = Connection.db.Teams.Where(item => item.ID == id).FirstOrDefault();
            Name.Content = team.Name;
            Logo.Source = ImagesManip.NewImage(team);
            PlayersList.ItemsSource = Connection.db.PlayersList.Where(item => item.IDTeam == id).ToList();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            TeamsListWindow tlw = new TeamsListWindow();
            tlw.Show();
            this.Close();
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