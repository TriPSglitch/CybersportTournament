using CybersportTournament.ListWindows;
using System.Linq;
using System.Windows;
using ConnectionClass;
using System.Windows.Media.Imaging;
using System.IO;

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
            Photo.Source = NewImage(player);
            Team.Content = team.Name;
            TeamLogo.Source = NewImage(team);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            PlayersListWindow plw = new PlayersListWindow();
            plw.Show();
            this.Close();
        }

        private BitmapImage NewImage(Players player)
        {
            #region Декодирование картинки из бд
            MemoryStream ms = new MemoryStream(player.Photo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        private BitmapImage NewImage(Teams team)
        {
            MemoryStream ms = new MemoryStream(team.Logo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
            #endregion
        }
    }
}
