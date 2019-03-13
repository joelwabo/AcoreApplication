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

namespace AcoreApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ATTRIBUTS 
        IProcessService processService;
        IRecetteService recetteService;
        ISegmentService segmentService;
        public ICommand OnOffCommand { get; set; }
        public ICommand StartServiceCommand { get; set; }
        public ICommand SelectedProcessChangedCommand { get; set; }
        public ICommand SegmentLoadingRowCommand { get; set; }
        public ICommand SegmentCellEditCommand { get; set; }
        public ICommand AddingNewProcessCommand { get; set; }
        public ICommand EditingProcessCommand { get; set; }
        public ICommand ValideButton { get; set; }
        public ICommand AddingNewSegmentCommand { get; set; }
        public ICommand AddingNewRecetteCommand { get; set; }

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
                
        public MainViewModel(IAutomateService automateService, IProcessService _processService, IRecetteService _recetteService, ISegmentService _segmentService)
        {
            processService = _processService;
            recetteService = _recetteService;
            segmentService = _segmentService;
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

            SelectedProcessChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedProcessChanged);
            SegmentLoadingRowCommand = new RelayCommand<DataGridRowEventArgs>(SegmentLoadingRow);
            SegmentCellEditCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(CellEditCommand);
            AddingNewProcessCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewProcess);
            AddingNewRecetteCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewRecette);
            AddingNewSegmentCommand = new RelayCommand<AddingNewItemEventArgs>(AddingNewSegment);
            EditingProcessCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditingProcess);
            ValideButton = new RelayCommand<Object>(valideButton);
            RecetteSelected = ListProcess[0].Recettes[0];
        }

        private void AddingNewSegment(AddingNewItemEventArgs arg)
        {
            foreach (Process process in ListProcess)
                foreach (Recette rec in process.Recettes)
                    foreach (Segment seg in rec.Segments)
                        segmentService.UpdateSegment(seg);

            segmentService.InsertSegment();
            ListProcess = processService.GetAllData();
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
                foreach (Recette rec in process.Recettes)
                {
                    recetteService.UpdateRecette(rec);
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
        
        private bool CanExecuteOnOff(Redresseur redresseur)
        {
            return true;
        }
        
        #endregion
    }

}