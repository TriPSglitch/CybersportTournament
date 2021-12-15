using ConnectionClass;
using System.Linq;
using System.Windows;

namespace CybersportTournament.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для MapsListWindow.xaml
    /// </summary>
    public partial class MapsListWindow : Window
    {
        public MapsListWindow()
        {
            InitializeComponent();
            var result = (from Map in Connection.db.MapsGame
                          join Game in Connection.db.Games on Map.IDGame equals Game.ID
                          select new
                          {
                              Name = Map.Name,
                              Game = Game.Name,
                              Image = Map.Image
                          }).ToList();
            MapsList.ItemsSource = result;
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
                var result = (from Map in Connection.db.MapsGame
                              join Game in Connection.db.Games on Map.IDGame equals Game.ID
                              where Game.Name.Contains(Search.Text) || Map.Name.Contains(Search.Text)
                              select new
                              {
                                  Name = Map.Name,
                                  Game = Game.Name,
                                  Image = Map.Image
                              }).ToList();

                MapsList.ItemsSource = result;
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                var result = (from Map in Connection.db.MapsGame
                              join Game in Connection.db.Games on Map.IDGame equals Game.ID
                              select new
                              {
                                  Name = Map.Name,
                                  Game = Game.Name,
                                  Image = Map.Image
                              }).ToList();
                MapsList.ItemsSource = result;
            }
            #endregion
        }
    }
}
