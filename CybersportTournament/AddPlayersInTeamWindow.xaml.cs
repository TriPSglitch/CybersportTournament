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
using System.Windows.Shapes;
using ConnectionClass;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для AddPlayersInTeamWindow.xaml
    /// </summary>
    public partial class AddPlayersInTeamWindow : Window
    {
        public AddPlayersInTeamWindow()
        {
            InitializeComponent();
            TeamsBox.ItemsSource = Connection.db.Teams.Select(item => item.Name).ToList();
        }
    }
}
