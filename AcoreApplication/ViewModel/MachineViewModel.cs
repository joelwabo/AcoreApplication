using GalaSoft.MvvmLight;
using AcoreApplication.Model;
using System;
using System.Runtime.CompilerServices;

namespace AcoreApplication.ViewModel
{
    public class MachineViewModel : ViewModelBase
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

        public MachineViewModel()
        {
        }
    }
}
