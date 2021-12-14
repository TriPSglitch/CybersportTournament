﻿using ConnectionClass;
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
            TeamsBox.ItemsSource = Connection.db.Teams.Select(item => item.Name).ToList();
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
                Photo.Source = new BitmapImage(new Uri(op.FileName));
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
            if (FirstName.Text == "" || SecondName.Text == "" || Email.Text == "" || Nickname.Text == "")
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }

            Persons person = new Persons(SecondName.Text, FirstName.Text, Email.Text)
            {
                Role = 2
            };
            if (MiddleName.Text != "")
            {
                person.MiddleName = MiddleName.Text;
            }
            Connection.db.Persons.Add(person);
            Connection.db.SaveChanges();

            Players player = new Players(Connection.db.Persons.Max(x => x.ID), Nickname.Text);

            if (Photo.Source != null)
                player.Photo = BitmapSourceToByteArray((BitmapSource)Photo.Source);

            Connection.db.Players.Add(player);
            Connection.db.SaveChanges();

            if (TeamsBox.SelectedItem != null)
            {
                PlayersList playersList = new PlayersList(Connection.db.Teams.Where(item => item.Name == TeamsBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault(),
                                                          Connection.db.Players.Max(item => item.ID));
                Connection.db.PlayersList.Add(playersList);
                Connection.db.SaveChanges();
            }

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