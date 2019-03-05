using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using static AcoreApplication.Model.Constantes;
using static AcoreApplication.Model.Option;
using static AcoreApplication.Model.Segment;

namespace AcoreApplication.Model
{
    public class Recette : ObservableObject
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
        private string nom;
        public string Nom
        {
            get { return nom; }
            set { NotifyPropertyChanged(ref nom, value); }
        }
        private int cyclage;
        public int Cyclage
        {
            get { return cyclage; }
            set { NotifyPropertyChanged(ref cyclage, value); }
        }
        private int segCours;
        public int SegCours
        {
            get { return segCours; }
            set { NotifyPropertyChanged(ref segCours, value); }
        }
        private DateTime tempsRestant;
        public DateTime TempsRestant
        {
            get { return tempsRestant; }
            set { NotifyPropertyChanged(ref tempsRestant, value); }
        }

        private ObservableCollection<Segment> segments;
        public ObservableCollection<Segment> Segments
        {
            get { return segments; }
            set { NotifyPropertyChanged(ref segments, value); }
        }
        private ObservableCollection<Option> options;
        public ObservableCollection<Option> Options
        {
            get { return options; }
            set { NotifyPropertyChanged(ref options, value); }
        }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Recette()
        {

        }

        public Recette(SqlDataReader reader)
        {

            Id = (int)reader["Id"];
            IdProcess = (int)reader["IdProcess"];
            Nom = (string)reader["Nom"];
            Cyclage = (int)reader["Cyclage"];
            TempsRestant = DateTime.Parse(reader["TempsRestant"].ToString());

            Segments = GetAllSegmentFromRecetteId(Id);
            Options = GetAllOptionsFromTableId(Id, "Id" + this.GetType().Name);
        }

        #endregion
        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public static ObservableCollection<Recette> GetAllRecetteFromProcessId(int idProcess)
        {
            ObservableCollection<Recette> recettes = new ObservableCollection<Recette>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Recette WHERE IdProcess = " + idProcess, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Recette recette = new Recette(reader);
                        recettes.Add(recette);
                    }
                }
            }
            return recettes;
        }
        #region METHODES



        #endregion

    }
}
