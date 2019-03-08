using AcoreApplication.Model;
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

namespace AcoreApplication.Views
{
    /// <summary>
    /// Logique d'interaction pour ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(-15, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -15, 0);
        SolidColorBrush OffColor = new SolidColorBrush(Color.FromArgb(144, 144, 144, 100));
        SolidColorBrush OnColor = new SolidColorBrush(Color.FromArgb(53, 255, 53, 100));
        public bool Toogled  = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = OffColor;
            Toogled = false;
            Dot.Margin = LeftSide;
        }


        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(Toogled)
            {
                Back.Fill = OffColor;
                Toogled = false;
                Dot.Margin = LeftSide;
            }
            else
            {
                Back.Fill = OnColor;
                Toogled = true;
                Dot.Margin = RightSide;
            }
            Redresseur red = (Redresseur)this.DataContext;
            red.OnOff = Toogled;
        }
    }
}
