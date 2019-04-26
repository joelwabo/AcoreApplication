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
using System.Text;
using System.IO;
using Microsoft.Win32;

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
        public ICommand EditingRegistreCommand { get; set; }
        public ICommand EditingRecetteCommand { get; set; }
        public ICommand EditingProcessCommand { get; set; }
        public ICommand ValideButton { get; set; }
        public ICommand AddingProcessCommand { get; set; }
        public ICommand AddingSegmentCommand { get; set; }
        public ICommand AddingRecetteCommand { get; set; }
        public ICommand AddingRegistreCommand { get; set; }
        public ICommand AddingRedresseurCommand { get; set; }
        public ICommand SelectedRecetteChangedCommand { get; set; }

        public ICommand CloseButtonCommand { get; set; }

        public ICommand ImportRedresseurCommand { get; set; }
        
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

        private Visibility temperatureVisibilityParam = Visibility.Visible;
        public Visibility TemperatureVisibilityParam
        {
            get { return temperatureVisibilityParam; }
            set { NotifyPropertyChanged(ref temperatureVisibilityParam, value); }
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
            TemperatureVisibilityParam = Visibility.Visible;

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
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "RemoteRecette"));
            ListEtats.Add((AcoreApplication.Model.Constantes.MODES)Enum.Parse(typeof(AcoreApplication.Model.Constantes.MODES), "Supervision"));



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
            AddingRegistreCommand = new RelayCommand<AddingNewItemEventArgs>(AddingRegistre);
            //AddingRegistreCommand = new RelayCommand<Object>(AddRegistre);
            AddingSegmentCommand = new RelayCommand<AddingNewItemEventArgs>(AddingSegment);
            AddingRedresseurCommand = new RelayCommand<AddingNewItemEventArgs>(AddingRedresseur);
            LoadingSegmentCommand = new RelayCommand<DataGridRowEventArgs>(LoadingSegment);
            EditingSegmentCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingSegment);
            EditingRecetteCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingRecette);
            EditingRegistreCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingRegistre);
            EditingProcessCommand = new RelayCommand<DataGridRowEditEndingEventArgs>(EditingProcess);
            RegistreLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(RegistreLoadingRow);
            SelectedProcessChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedProcessChanged);
            SelectedRecetteChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedRecetteChanged);
            CloseButtonCommand = new RelayCommand<Object>(CloseButtonCommandMethod);
            SelectedHistoriqueChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedHistoriqueChanged);

            ARowEditEnding = new RelayCommand<SelectedCellsChangedEventArgs>(ARowEditEndingMethod);
            ValideButton = new RelayCommand<Object>(valideButton);
            ImportRedresseurCommand = new RelayCommand<Object>(ImportRedresseur);


            //ExampleOf CSV Import and export and picker
            //exportCSV("C:/Users/Pablo.PEREZ-MARTINEZ/Downloads/csvHeader.csv");

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

            if (checkTemperatureVisibility())
            {
                TemperatureVisibilityParam = Visibility.Visible;
            }
            else
            {
                TemperatureVisibilityParam = Visibility.Collapsed;
            }

        }

        private bool checkTemperatureVisibility()
        {
            if (ListRedresseur != null)
            {
                foreach (Redresseur redresseur in ListRedresseur)
                {
                    if (redresseur.Temperature>=0)
                    {
                        return true;
                    }
                }
            }
            return false;
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
                    if (redresseurSelected != null)
                    {
                        if (redresseur2.Id == (int)redresseurSelected.Id)
                        {
                            return;
                        }
                    }
                }

                foreach (Redresseur redresseur in ListRedresseur)
                {
                    if (redresseurSelected != null)
                    {
                        if (redresseur.Id == redresseurSelected.Id)
                        {
                            ListRedresseurToShow.Add(redresseur);

                        }
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
            SimpleIoc.Default.GetInstance<IRedresseurService>().Insert();
            foreach (Redresseur redresseur in ListRedresseur)
                SimpleIoc.Default.GetInstance<IRedresseurService>().Update(redresseur);

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
        /*
        private void AddRegistre(Object obj) {

            SimpleIoc.Default.GetInstance<IRegistreService>().Insert(new DataService.Registre(RedresseurSelected.Id));
            ObservableCollection<DataService.Registre> list = SimpleIoc.Default.GetInstance<IRegistreService>().GetAllData(RedresseurSelected.Id);
            RedresseurSelected.Registres = list;
        }
        */
        private void AddingRegistre(AddingNewItemEventArgs arg)
        {
            SimpleIoc.Default.GetInstance<IRegistreService>().Insert(new DataService.Registre(RedresseurSelected.Id));
            ObservableCollection<DataService.Registre> list = SimpleIoc.Default.GetInstance<IRegistreService>().GetAllData(RedresseurSelected.Id);
            //RedresseurSelected.Registres = list;
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

        private void EditingRegistre(DataGridRowEditEndingEventArgs arg)
        {
            DataService.Registre registre = arg.Row.Item as DataService.Registre;
            SimpleIoc.Default.GetInstance<IRegistreService>().Update(registre);
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

        private void ImportRedresseur(Object obj) {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "Text documents (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;

                using (var reader = new StreamReader(@filename))
                {
                    List<string> listA = new List<string>();
                    List<string> listB = new List<string>();
                    while (!reader.EndOfStream)
                    {

                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        try
                        {
                            Redresseur red = new Redresseur();
                            red.IpAdresse = "192.168.1.111";
                            red.IdProcess = Int32.Parse(values[1]);

                            SimpleIoc.Default.GetInstance<IRedresseurService>().Insert();
                            ListRedresseur = SimpleIoc.Default.GetInstance<IRedresseurService>().GetAllData();
                            
                            //SimpleIoc.Default.GetInstance<IRedresseurService>().Insert();
                            //Redresseur red = new Redresseur(new DataService.Redresseur());
                            //red.IpAdresse = values[2];

                        }

                        catch {
                        }
                    }
                }
            }

        }
        private void exportCSV(string filePath) {
            //before your loop

            //filePath = "C://csvHeader.csv";

            /* WRITE A CSV FILE
            var csv = new StringBuilder();

            //in your loop
            var first = "HELLO";
            var second = "HELLO2";
            var third = "HELLO3";
            var newLine = string.Format("{0},{1},{2}", first,second,third);
            csv.AppendLine(newLine);

            //after your loop
            File.WriteAllText(filePath, csv.ToString());


            */

            /*
            //READ A CSV FILE
             // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "Text documents (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;

                using (var reader = new StreamReader(@filename))
                {
                    List<string> listA = new List<string>();
                    List<string> listB = new List<string>();
                    while (!reader.EndOfStream)
                    {

                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listA.Add(values[0]);
                        listB.Add(values[1]);
                    }
                }
            }


            */


}

#endregion
}

}
