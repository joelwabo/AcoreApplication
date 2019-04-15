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
using System.Windows.Threading;

namespace AcoreApplication.Views
{
    /// <summary>
    /// Logique d'interaction pour MachineControle.xaml
    /// </summary>
    public partial class MachineControle : UserControl
    {
        public MachineControle()
        {
            InitializeComponent();

            DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += update;
            timer.Start();
        }

        private void update(object sender, EventArgs e)
        {
            Redresseur red = (Redresseur)this.DataContext;
            if (red != null)
            {
                if (red.Defaut)
                {
                   // DefautImage.Visibility = Visibility.Visible;
                }
                else
                {
                   // DefautImage.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
