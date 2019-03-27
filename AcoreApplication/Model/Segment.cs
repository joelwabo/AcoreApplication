using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class Segment : ObservableObject
    {
        #region ATTRIBUTS
        private int id;
        public int Id
        {
            get { return id; }
            set { NotifyPropertyChanged(ref id, value); }
        }
        private int idRecette;
        public int IdRecette
        {
            get { return idRecette; }
            set { NotifyPropertyChanged(ref idRecette, value); }
        }
        private string nom;
        public string Nom
        {
            get { return nom; }
            set { NotifyPropertyChanged(ref nom, value); }
        }
        private bool etat;
        public bool Etat
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
        private TimeSpan duree;
        public TimeSpan Duree
        {
            get { return duree; }
            set { NotifyPropertyChanged(ref duree, value); }
        }
        private int consigneDepartV;
        public int ConsigneDepartV
        {
            get { return consigneDepartV; }
            set { NotifyPropertyChanged(ref consigneDepartV, value); }
        }
        private int consigneDepartA;
        public int ConsigneDepartA
        {
            get { return consigneDepartA; }
            set { NotifyPropertyChanged(ref consigneDepartA, value); }
        }
        private int consigneArriveeV;
        public int ConsigneArriveeV
        {
            get { return consigneArriveeV; }
            set { NotifyPropertyChanged(ref consigneArriveeV, value); }
        }
        private int consigneArriveeA;
        public int ConsigneArriveeA
        {
            get { return consigneArriveeA; }
            set { NotifyPropertyChanged(ref consigneArriveeA, value); }
        }
        private TimeSpan tempsRestant;
        public TimeSpan TempsRestant
        {
            get { return tempsRestant; }
            set { NotifyPropertyChanged(ref tempsRestant, value); }
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
        private TimeSpan tempsOn;
        public TimeSpan TempsOn
        {
            get { return tempsOn; }
            set { NotifyPropertyChanged(ref tempsOn, value); }
        }
        private TimeSpan tempsOff;
        public TimeSpan TempsOff
        {
            get { return tempsOff; }
            set { NotifyPropertyChanged(ref tempsOff, value); }
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
        private bool rampe;
        public bool Rampe
        {
            get { return rampe; }
            set { NotifyPropertyChanged(ref rampe, value); }
        }
        private TimeSpan dureeRampe;
        public TimeSpan DureeRampe
        {
            get { return dureeRampe; }
            set { NotifyPropertyChanged(ref dureeRampe, value); }
        }

        private ObservableCollection<DataService.Options> options;
        public ObservableCollection<DataService.Options> Options
        {
            get { return options; }
            set { NotifyPropertyChanged(ref options, value); }
        }

        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Segment()
        {
        }

        public Segment(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            IdRecette = (int)reader["IdRecette"];
            Nom = (string)reader["Nom"];
            Etat = (bool)reader["Etat"];
            Type = (TYPEREDRESSEUR)Enum.Parse(typeof(TYPEREDRESSEUR), (string)reader["Type"]);
            Duree = TimeSpan.Parse(reader["Duree"].ToString());
            ConsigneDepartV = (int)reader["ConsigneDepartV"];
            ConsigneDepartA = (int)reader["ConsigneDepartA"];
            ConsigneArriveeV = (int)reader["ConsigneArriveeV"];
            ConsigneArriveeA = (int)reader["ConsigneArriveeA"];
            TempsRestant = TimeSpan.Parse(reader["TempsRestant"].ToString());
            Pulse = (bool)reader["Pulse"];
            CompteurAH = (int)reader["CompteurAH"];
            Temporisation = (bool)reader["Temporisation"];
            TempsOn = TimeSpan.Parse(reader["TempsOn"].ToString());
            TempsOff = TimeSpan.Parse(reader["TempsOff"].ToString());
            AH = (bool)reader["AH"];
            CompteurAH = (int)reader["CompteurAH"];
            CalibreAH = (CALIBRE)Enum.Parse(typeof(CALIBRE), (string)reader["CalibreAH"]);
            Rampe = (bool)reader["Rampe"];
            DureeRampe = TimeSpan.Parse(reader["DureeRampe"].ToString());

            Options = OptionsService.GetAllOptionsFromTableId(Id, "Id" + this.GetType().Name);
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

        public static ObservableCollection<Segment> GetAllSegmentFromRecetteId(int idRecette)
        {
            ObservableCollection<Segment> segments = new ObservableCollection<Segment>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Segment WHERE IdRecette = " + idRecette, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Segment segment = new Segment(reader);
                        segments.Add(segment);
                    }
                }
            }
            return segments;
        }

        #endregion
    }
}

