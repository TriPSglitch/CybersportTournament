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
    /// Логика взаимодействия для AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        public AddPlayerWindow()
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
                Photo.Source = new BitmapImage(new Uri(op.FileName));
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
            Persons person = new Persons()
            {
                FirstName = FirstName.Text,
                SecondName = SecondName.Text,
                Email = Email.Text,
                Role = 2
            };
            if (MiddleName.Text != "")
            {
                person.MiddleName = MiddleName.Text;
            }
            Connection.db.Persons.Add(person);
            Connection.db.SaveChanges();

            Players player = new Players()
            {
                IDPerson = Connection.db.Persons.Max(x => x.ID),
                Nickname = Nickname.Text,
                Photo = BitmapSourceToByteArray((BitmapSource)Photo.Source)
            };
            Connection.db.Players.Add(player);
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
