using System.Windows;
using System.Linq;
using ConnectionClass;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            #region Валидация
            if (Login.Text == "" || Password.Password == "" || FirstName.Text == "" || SecondName.Text == "" || Email.Text == "")
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }
            if (Connection.db.Users.Select(item => item.Login).Contains(Login.Text))
            {
                ErrorWindow ew = new ErrorWindow("такой пользователь уже существует");
                ew.Show();
                return;
            }
            if (!IsValidEmail(Email.Text))
            {
                ErrorWindow ew = new ErrorWindow("неверный формат почты");
                ew.Show();
                return;
            }
            #endregion


            #region Добавление пользователя
            Persons person = new Persons()
            {
                SecondName = SecondName.Text,
                FirstName = FirstName.Text,
                Email = Email.Text,
                Role = 1
            };
            if (MiddleName.Text != "")
            {
                person.MiddleName = MiddleName.Text;
            }

            Connection.db.Persons.Add(person);
            Connection.db.SaveChanges();

            Users user = new Users()
            {
                IDPerson = Connection.db.Persons.Max(x => x.ID),
                Login = Login.Text,
                Password = Encrypt.Hash(Password.Password)
            };

            Connection.db.Users.Add(user);
            Connection.db.SaveChanges();
            #endregion


            #region Результат и переход на окно авторизации
            RegitrationConfirmedWindow rcw = new RegitrationConfirmedWindow();
            rcw.Show();

            AuthorizationWindow aw = new AuthorizationWindow();
            aw.Show();
            this.Close();
            #endregion
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            #region Назад
            AuthorizationWindow aw = new AuthorizationWindow();
            aw.Show();
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