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
        IProcessService processService;
        IRecetteService recetteService;
        ISegmentService segmentService;
        IAutomateService automateService;
        IRedresseurService redresseurService;
        public ICommand OnOffCommand { get; set; }
        public ICommand StartServiceCommand { get; set; }
        public ICommand SelectedProcessChangedCommand { get; set; }
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
        public ICommand ARowEditEnding { get; set; }

        

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

        private ObservableCollection<Historique> listHistorique;
        public ObservableCollection<Historique> ListHistorique
        {
            get { return listHistorique; }
            set { NotifyPropertyChanged(ref listHistorique, value); }
        }

        private Visibility pulseVisibilityParam = Visibility.Visible;
        public Visibility PulseVisibilityParam
        {
            get { return pulseVisibilityParam; }
            set { NotifyPropertyChanged(ref pulseVisibilityParam, value); }
        }
        private Visibility tempoVisibilityParam = Visibility.Visible;
        public Visibility TempoVisibilityParam
        {
            get { return tempoVisibilityParam; }
            set { NotifyPropertyChanged(ref tempoVisibilityParam, value); }
        }

        private string imageSource = "../Resources/log_in1.png";
        public string ImageSource
        {
            get { return imageSource; }
            set { NotifyPropertyChanged(ref imageSource, value); }
        }

        private ObservableCollection<AcoreApplication.Model.Constantes.MODES> listEtats;
        public ObservableCollection<AcoreApplication.Model.Constantes.MODES> ListEtats

        {
            get { return listEtats; }
            set { NotifyPropertyChanged(ref listEtats, value); }
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
            automateService = _automateService;
            processService = SimpleIoc.Default.GetInstance<IProcessService>();
            recetteService = SimpleIoc.Default.GetInstance<IRecetteService>();
            segmentService = SimpleIoc.Default.GetInstance<ISegmentService>();
            redresseurService = SimpleIoc.Default.GetInstance<IRedresseurService>();
            ListAutomate = _automateService.GetAllData();
            ListProcess = processService.GetAllData();
            ListRedresseur = new ObservableCollection<Redresseur>();
            ListHistorique = new ObservableCollection<Historique>();

            pulseVisibilityParam = new Visibility();
            PulseVisibilityParam = Visibility.Visible;

            tempoVisibilityParam = new Visibility();
            TempoVisibilityParam = Visibility.Visible;
            imageSource = "../Resources/log_in1.png";

            foreach (Automate automate in ListAutomate)
                foreach (Redresseur redresseur in ListAutomate[ListAutomate.IndexOf(automate)].Redresseurs)
                {
                    ListRedresseur.Add(redresseur);
                    foreach (Historique historique in redresseur.Historiques)
                        ListHistorique.Add(historique);
                }

            ListEtats = new ObservableCollection<AcoreApplication.Model.Constantes.MODES>();
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "LocalRecette"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "LocalManuel"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "RemoteManuel"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "Supervision"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "RemoteRecette"));


            SelectedProcessChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedProcessChanged);
            SelectedRecetteChangedCommand = new RelayCommand<SelectionChangedEventArgs>(RecetteChangedCommand);
            SegmentLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(SegmentLoadingRow);
            RegistreLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(RegistreLoadingRow);
            SegmentCellEditCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(CellEditCommand);
            AddingNewProcessCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewProcess);
            AddingNewRecetteCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewRecette);
            AddingNewSegmentCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewSegment);
            EditingProcessCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditingProcess);
            AddingNewRedresseurCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewRedresseur);
            ARowEditEnding = new RelayCommand<SelectedCellsChangedEventArgs>(ARowEditEndingMethod);
            ValideButton = new RelayCommand<Object>(valideButton);
            RecetteSelected = ListProcess[0].Recettes[0];
            RedresseurSelected = ListRedresseur[0];
            
        }


        private void ARowEditEndingMethod(SelectedCellsChangedEventArgs arg)
        {
            if (checkPulseVisibility())
            {
                PulseVisibilityParam = Visibility.Visible;
            }
            else
            {
                PulseVisibilityParam = Visibility.Collapsed;
            }

            if (checkTempoVisibility())
            {
                TempoVisibilityParam = Visibility.Visible;
            }
            else
            {
                TempoVisibilityParam = Visibility.Collapsed;
            }

        }

        private bool checkPulseVisibility(){
            foreach (Redresseur redresseur in ListRedresseur)
            {
                if (redresseur.Pulse == true)
                {
                    return true;
                }
            }
            return false;
        }
        private bool checkTempoVisibility()
        {
            foreach (Redresseur redresseur in ListRedresseur)
            {
                if (redresseur.Temporisation == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddingNewRedresseur(AddingNewItemEventArgs arg)
        {
            foreach (Redresseur redresseur in ListRedresseur)
                redresseurService.UpdateRedresseur(redresseur);

            redresseurService.InsertRedresseur();
            ListAutomate = automateService.GetAllData();
        }

        private void AddingNewSegment(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                foreach (Recette rec in process.Recettes)
                    foreach (Segment seg in rec.Segments)
                        segmentService.UpdateSegment(seg);

            segmentService.InsertSegment();
        }

        private void AddingNewRecette(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                foreach (Recette rec in process.Recettes)
                    recetteService.UpdateRecette(rec);

            recetteService.InsertRecette();
            ListProcess = processService.GetAllData();
        }

        private void valideButton(Object obj)
        {
            foreach (Process process in ListProcess)
            { 
                processService.UpdateProcess(process);
                if (process.Recettes != null)
                    foreach (Recette rec in process.Recettes)
                    {
                        recetteService.UpdateRecette(rec);
                        if (rec.Segments != null)
                            foreach (Segment seg in rec.Segments)
                                    segmentService.UpdateSegment(seg);
                    }
            }
            ListProcess = processService.GetAllData();
        }

        private void EditingProcess(DataGridCellEditEndingEventArgs arg)
        {
        }

        private void AddingNewProcess(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                processService.UpdateProcess(process);

            processService.InsertProcess();
            ListProcess = processService.GetAllData();
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

        private void RecetteChangedCommand(SelectionChangedEventArgs arg)
        {
            if (arg.AddedItems.Count > 0)
            {
                Recette recette = arg.AddedItems[0] as Recette;
                Redresseur red = arg.Source as Redresseur;
                red.SelectedRecette = recette;
            }
        }

        private bool CanExecuteOnOff(Redresseur redresseur)
        {
            return true;
        }
        
        #endregion
    }

}