using GalaSoft.MvvmLight;
using AcoreApplication.Model;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Input;
using AcoreApplication.FrameworkMvvm;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace AcoreApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ATTRIBUTS 
        public ICommand OnOffCommand { get; set; }
        public ICommand StartServiceCommand { get; set; }
        public ICommand ClickedProcessCommand { get; set; }
        public ICommand SegmentLoadingRowCommand { get; set; }
        public ICommand SegmentCellEditCommand { get; set; }

        private Recette recetteSelected = null;
        public Recette RecetteSelected
        {
            get { return recetteSelected; }
            set { NotifyPropertyChanged(ref recetteSelected, value); }
        }

        private ObservableCollection<Automate> listAutomate;
        public ObservableCollection<Automate> ListAutomate
        {
            get { return listAutomate; }
            set { NotifyPropertyChanged(ref listAutomate, value); }
        }

        private ObservableCollection<Redresseur> listRedresseur;
        public ObservableCollection<Redresseur> ListRedresseur
        {
            get { return listRedresseur; }
            set { NotifyPropertyChanged(ref listRedresseur, value); }
        }

        private ObservableCollection<Process> listProcess;
        public ObservableCollection<Process> ListProcess
        {
            get { return listProcess; }
            set { NotifyPropertyChanged(ref listProcess, value); }
        }

        private ObservableCollection<Historique> listHistorique;
        public ObservableCollection<Historique> ListHistorique
        {
            get { return listHistorique; }
            set { NotifyPropertyChanged(ref listHistorique, value); }
        }

        #endregion

        #region Methode
        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }
                
        public MainViewModel(IAutomateService automateService, IProcessService processService)
        {
            ListAutomate = automateService.GetAllData();
            ListProcess = processService.GetAllData();
            ListRedresseur = new ObservableCollection<Redresseur>();
            ListHistorique = new ObservableCollection<Historique>();
            foreach (Automate automate in ListAutomate)
                foreach (Redresseur redresseur in ListAutomate[ListAutomate.IndexOf(automate)].Redresseurs)
                {
                    ListRedresseur.Add(redresseur);
                    foreach (Historique historique in redresseur.Historiques)
                        ListHistorique.Add(historique);
                }

            OnOffCommand = new RelayCommand<Redresseur>(OnOff, CanExecuteOnOff); 
            StartServiceCommand = new RelayCommand<Redresseur>(StartService);
            ClickedProcessCommand = new RelayCommand<Process>(OnProcessClicked);
            SegmentLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(SegmentLoadingRow);
            SegmentCellEditCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(CellEditCommand);
            
            RecetteSelected = ListProcess[0].Recettes[0];
        }

        private void SegmentLoadingRow(DataGridRowEventArgs arg)
        {
            Messenger.Default.Send(RecetteSelected.Segments);
        }
        private void CellEditCommand(DataGridCellEditEndingEventArgs arg)
        {
            Messenger.Default.Send(RecetteSelected.Segments);
        }

        private void OnProcessClicked(Process process)
        {
            RecetteSelected = process.Recettes[0];
            Messenger.Default.Send(process);
        }

        private void OnOff(Redresseur redresseur)
        {
            if (redresseur.OnOff)
            {
                ListAutomate[redresseur.IdAutomate - 1].StopModbusService();
                redresseur.OnOff = false;
            }
            else
            { 
                ListAutomate[redresseur.IdAutomate - 1].StartModbusService();
                redresseur.OnOff = true;
            }
        }

        private void StartService(Redresseur redresseur)
        {
            if (redresseur.OnOff)
                ListAutomate[redresseur.IdAutomate - 1].StartRecipe();
            else
                MessageBox.Show("Machine éteinte");
        }

        private bool CanExecuteOnOff(Redresseur redresseur)
        {
            return true;
        }
        
        #endregion
    }
}