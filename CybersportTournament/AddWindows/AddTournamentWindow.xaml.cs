using ConnectionClass;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CybersportTournament.AddWindows
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
            #region Добавление турнира
            if (Name.Text == "" || GamesBox.SelectedItem == null)
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }

            Tournaments tournament = new Tournaments()
            {
                IDGame = Connection.db.Games.Where(item => item.Name == GamesBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault(),
                Name = Name.Text
            };

            if (PrizeFund.Text != "")
            {
                if (PrizeFund.Text.ToString().Count(item => item == '.') > 1)
                {
                    ErrorWindow ew = new ErrorWindow("неверный формат призового фонда");
                    ew.Show();
                    return;
                }
                tournament.PrizeFund = Convert.ToDecimal(PrizeFund.Text);
            }

            if (Logo.Source != null)
                tournament.Logo = ImagesManip.BitmapSourceToByteArray((BitmapSource)Logo.Source);

            Connection.db.Tournaments.Add(tournament);
            Connection.db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }

        private void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            #region Валидация
            if (Regex.IsMatch((((TextBox)sender).Text).ToString(), "[^\\d-.]"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            #endregion
        }
    }
}