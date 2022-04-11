using ConnectionClass;
using CybersportTournament.ElementsWindows;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CybersportTournament.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddRoundWindow.xaml
    /// </summary>
    public partial class AddRoundWindow : Window
    {
        Match match;

        public AddRoundWindow()
        {
            InitializeComponent();
        }

        public AddRoundWindow(int IDMatch)
        {
            InitializeComponent();
            match = Connection.db.Match.Where(item => item.ID == IDMatch).FirstOrDefault();
            Match.Content = match.Name;
            int numberRound;
            if (Connection.db.RoundsList.Where(item => item.IDMatch == match.ID).FirstOrDefault() == null)
                numberRound = 1;
            else
                numberRound = Connection.db.RoundsList.Where(item => item.IDMatch == match.ID).Max(item => item.Rounds.Name) + 1;
            Round.Content = numberRound;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление раунда

            if (Period.Text.ToString() != "" && !Valid(Period.Text.ToString()))
            {
                ErrorWindow ew = new ErrorWindow("неверный формат длительности раунда");
                ew.Show();
                return;
            }
            if (Result.Text.ToString() != "" && !Valid(Result.Text.ToString()))
            {
                ErrorWindow ew = new ErrorWindow("неверный формат результата раунда");
                ew.Show();
                return;
            }


            Rounds round = new Rounds()
            {
                Name = Convert.ToInt32(Round.Content)
            };
            if (Period.Text.ToString() != "")
            {
                int index = Period.Text.IndexOf(":");
                round.Period = new TimeSpan(Convert.ToInt32(Period.Text.Substring(0, index)), Convert.ToInt32(Period.Text.Substring(index + 1, Period.Text.Length - index - 1)), 0);
                match.Period = MatchPeriod(Period.Text.ToString(), (TimeSpan)match.Period);
            }
            round.Result = "0:0";
            Connection.db.Rounds.Add(round);
            Connection.db.SaveChanges();
            if (Result.Text.ToString() != "")
            {
                if (MatchResult(Result.Text.ToString(), match.Result.ToString()) != "ошибка")
                    match.Result = MatchResult(Result.Text.ToString(), match.Result.ToString());
                else
                {
                    ErrorWindow ew = new ErrorWindow("счёт за раунд не может быть равным");
                    ew.Show();
                    return;
                }

                round.Result = Result.Text;
            }
            Connection.db.SaveChanges();


            RoundsList roundList = new RoundsList()
            {
                IDRound = round.ID,
                IDMatch = match.ID
            };
            Connection.db.RoundsList.Add(roundList);
            Connection.db.SaveChanges();

            MatchWindow mw = new MatchWindow(match.ID);
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

        private void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            #region Валидация
            if (Regex.IsMatch((((TextBox)sender).Text).ToString(), "[^\\d-:]"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        private bool Valid(string str)
        {
            if (str.Count(item => item == ':') != 1)
                return false;
            else
                return true;
            #endregion
        }

        public TimeSpan MatchPeriod(string roundPeriod, TimeSpan matchPeriod)
        {
            #region Авторасчёт длительности матча
            int index = roundPeriod.IndexOf(":");
            TimeSpan rp = new TimeSpan(Convert.ToInt32(roundPeriod.Substring(0, index)), 
                Convert.ToInt32(roundPeriod.Substring(index + 1, roundPeriod.Length - index - 1)), 0);
            return matchPeriod + rp;
            #endregion
        }

        public string MatchResult(string score, string matchScore)
        {
            #region Авто изменение счёта матча
            int length = score.IndexOf(':');
            int firstTeamRoundScore = Convert.ToInt32(score.Substring(0, length));
            int secondTeamRoundScore = Convert.ToInt32(score.Substring(length + 1, score.Length - length - 1));
            length = matchScore.IndexOf(':');
            int firstTeamMatchScore = Convert.ToInt32(matchScore.Substring(0, length));
            int secondTeamMatchScore = Convert.ToInt32(matchScore.Substring(length + 1, matchScore.Length - length - 1));

            if (firstTeamRoundScore == secondTeamRoundScore)
            {
                ErrorWindow ew = new ErrorWindow("счёт не может быть одинаковым");
                ew.Show();
                return "ошибка";
            }
            else if (firstTeamRoundScore > secondTeamRoundScore)
            {
                firstTeamMatchScore += 1;
            }
            else
            {
                secondTeamMatchScore += 1;
            }
            return firstTeamMatchScore.ToString() + ":" + secondTeamMatchScore.ToString();
            #endregion
        }
    }
}