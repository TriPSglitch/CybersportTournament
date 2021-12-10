using System.Windows;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для RegistrationExistingUserError.xaml
    /// </summary>
    public partial class RegistrationExistingUserErrorWindow : Window
    {
        public RegistrationExistingUserErrorWindow()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
