using ConnectionClass;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CybersportTournament.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        public AddPlayerWindow()
        {
            InitializeComponent();
            TeamsBox.ItemsSource = Connection.db.Teams.Select(item => item.Name).ToList();
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            #region Выбор картинки
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            Photo.Source = image;
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
            if (FirstName.Text == "" || SecondName.Text == "" || Email.Text == "" || Nickname.Text == "")
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }
            if(!IsValidEmail(Email.Text))
            {
                ErrorWindow ew = new ErrorWindow("неверный формат почты");
                ew.Show();
                return;
            }

            Persons person = new Persons()
            {
                SecondName = SecondName.Text,
                FirstName = FirstName.Text,
                Email = Email.Text,
                IDRole = 2
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
                Nickname = Nickname.Text
            };

            if (Photo.Source != null)
                player.Photo = ImagesManip.BitmapSourceToByteArray((BitmapSource)Photo.Source);

            Connection.db.Players.Add(player);
            Connection.db.SaveChanges();

            if (TeamsBox.SelectedItem != null)
            {
                PlayersList playersList = new PlayersList()
                {
                    IDTeam = Connection.db.Teams.Where(item => item.Name == TeamsBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault(),
                    IDPlayer = Connection.db.Players.Max(item => item.ID)
                };
                Connection.db.PlayersList.Add(playersList);
                Connection.db.SaveChanges();
            }

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }

        private bool IsValidEmail(string email)
        {
            #region Валидация
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (Regex.IsMatch(email, regex))
                return true;
            else
                return false;
        }

        private void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Regex.IsMatch((((TextBox)sender).Text).ToString(), "[^А-я-:]"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            #endregion
        }
    }
}