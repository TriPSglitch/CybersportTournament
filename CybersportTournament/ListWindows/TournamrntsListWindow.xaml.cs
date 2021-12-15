using ConnectionClass;
using System;
using System.Linq;
using System.Windows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для TournamrntsListWindow.xaml
    /// </summary>
    public partial class TournamrntsListWindow : Window
    {
        public TournamrntsListWindow()
        {
            InitializeComponent();

            var result = (from Tournament in Connection.db.Tournaments
                          join Game in Connection.db.Games on Tournament.IDGame equals Game.ID
                          select new
                          {
                              Name = Tournament.Name,
                              Game = Game.Name,
                              PrizeFund = Tournament.PrizeFund,
                              Logo = Tournament.Logo
                          }).ToList();

            TournamrntsList.ItemsSource = result;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void SearchPreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            #region Поиск
            if (Search.Text == "Поиск")
                Search.Text = "";
        }

        private void SearchLostFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "")
                Search.Text = "Поиск";
        }

        private void SearchTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Search.Text != "" && Search.Text != "Поиск")
            {
                var result = (from Tournament in Connection.db.Tournaments
                              join Game in Connection.db.Games on Tournament.IDGame equals Game.ID
                              where Tournament.Name.Contains(Search.Text) || Game.Name.Contains(Search.Text) || Tournament.PrizeFund.ToString().Contains(Search.Text)
                              select new
                              {
                                  Name = Tournament.Name,
                                  Game = Game.Name,
                                  PrizeFund = Tournament.PrizeFund,
                                  Logo = Tournament.Logo
                              }).ToList();

                TournamrntsList.ItemsSource = result;
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                var result = (from Tournament in Connection.db.Tournaments
                              join Game in Connection.db.Games on Tournament.IDGame equals Game.ID
                              select new
                              {
                                  Name = Tournament.Name,
                                  Game = Game.Name,
                                  PrizeFund = Tournament.PrizeFund,
                                  Logo = Tournament.Logo
                              }).ToList();

                TournamrntsList.ItemsSource = result;
            }
            #endregion
        }
    }
}
