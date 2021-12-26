using CybersportTournament.ListWindows;
using System.Linq;
using System.Windows;
using ConnectionClass;

namespace CybersportTournament.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        Players player;
        Persons person;
        Teams team;
        public PlayerWindow(int playerID)
        {
            InitializeComponent();
            player = Connection.db.Players.Where(item => item.ID == playerID).FirstOrDefault();
            person = Connection.db.Persons.Where(item => item.ID == player.IDPerson).FirstOrDefault();
            team = Connection.db.PlayersList.Where(item => item.IDPlayer == player.ID).Select(item => item.Teams).FirstOrDefault();

            SecondName.Content = person.SecondName;
            FirstName.Content = person.FirstName;
            MiddleName.Content = person.MiddleName;
            Nickname.Content = player.Nickname;
            Photo.Source = ImagesManip.NewImage(player);
            Team.Content = team.Name;
            TeamLogo.Source = ImagesManip.NewImage(team);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            PlayersListWindow plw = new PlayersListWindow();
            plw.Show();
            this.Close();
        }
    }
}