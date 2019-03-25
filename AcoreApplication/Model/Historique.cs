using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;
using System.Collections.ObjectModel;
using static AcoreApplication.Model.Redresseur;


namespace AcoreApplication.Model
{
    public class Historique
    {
        #region ATTRIBUTS
        public DateTime DateDebut { get; set; }
        public int IdRedresseur { get; set; }
        public int IdUtilisateur  { get; set; }
        public string OrdreFabrication { get; set; }
        public ETATFIN EtatFin { get; set; }
        public DateTime DateFin { get; set; }
        public MODES Type { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Historique()
        {

        }
        #endregion

        #region METHODS
        public Historique(SqlDataReader reader)
        { 
                    IdRedresseur = (int)reader["IdRedresseur"];
                    IdUtilisateur = (int)reader["IdUtilisateur"];
                    DateDebut = (DateTime)reader["DateDebut"];
                    OrdreFabrication = (string)reader["OrdreFabrication"];
                    EtatFin = (ETATFIN)Enum.Parse(typeof(ETATFIN), (string)reader["EtatFin"]);
                    DateFin = (DateTime)reader["DateFin"];   
                    //Type = (MODES)Enum.Parse(typeof(MODES), (string)reader["Type"]);
        }

        internal DataService.Historique ToDataBase()
        {
            DataService.Historique historique = new DataService.Historique();

            historique.IdRedresseur = IdRedresseur;
            historique.IdUtilisateur = IdUtilisateur;
            historique.DateDebut = DateDebut;
            historique.OrdreFabrication = OrdreFabrication;
            historique.EtatFin =EtatFin.ToString();
            historique.DateFin = DateFin;
            historique.Type = Type.ToString();

            return historique;

        }


        #endregion
    }
}
