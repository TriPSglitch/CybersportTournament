using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CybersportTournament
{
    public partial class Style : ResourceDictionary
    {
        private void MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromRgb(120,125,124));
        }

        private void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Border)sender).Background = new SolidColorBrush(Color.FromRgb(79,88,86));
        }
    }
}