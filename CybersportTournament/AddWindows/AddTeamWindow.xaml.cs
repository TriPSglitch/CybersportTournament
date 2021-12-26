using ConnectionClass;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CybersportTournament.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public AddTeamWindow()
        {
            InitializeComponent();
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            #region Выбор картинки
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            Logo.Source = image;
            #endregion
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление команды
            Teams team = new Teams()
            {
                Name = Name.Text
            };
            if (Logo.Source != null)
                team.Logo = ImagesManip.BitmapSourceToByteArray((BitmapSource)Logo.Source);

            Connection.db.Teams.Add(team);
            Connection.db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }
    }
}