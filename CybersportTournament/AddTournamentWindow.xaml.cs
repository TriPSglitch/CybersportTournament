using ConnectionClass;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для AddTournamentWindow.xaml
    /// </summary>
    public partial class AddTournamentWindow : Window
    {
        public AddTournamentWindow()
        {
            InitializeComponent();
            GamesBox.ItemsSource = Connection.db.Games.Select(item => item.Name).ToList();
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            #region Выбор картинки
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выбрать изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Logo.Source = new BitmapImage(new Uri(op.FileName));
            }
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
            #region Добавление игрока
            if (Name.Text == "" || GamesBox.SelectedItem == null)
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }

            Tournaments tournament = new Tournaments(Connection.db.Games.Where(item => item.Name == GamesBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault(), Name.Text);

            if (PrizeFund.Text != "")
            {
                tournament.PrizeFund = Convert.ToDecimal(PrizeFund.Text);
            }

            if (Logo.Source != null)
                tournament.Logo = BitmapSourceToByteArray((BitmapSource)Logo.Source);

            Connection.db.Tournaments.Add(tournament);
            Connection.db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }

        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            #region Кодирование картинки
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
            #endregion
        }
    }
}
