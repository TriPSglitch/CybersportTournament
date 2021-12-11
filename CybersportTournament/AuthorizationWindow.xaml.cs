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
            if (Login.Text == "" || Password.Password == "")
            {
                ErrorWindow ew = new ErrorWindow("пустые поля");
                ew.Show();
                return;
            }
            if (Connection.db.Users.Select(item => item.Login + " " + item.Password).Contains(Login.Text + " " + Password.Password))
            {
                Window1 w1 = new Window1();
                w1.Show();
                this.Close();
            }
            else
            {
                ErrorWindow ew = new ErrorWindow("неверный логин/пароль");
                ew.Show();
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