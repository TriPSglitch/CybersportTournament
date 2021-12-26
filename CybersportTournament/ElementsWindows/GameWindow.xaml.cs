using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using CybersportTournament.ListWindows;
using ConnectionClass;
using System.Linq;
namespace CybersportTournament.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Games game;
        public GameWindow(int id)
        {
            InitializeComponent();
            game = Connection.db.Games.Where(item => item.ID == id).FirstOrDefault();
            Name.Content = game.Name;
            Logo.Source = NewImage(game);
            TournamentsList.ItemsSource = Connection.db.Tournaments.Where(item => item.IDGame == id).ToList();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            GamesListWindow glw = new GamesListWindow();
            glw.Show();
            this.Close();
        }

        private BitmapImage NewImage(Games game)
        {
            #region Декодирование картинки из бд
            MemoryStream ms = new MemoryStream(game.Logo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
            #endregion
        }

        private void PlayersListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Tournaments)TournamentsList.SelectedItem).ID;
            TournamentWindow tw = new TournamentWindow(id);
            tw.Show();
            this.Hide();
        }
    }
}
