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
            if (Login.Text == "" || Password.Password == "")
            {
                MessageBox.Show("Ошибка пустые поля");
                return;
            }
            if (Connection.db.Users.Select(item => item.Login + " " + item.Password).Contains(Login.Text + " " + Password.Password))
            {
                MessageBox.Show("Вы авторизованы");
            }
            else
            {
                MessageBox.Show("Ошибка логина/пароля");
                return;
            }
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            rw.Show();
            this.Close();
        }
    }
}