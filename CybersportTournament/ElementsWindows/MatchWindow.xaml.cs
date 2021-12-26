using ConnectionClass;
using CybersportTournament.AddWindows;
using System.Linq;
using System.Windows;

namespace CybersportTournament.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для MatchWindow.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        Match match;
        public MatchWindow(int id)
        {
            InitializeComponent();
            match = Connection.db.Match.Where(item => item.ID == id).FirstOrDefault();
            Name.Content = match.Name;
            Time.Content = match.Time;
            Period.Content = match.Period;
            Result.Content = match.Result;
            int numberList = match.Number;
            int idTournament = Connection.db.MatchList.Where(item => item.IDMatch == id).Select(item => item.IDTournament).FirstOrDefault();
            int firstTeamID = Connection.db.TeamsList.Where(item => item.IDTournament == idTournament && item.NumberTeamList == numberList).Select(item => item.IDTeam).FirstOrDefault();
            int secondTeamID = Connection.db.TeamsList.Where(item => item.IDTournament == idTournament && item.NumberTeamList == numberList && item.IDTeam != firstTeamID).Select(item => item.IDTeam).FirstOrDefault();
            Team1.Content = Connection.db.Teams.Where(item => item.ID == firstTeamID).Select(item => item.Name).FirstOrDefault();
            Team2.Content = Connection.db.Teams.Where(item => item.ID == secondTeamID).Select(item => item.Name).FirstOrDefault();
            RoundsList.ItemsSource = Connection.db.RoundsList.Where(item => item.IDMatch == id).ToList();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void RoundsListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((Rounds)RoundsList.SelectedItem).ID;
            MatchWindow mw = new MatchWindow(id);
            mw.Show();
            this.Hide();
        }

        private void AddRoundClick(object sender, RoutedEventArgs e)
        {
            AddRoundWindow arw = new AddRoundWindow(match.ID);
            arw.Show();
            this.Close();
        }
    }
}
