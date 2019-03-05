using GalaSoft.MvvmLight;
using AcoreApplication.Model;
using System;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using AcoreApplication.Views;

namespace AcoreApplication.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        /*
         Var declaration
             */
        private void NotifyPropertyChanged(ref string id, string value)
        {
            throw new NotImplementedException();
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public ICommand loginButtonCommand { get; set; }

        private void loginButton(Object obj)
        {
            /*this.Hide();
            MainWindow win = new MainWindow();
            win.ShowDialog();*/
        }
        public LoginViewModel()
        {
            loginButtonCommand = new RelayCommand<Object>(loginButton);
        }
    }
}
