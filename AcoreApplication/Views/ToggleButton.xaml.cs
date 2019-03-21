using AcoreApplication.Model;
using GalaSoft.MvvmLight.Messaging;
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
        SolidColorBrush OffColor = new SolidColorBrush(Color.FromRgb(144, 144, 144));
        SolidColorBrush OnColor = new SolidColorBrush(Color.FromRgb(130, 220, 130));
        public bool Toogled  = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = OffColor;
            Toogled = false;
            Dot.Margin = RightSide;
        }
        
        private void UpdateToggle(Redresseur red)
        {
            if (red.OnOff)
            {
                Back.Fill = OffColor;
                Toogled = false;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = OnColor;
                Toogled = true;
                Dot.Margin = LeftSide;
            }

        }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(Toogled)
            {
                Back.Fill = OffColor;
                Toogled = false;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = OnColor;
                Toogled = true;
                Dot.Margin = LeftSide;
            }
            Redresseur red = (Redresseur)this.DataContext;
            red.OnOff = Toogled;
        }
    }
}
