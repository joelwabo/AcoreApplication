﻿using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AcoreApplication.Model.Constantes;
using static AcoreApplication.Model.Option;
using static AcoreApplication.Model.Registre;

namespace AcoreApplication.Model
{
    public class Redresseur : ObservableObject
    {
        #region ATTRIBUTS
        private int id;
        public int Id
        {
            get { return id; }
            set { NotifyPropertyChanged(ref id, value); }
        }
        private int idProcess;
        public int IdProcess
        {
            get { return idProcess; }
            set { NotifyPropertyChanged(ref idProcess, value); }
        }
        private int idAutomate;
        public int IdAutomate
        {
            get { return idAutomate; }
            set { NotifyPropertyChanged(ref idAutomate, value); }
        }
        private bool onOff;
        public bool OnOff
        {
            get { return onOff; }
            set { NotifyPropertyChanged(ref onOff, value); }
        }
        private bool miseSousTension;
        public bool MiseSousTension
        {
            get { return miseSousTension; }
            set { NotifyPropertyChanged(ref miseSousTension, value); }
        }
        private MODES etat;
        public MODES Etat
        {
            get { return etat; }
            set { NotifyPropertyChanged(ref etat, value); }
        }
        private TYPEREDRESSEUR type;
        public TYPEREDRESSEUR Type
        {
            get { return type; }
            set { NotifyPropertyChanged(ref type, value); }
        }
        private float calibreV;
        public float CalibreV
        {
            get { return calibreV; }
            set { NotifyPropertyChanged(ref calibreV, value); }
        }
        private int uMax;
        public int UMax
        {
            get { return uMax; }
            set { NotifyPropertyChanged(ref uMax, value); }
        }
        private float calibreA;
        public float CalibreA
        {
            get { return calibreA; }
            set { NotifyPropertyChanged(ref calibreA, value); }
        }
        private int iMax;
        public int IMax
        {
            get { return iMax; }
            set { NotifyPropertyChanged(ref iMax, value); }
        }
        private int consigneV;
        public int ConsigneV
        {
            get { return consigneV; }
            set { NotifyPropertyChanged(ref consigneV, value); }
        }
        private int consigneA;
        public int ConsigneA
        {
            get { return id; }
            set { NotifyPropertyChanged(ref consigneA, value); }
        }
        private int lectureV;
        public int LectureV
        {
            get { return lectureV; }
            set { NotifyPropertyChanged(ref lectureV, value); }
        }
        private int lectureA;
        public int LectureA
        {
            get { return lectureA; }
            set { NotifyPropertyChanged(ref lectureA, value); }
        }
        private int temperature;
        public int Temperature
        {
            get { return temperature; }
            set { NotifyPropertyChanged(ref temperature, value); }
        }
        private bool aH;
        public bool AH
        {
            get { return aH; }
            set { NotifyPropertyChanged(ref aH, value); }
        }
        private int compteurAH;
        public int CompteurAH
        {
            get { return compteurAH; }
            set { NotifyPropertyChanged(ref compteurAH, value); }
        }
        private CALIBRE calibreAH;
        public CALIBRE CalibreAH
        {
            get { return calibreAH; }
            set { NotifyPropertyChanged(ref calibreAH, value); }
        }
        private bool inverseur;
        public bool Inverseur
        {
            get { return inverseur; }
            set { NotifyPropertyChanged(ref inverseur, value); }
        }
        private bool prevent;
        public bool Prevent
        {
            get { return prevent; }
            set { NotifyPropertyChanged(ref prevent, value); }
        }
        private bool pulse;
        public bool Pulse
        {
            get { return pulse; }
            set { NotifyPropertyChanged(ref pulse, value); }
        }
        private bool temporisation;
        public bool Temporisation
        {
            get { return temporisation; }
            set { NotifyPropertyChanged(ref temporisation, value); }
        }
        private int tempsOn;
        public int TempsOn
        {
            get { return tempsOn; }
            set { NotifyPropertyChanged(ref tempsOn, value); }
        }
        private int tempsOff;
        public int TempsOff
        {
            get { return tempsOff; }
            set { NotifyPropertyChanged(ref tempsOff, value); }
        }
        private DateTime dureeTempo;
        public DateTime DureeTempo
        {
            get { return dureeTempo; }
            set { NotifyPropertyChanged(ref dureeTempo, value); }
        }
        private DateTime dureeRestante;
        public DateTime DureeRestante
        {
            get { return dureeRestante; }
            set { NotifyPropertyChanged(ref dureeRestante, value); }
        }
        private bool rampe;
        public bool Rampe
        {
            get { return rampe; }
            set { NotifyPropertyChanged(ref rampe, value); }
        }
        private DateTime dureeRampe;
        public DateTime DureeRampe
        {
            get { return dureeRampe; }
            set { NotifyPropertyChanged(ref dureeRampe, value); }
        }
        private string fichierIncident;
        public string FichierIncident
        {
            get { return fichierIncident; }
            set { NotifyPropertyChanged(ref fichierIncident, value); }
        }
        private bool defaut;
        public bool Defaut
        {
            get { return defaut; }
            set { NotifyPropertyChanged(ref defaut, value); }
        }
        
        private ObservableCollection<int> tab;
        public ObservableCollection<int> Tab
        {
            get { return tab; }
            set { NotifyPropertyChanged(ref tab, value); }
        }

        public ChartValues<double> valuesA;
        public ChartValues<double> ValuesA
        {
            get { return valuesA; }
            set { NotifyPropertyChanged(ref valuesA, value); }
        }
        public ChartValues<double> valuesB;
        public ChartValues<double> ValuesB
        {
            get { return valuesB; }
            set { NotifyPropertyChanged(ref valuesB, value); }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public ObservableCollection<Option> options;
        public ObservableCollection<Option> Options
        {
            get { return options; }
            set { NotifyPropertyChanged(ref options, value); }
        }
        private ObservableCollection<Registre> registres;
        public ObservableCollection<Registre> Registres
        {
            get { return registres; }
            set { NotifyPropertyChanged(ref registres, value); }
        }
        private ObservableCollection<Historique> historiques;
        public ObservableCollection<Historique> Historiques
        {
            get { return historiques; }
            set { NotifyPropertyChanged(ref historiques, value); }
        }
        #endregion 

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Redresseur()
        {

        }

        public Redresseur(SqlDataReader reader)
        {
            ValuesA = new ChartValues<double> { 0 };
            ValuesB = new ChartValues<double> { 0 };
            Id = (int)reader["Id"];
            IdProcess = (int)reader["IdProcess"];
            IdAutomate = (int)reader["IdAutomate"];
            OnOff = (bool)reader["OnOff"];
            MiseSousTension = (bool)reader["MiseSousTension"];
            Etat = (MODES)Enum.Parse(typeof(MODES), (string)reader["Etat"]);
            Type = (TYPEREDRESSEUR)Enum.Parse(typeof(TYPEREDRESSEUR), (string)reader["Type"]);
            UMax = (int)reader["UMax"];
            IMax = (int)reader["IMax"];
            ConsigneV = (int)reader["ConsigneV"];
            ConsigneA = (int)reader["ConsigneA"];
            LectureV = (int)reader["LectureV"];
            LectureA = (int)reader["LectureA"];
            Temperature = (int)reader["Temperature"];
            AH = (bool)reader["AH"];
            CompteurAH = (int)reader["CompteurAH"];
            CalibreAH = (CALIBRE)Enum.Parse(typeof(CALIBRE), (string)reader["CalibreAH"]);
            Pulse = (bool)reader["Pulse"];
            Temporisation = (bool)reader["Temporisation"];
            TempsOn = (int)reader["TempsOn"];
            TempsOff = (int)reader["TempsOff"];
            DureeTempo = DateTime.Parse(reader["DureeTempo"].ToString());
            DureeRestante = DateTime.Parse(reader["DureeRestante"].ToString());
            Rampe = (bool)reader["Rampe"];
            DureeRampe = DateTime.Parse(reader["DureeRampe"].ToString());
            Defaut = (bool)reader["Defaut"];

            Options = GetAllOptionsFromTableId(Id, "Id" + this.GetType().Name);
            Registres = GetAllRegisterFromRedresseurId(Id);
            Historiques = new ObservableCollection<Historique>();
            Historique historique = new Historique();
            historique.GetHistoriqueFromDB(Id);
            Historiques.Add(historique);
        }
        #endregion

        #region METHODES

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public static ObservableCollection<Redresseur> GetAllRedresseurFromAutotameId(int idAutomate)
        {
            ObservableCollection<Redresseur> redresseurs = new ObservableCollection<Redresseur>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Redresseur WHERE IdAutomate = " + idAutomate, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Redresseur redresseur = new Redresseur(reader);
                        redresseurs.Add(redresseur);
                    }
                }
            }
            return redresseurs;
        }

        #endregion
    }
}
