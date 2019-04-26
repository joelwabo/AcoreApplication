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

namespace AcoreApplication.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Clk(object sender, RoutedEventArgs e)
        {
            //this should be in the viewModel
            this.Hide();
            //TODO: check password from Database
            if (checkPassword())
            {
                MainWindow win = new MainWindow();
                win.ShowDialog();
            }

        }
        private bool checkPassword() {
            if (this.username.Text == "User name")
            {
                return true;
            }
            else {
                return false;
            }
            
        }
    }
}
