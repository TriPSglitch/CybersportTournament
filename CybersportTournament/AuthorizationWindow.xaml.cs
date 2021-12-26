using System.Linq;
using System.Windows;
using ConnectionClass;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationClick(object sender, RoutedEventArgs e)
        {
            #region Авторизация
            if (Authorization(Login.Text.ToString(), Password.Password.ToString()))
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        public bool Authorization(string login, string password)
        {
            if (Login.Text == "" || Password.Password == "")
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return false;
            }
            if (Connection.db.Users.Select(item => item.Login + " " + item.Password).Contains(Login.Text + " " + Encrypt.Hash(Password.Password)))
            {
                int personID = Connection.db.Users.Where(users => users.Login == Login.Text).Select(users => users.IDPerson).FirstOrDefault();
                int Role = Connection.db.Persons.Where(users => users.ID == personID).Select(users => users.IDRole).FirstOrDefault();
                User.Role = Role;
                User.IDPerson = personID;
                return true;
            }
            else
            {
                ErrorWindow ew = new ErrorWindow("неверный логин/пароль");
                ew.Show();
                return false;
            }
            #endregion
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            #region Переход на окно регистрации
            RegistrationWindow rw = new RegistrationWindow();
            rw.Show();
            this.Close();
            #endregion
        }
    }
}