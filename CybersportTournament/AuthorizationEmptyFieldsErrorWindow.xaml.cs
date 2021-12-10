using System.Windows;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationEmptyFieldsErrorWindow.xaml
    /// </summary>
    public partial class AuthorizationEmptyFieldsErrorWindow : Window
    {
        public AuthorizationEmptyFieldsErrorWindow()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
