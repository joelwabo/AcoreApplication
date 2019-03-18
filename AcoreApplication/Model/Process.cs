using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;
using static AcoreApplication.Model.Recette;
using static AcoreApplication.Model.Option;
using System;

namespace AcoreApplication.Model
{
    public class Process
    {
        #region ATTRIBUTS
        public int Id { get; set; }
        public string Nom { get; set; }
        public int UMax { get; set; }
        public int IMax { get; set; }
        public bool Pulse { get; set; }
        public bool Inverseur { get; set; }
        public bool AH { get; set; }
        public ObservableCollection<Recette> Recettes { get; set; }
        public ObservableCollection<Option> Options { get; set; }
        #endregion 

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Process()
        {

        }

        public Process(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Nom = (string)reader["Nom"];
            UMax = (int)reader["UMax"];
            IMax = (int)reader["IMax"];
            Pulse = (bool)reader["Pulse"];
            Inverseur = (bool)reader["Inverseur"];
            AH = (bool)reader["AH"];

            Recettes = GetAllRecetteFromProcessId(Id);
            Options = GetAllOptionsFromTableId(Id, "Id" + this.GetType().Name);
        }
        #endregion

        #region METHODES


        internal DataBase.Process ToDataBase()
        {
            DataBase.Process process = new DataBase.Process();

            process.Nom = Nom;
            process.UMax = UMax;
            process.IMax = IMax;
            process.Pulse = Pulse;
            process.Inverseur = Inverseur;
            process.AH = AH;

            return process;
            
        }

        #endregion
    }
}
