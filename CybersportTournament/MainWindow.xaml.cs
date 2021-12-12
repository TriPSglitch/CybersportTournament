using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*if (AuthorizationWindow.user.Role == 1)
            {
                AddItem.Visibility = Visibility.Hidden;
            }*/
        }

        private void AddTeamClick(object sender, RoutedEventArgs e)
        {
            AddTeamWindow atw = new AddTeamWindow();
            atw.Show();
            this.Close();
        }

        private void AddPlayerClick(object sender, RoutedEventArgs e)
        {
            AddPlayerWindow apw = new AddPlayerWindow();
            apw.Show();
            this.Close();
        }
    }
}
