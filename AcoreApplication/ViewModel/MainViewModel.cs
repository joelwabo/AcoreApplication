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
        public ICommand LoadingSegmentCommand { get; set; }
        public ICommand EditingSegmentCommand { get; set; }
        public ICommand EditingRecetteCommand { get; set; }
        public ICommand EditingProcessCommand { get; set; }
        public ICommand ValideButton { get; set; }
        public ICommand AddingProcessCommand { get; set; }
        public ICommand AddingSegmentCommand { get; set; }
        public ICommand AddingRecetteCommand { get; set; }
        public ICommand AddingRedresseurCommand { get; set; }
        public ICommand SelectedRecetteChangedCommand { get; set; }

        public ICommand CloseButtonCommand { get; set; }
        public ICommand ARowEditEnding { get; set; }

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

        private DataService.Process processSelected = null;
        public DataService.Process ProcessSelected
        {
            get { return processSelected; }
            set { NotifyPropertyChanged(ref processSelected, value); }
        }

        private Recette recetteSelected = null;
        public Recette RecetteSelected
        {
            get { return recetteSelected; }
            set { NotifyPropertyChanged(ref recetteSelected, value); }
        }

        private ObservableCollection<DataService.Automate> listAutomate;
        public ObservableCollection<DataService.Automate> ListAutomate
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

        private ObservableCollection<Redresseur> listRedresseurToShow;
        public ObservableCollection<Redresseur> ListRedresseurToShow

        {
            get { return listRedresseurToShow; }
            set { NotifyPropertyChanged(ref listRedresseurToShow, value); }
        }

        private ObservableCollection<DataService.Process> listProcess;
        public ObservableCollection<DataService.Process> ListProcess
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

        private Visibility pulseVisibilityParam = Visibility.Visible;
        public Visibility PulseVisibilityParam
        {
            get { return pulseVisibilityParam; }
            set { NotifyPropertyChanged(ref pulseVisibilityParam, value); }
        }
            
        private Visibility typeVisibilityParam = Visibility.Visible;
        public Visibility TypeVisibilityParam
        {
            get { return typeVisibilityParam; }
            set { NotifyPropertyChanged(ref typeVisibilityParam, value); }
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

        private ObservableCollection<AcoreApplication.Model.Constantes.CALIBRE> listCalibres;
        public ObservableCollection<AcoreApplication.Model.Constantes.CALIBRE> ListCalibres

        {
            get { return listCalibres; }
            set { NotifyPropertyChanged(ref listCalibres, value); }
        }
        #endregion

        #region Methode
        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            checkChanges();
            return true;
        }

        public MainViewModel(IProcessService _processService)
        {
            ListAutomate = AutomateService.GetAllData();
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
            ListHistorique = SimpleIoc.Default.GetInstance<IHistoriqueService>().GetAllData();
            ListRedresseur = new ObservableCollection<Redresseur>();
            ListRedresseurToShow = new ObservableCollection<Redresseur>();

            PulseVisibilityParam = Visibility.Collapsed;
            TypeVisibilityParam = Visibility.Visible;

            tempoVisibilityParam = new Visibility();
            TempoVisibilityParam = Visibility.Visible;
            imageSource = "../Resources/log_in1.png";

            foreach (DataService.Automate automate in ListAutomate)
            {
                foreach (Redresseur redresseur in ListAutomate[ListAutomate.IndexOf(automate)].Redresseurs) {
                    ListRedresseur.Add(redresseur);
                }
            }


            ListEtats = new ObservableCollection<AcoreApplication.Model.Constantes.MODES>();
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "LocalRecette"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "LocalManuel"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "RemoteManuel"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "Supervision"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "RemoteRecette"));



            ListCalibres = new ObservableCollection<AcoreApplication.Model.Constantes.CALIBRE>();
            ListCalibres.Add((AcoreApplication.Model.Constantes.CALIBRE)Enum.Parse(typeof(AcoreApplication.Model.Constantes.CALIBRE), "A_H"));
            ListCalibres.Add((AcoreApplication.Model.Constantes.CALIBRE)Enum.Parse(typeof(AcoreApplication.Model.Constantes.CALIBRE), "A_S"));
            ListCalibres.Add((AcoreApplication.Model.Constantes.CALIBRE)Enum.Parse(typeof(AcoreApplication.Model.Constantes.CALIBRE), "A_MN"));



            RedresseurSelected = null;
            ProcessSelected = ListProcess[0];
            RecetteSelected = ProcessSelected.Recettes[0];
            historiqueSelectedSegment = null;

            AddingProcessCommand = new RelayCommand<Object>(AddingProcess);
            AddingRecetteCommand = new RelayCommand<AddingNewItemEventArgs>(AddingRecette);
            AddingSegmentCommand = new RelayCommand<AddingNewItemEventArgs>(AddingSegment);
            AddingRedresseurCommand = new RelayCommand<AddingNewItemEventArgs>(AddingRedresseur);
            LoadingSegmentCommand = new RelayCommand<DataGridRowEventArgs>(LoadingSegment);
            EditingSegmentCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingSegment);
            EditingRecetteCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingRecette);
            EditingProcessCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingProcess);
            RegistreLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(RegistreLoadingRow);
            SelectedProcessChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedProcessChanged);
            SelectedRecetteChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedRecetteChanged);
            CloseButtonCommand = new RelayCommand<Object>(CloseButtonCommandMethod);
            SelectedHistoriqueChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedHistoriqueChanged);

            ARowEditEnding = new RelayCommand<SelectedCellsChangedEventArgs>(ARowEditEndingMethod);
            ValideButton = new RelayCommand<Object>(valideButton);
            

        }

        private void CloseButtonCommandMethod(Object obj)
        {
            if (ListRedresseurToShow != null)
            {
                foreach (Redresseur redresseur in ListRedresseurToShow)
                {
                    if (redresseur.Id == (int) obj)
                    {
                        ListRedresseurToShow.Remove(redresseur);
                        break;
                    }
                }
            }
        }

        private void checkChanges() {
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

            if (checkTypeVisibility())
            {
                TypeVisibilityParam = Visibility.Visible;
            }
            else
            {
                TypeVisibilityParam = Visibility.Collapsed;
            }

        }

        private bool checkTypeVisibility()
        {
            if (ListRedresseur != null)
            {
                foreach (Redresseur redresseur in ListRedresseur)
                {
                    if (redresseur.Inverseur == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ARowEditEndingMethod(SelectedCellsChangedEventArgs arg)
        {
            checkChanges();
            if (ListRedresseur != null)
            {
                foreach (Redresseur redresseur2 in ListRedresseurToShow)
                {
                    if (redresseur2.Id == (int)redresseurSelected.Id)
                    {
                        return;
                    }
                }

                foreach (Redresseur redresseur in ListRedresseur)
                {
                    if (redresseur.Id == redresseurSelected.Id)
                    {
                        ListRedresseurToShow.Add(redresseur);
                      
                    }
                }
            }

        }

        private bool checkPulseVisibility(){
            if (ListRedresseur!=null)
            {
                foreach (Redresseur redresseur in ListRedresseur)
                {
                    if (redresseur.Pulse == true)
                    {

                        
                        return true;
                    }
                }
            }
            return false;
        }
        private bool checkTempoVisibility()
        {
            if (ListRedresseur != null)
            {
                foreach (Redresseur redresseur in ListRedresseur)
                {
                    if (redresseur.Temporisation == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private void AddingProcess(Object obj)
        {
            SimpleIoc.Default.GetInstance<IProcessService>().Insert();
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
        }

        private void AddingRedresseur(AddingNewItemEventArgs arg)
        {
            foreach (Redresseur redresseur in ListRedresseur)
                SimpleIoc.Default.GetInstance<IRedresseurService>().Update(redresseur);

            SimpleIoc.Default.GetInstance<IRedresseurService>().Insert();
            ListAutomate = AutomateService.GetAllData();
        }

        private void AddingSegment(AddingNewItemEventArgs arg)
        {
            arg.NewItem = new Segment(RecetteSelected.Id);
            SimpleIoc.Default.GetInstance<ISegmentService>().Insert(arg.NewItem as Segment);
        }

        private void AddingRecette(AddingNewItemEventArgs arg)
        {
            arg.NewItem = new Recette(ProcessSelected.Id);
            SimpleIoc.Default.GetInstance<IRecetteService>().Insert(arg.NewItem as Recette);
        }

        private void valideButton(Object obj)
        {
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
            ListHistorique = SimpleIoc.Default.GetInstance<IHistoriqueService>().GetAllData();
            ListRedresseur = new ObservableCollection<Redresseur>();
            foreach (DataService.Automate automate in ListAutomate)
                foreach (Redresseur redresseur in ListAutomate[ListAutomate.IndexOf(automate)].Redresseurs)
                    ListRedresseur.Add(redresseur);
        }

        private void EditingProcess(DataGridRowEditEndingEventArgs arg)
        {
            DataService.Process process = arg.Row.Item as DataService.Process;
            SimpleIoc.Default.GetInstance<IProcessService>().Update(process);
        }

        private void EditingSegment(DataGridRowEditEndingEventArgs arg)
        {
            Segment segment = arg.Row.Item as Segment;
            SimpleIoc.Default.GetInstance<ISegmentService>().Update(segment);
            Messenger.Default.Send(RecetteSelected.Segments);
        }

        private void EditingRecette(DataGridRowEditEndingEventArgs arg)
        {
            Recette recette = arg.Row.Item as Recette;
            SimpleIoc.Default.GetInstance<IRecetteService>().Update(recette);
        }

        private void RegistreLoadingRow(DataGridRowEventArgs arg)
        {
            Messenger.Default.Send(RedresseurSelected.Registres);
        }

        private void LoadingSegment(DataGridRowEventArgs arg)
        {
            Messenger.Default.Send(RecetteSelected.Segments);
        }

        private void SelectedProcessChanged(SelectionChangedEventArgs arg)
        {
            if(arg.AddedItems.Count>0)
            {
                DataService.Process process = arg.AddedItems[0] as DataService.Process;
                ProcessSelected = process;
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
