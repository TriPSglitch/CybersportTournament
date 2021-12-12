using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ConnectionClass;

namespace CybersportTournament
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
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выбрать изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Logo.Source = new BitmapImage(new Uri(op.FileName));
                int lastSlash = op.FileName.LastIndexOf("\\") + 1;
                int lastPoint = op.FileName.LastIndexOf(".");
                string logoName = op.FileName.Substring(lastSlash, lastPoint - lastSlash);
                LogoLabel.Content = logoName;
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            Teams team = new Teams()
            {
                Name = Name.Text,
                Logo = BitmapSourceToByteArray((BitmapSource)Logo.Source)
            };
            Connection.db.Teams.Add(team);
            Connection.db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
