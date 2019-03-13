using GalaSoft.MvvmLight;
using AcoreApplication.Model;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;

namespace AcoreApplication.ViewModel
{
    public class MachineViewModel : ViewModelBase
    {
        #region ATTRIBUTS 
        public ICommand SelectedModeCommand { get; set; }


        #endregion

        #region Methode
        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public MachineViewModel()
        {
            SelectedModeCommand = new RelayCommand<SelectionChangedEventArgs>(OnModeSelected);

        }

        private void OnModeSelected(SelectionChangedEventArgs arg)
        {
            string mode = arg.AddedItems[0] as string;
        }

        #endregion
    }
}
