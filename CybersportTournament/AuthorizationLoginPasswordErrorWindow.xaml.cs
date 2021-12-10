using System.Windows;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationErrorWindow.xaml
    /// </summary>
    public partial class AuthorizationLoginPasswordErrorWindow : Window
    {
        public AuthorizationLoginPasswordErrorWindow()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
