using ConnectionClass;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CybersportTournament.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddGameWindow.xaml
    /// </summary>
    public partial class AddGameWindow : Window
    {
        public AddGameWindow()
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
            #region Добавление игры
            if (Name.Text == "")
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }
            Games game = new Games()
            {
                Name = Name.Text
            };

            if (Link.Text.ToString() != "")
            {
                if (!Uri.TryCreate(Link.Text.ToString(), UriKind.RelativeOrAbsolute, out var test))
                {
                    ErrorWindow ew = new ErrorWindow("неверный формат ссылки");
                    ew.Show();
                    return;
                }
                game.Link = Link.Text;
            }

            if (Logo.Source != null)
                game.Logo = ImagesManip.BitmapSourceToByteArray((BitmapSource)Logo.Source);

            Connection.db.Games.Add(game);
            Connection.db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }
    }
}