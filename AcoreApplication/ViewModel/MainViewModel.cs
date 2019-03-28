﻿using GalaSoft.MvvmLight;
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

        #endregion

        #region Methode
        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }
                
        public MainViewModel(IProcessService _processService)
        {
            ListAutomate = AutomateService.GetAllData();
            ListProcess = SimpleIoc.Default.GetInstance<IProcessService>().GetAllData();
            ListHistorique = SimpleIoc.Default.GetInstance<IHistoriqueService>().GetAllData();
            ListRedresseur = new ObservableCollection<Redresseur>();
            foreach (DataService.Automate automate in ListAutomate)
                foreach (Redresseur redresseur in ListAutomate[ListAutomate.IndexOf(automate)].Redresseurs)
                    ListRedresseur.Add(redresseur);

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
            SelectedHistoriqueChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedHistoriqueChanged);
            ValideButton = new RelayCommand<Object>(valideButton);
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