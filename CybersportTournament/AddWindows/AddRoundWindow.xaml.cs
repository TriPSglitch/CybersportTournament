using ConnectionClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CybersportTournament.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddRoundWindow.xaml
    /// </summary>
    public partial class AddRoundWindow : Window
    {
        List<char> numbers = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public AddRoundWindow()
        {
            InitializeComponent();
            MatchesBox.ItemsSource = Connection.db.Match.Select(item => item.Name).ToList();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление раунд
            if (MatchesBox.SelectedItem == null || RoundBox == null)
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }

            Rounds round = new Rounds()
            {
                Name = Convert.ToInt32(RoundBox.Text)
            };
            Connection.db.Rounds.Add(round);
            Connection.db.SaveChanges();


            RoundsList roundList = new RoundsList()
            {
                IDRound = Connection.db.Rounds.Max(item => item.ID),
                IDMatch = Connection.db.Match.Where(item => item.Name == MatchesBox.SelectedItem.ToString()).Select(item => item.ID).FirstOrDefault()
            };
            Connection.db.RoundsList.Add(roundList);
            Connection.db.SaveChanges();


            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            #endregion
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void RoundBoxTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            #region Валидация поля номера раунда
            if (RoundBox.Text != "" && !numbers.Contains(RoundBox.Text[RoundBox.Text.Length - 1]))
            {
                RoundBox.CaretIndex = RoundBox.Text.Length - 2;
                RoundBox.Text = RoundBox.Text.Remove(RoundBox.Text.Length - 1, 1);
            }
            else
            {
                RoundBox.CaretIndex = RoundBox.Text.Length;
            }
            #endregion
        }
    }
}
