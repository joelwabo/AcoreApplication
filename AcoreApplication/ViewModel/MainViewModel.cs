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
using System.Linq;
using GalaSoft.MvvmLight.Ioc;

namespace AcoreApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ATTRIBUTS 
        public ICommand OnOffCommand { get; set; }
        public ICommand StartServiceCommand { get; set; }
        public ICommand SelectedProcessChangedCommand { get; set; }
        public ICommand SelectedHistoriqueChangedCommand { get; set; }
        public ICommand RegistreLoadingRowCommand { get; set; }
        public ICommand SegmentLoadingRowCommand { get; set; }
        public ICommand SegmentCellEditCommand { get; set; }
        public ICommand AddingNewProcessCommand { get; set; }
        public ICommand EditingProcessCommand { get; set; }
        public ICommand ValideButton { get; set; }
        public ICommand AddingNewSegmentCommand { get; set; }
        public ICommand AddingNewRecetteCommand { get; set; }
        public ICommand AddingNewRedresseurCommand { get; set; }
        public ICommand SelectedRecetteChangedCommand { get; set; }

        private ObservableCollection<Segment> historiqueSelectedSegment = null;
        public ObservableCollection<Segment> HistoriqueSelectedSegment
        {
            get { return historiqueSelectedSegment; }
            set { NotifyPropertyChanged(ref historiqueSelectedSegment, value); }
        }
        private Redresseur redresseurSelected = null;
        public Redresseur RedresseurSelected
        {
            get { return redresseurSelected; }
            set { NotifyPropertyChanged(ref redresseurSelected, value); }
        }
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

        private ObservableCollection<DataService.Historique> listHistorique;
        public ObservableCollection<DataService.Historique> ListHistorique
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
                
        public MainViewModel(IAutomateService _automateService)
        {
            ListAutomate = _automateService.GetAllData();
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
            ListHistorique = SimpleIoc.Default.GetInstance<IHistoriqueService>().GetAllData();
            ListRedresseur = new ObservableCollection<Redresseur>();
            foreach (Automate automate in ListAutomate)
                foreach (Redresseur redresseur in ListAutomate[ListAutomate.IndexOf(automate)].Redresseurs)
                    ListRedresseur.Add(redresseur);

            SelectedProcessChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedProcessChanged);
            SelectedRecetteChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedRecetteChanged);
            SelectedHistoriqueChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedHistoriqueChanged);
            SegmentLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(SegmentLoadingRow);
            RegistreLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(RegistreLoadingRow);
            SegmentCellEditCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(CellEditCommand);
            AddingNewProcessCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewProcess);
            AddingNewRecetteCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewRecette);
            AddingNewSegmentCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewSegment);
            EditingProcessCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditingProcess);
            AddingNewRedresseurCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewRedresseur);
            ValideButton = new RelayCommand<Object>(valideButton);
            RecetteSelected = ListProcess[0].Recettes[0];
            RedresseurSelected = ListRedresseur[0];
            historiqueSelectedSegment = null ;
        }              

        private void AddingNewRedresseur(AddingNewItemEventArgs arg)
        {
            foreach (Redresseur redresseur in ListRedresseur)
                SimpleIoc.Default.GetInstance<IRedresseurService>().UpdateRedresseur(redresseur);

            SimpleIoc.Default.GetInstance<IRedresseurService>().InsertRedresseur();
            ListAutomate = SimpleIoc.Default.GetInstance<IAutomateService>().GetAllData();
        }

        private void AddingNewSegment(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                foreach (Recette rec in process.Recettes)
                    foreach (Segment seg in rec.Segments)
                        SimpleIoc.Default.GetInstance<ISegmentService>().UpdateSegment(seg);

            SimpleIoc.Default.GetInstance<ISegmentService>().InsertSegment();
        }

        private void AddingNewRecette(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                foreach (Recette rec in process.Recettes)
                    SimpleIoc.Default.GetInstance<IRecetteService>().UpdateRecette(rec);

            SimpleIoc.Default.GetInstance<IRecetteService>().InsertRecette();
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
        }

        private void valideButton(Object obj)
        {
            foreach (Process process in ListProcess)
            {
                SimpleIoc.Default.GetInstance<IProcessService>().UpdateProcess(process);
                if (process.Recettes != null)
                    foreach (Recette rec in process.Recettes)
                    {
                        SimpleIoc.Default.GetInstance<IRecetteService>().UpdateRecette(rec);
                        if (rec.Segments != null)
                            foreach (Segment seg in rec.Segments)
                                SimpleIoc.Default.GetInstance<ISegmentService>().UpdateSegment(seg);
                    }
            }
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
            ListHistorique = SimpleIoc.Default.GetInstance<IHistoriqueService>().GetAllData();
            ListAutomate = SimpleIoc.Default.GetInstance<IAutomateService>().GetAllData();
        }

        private void EditingProcess(DataGridCellEditEndingEventArgs arg)
        {
        }

        private void AddingNewProcess(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                SimpleIoc.Default.GetInstance<IProcessService>().UpdateProcess(process);

            SimpleIoc.Default.GetInstance<IProcessService>().InsertProcess();
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
        }

        private void RegistreLoadingRow(DataGridRowEventArgs arg)
        {
            Messenger.Default.Send(RedresseurSelected.Registres);
        }

        private void SegmentLoadingRow(DataGridRowEventArgs arg)
        {
            Messenger.Default.Send(RecetteSelected.Segments);
        }

        private void CellEditCommand(DataGridCellEditEndingEventArgs arg)
        {
            Messenger.Default.Send(RecetteSelected.Segments);
        }

        private void SelectedProcessChanged(SelectionChangedEventArgs arg)
        {
            if(arg.AddedItems.Count>0)
            { 
                Process process = arg.AddedItems[0] as Process;
                Messenger.Default.Send(process);
            }
        }

        private void SelectedRecetteChanged(SelectionChangedEventArgs arg)
        {
            if (arg.AddedItems.Count > 0)
            {
                Recette recette = arg.AddedItems[0] as Recette;
                Redresseur red = (arg.Source as ComboBox).DataContext as Redresseur;
                red.SelectedRecette = recette;
                red.SelectedRecette.SegCours = 0;
            }
        }

        private void SelectedHistoriqueChanged(SelectionChangedEventArgs arg)
        {
            if (arg.AddedItems.Count > 0)
            {
                DataService.Historique historique = arg.AddedItems[0] as DataService.Historique;
                HistoriqueSelectedSegment = historique.Recette2.Segments;
                Messenger.Default.Send(historique);
            }
        }

        private bool CanExecuteOnOff(Redresseur redresseur)
        {
            return true;
        }
        
        #endregion
    }

}