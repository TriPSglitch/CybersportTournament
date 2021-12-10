using System.Windows;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для RegistrationEmptyFieldsErrorWindow.xaml
    /// </summary>
    public partial class RegistrationEmptyFieldsErrorWindow : Window
    {
        public RegistrationEmptyFieldsErrorWindow()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
