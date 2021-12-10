using System.Windows;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для RegistrationExistingUserError.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string error)
        {
            InitializeComponent();
            Error.Content = "Ошибка: " + error;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
