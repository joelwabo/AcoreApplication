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

        public Historique(SqlDataReader reader)
        { 
                    IdRedresseur = (int)reader["IdRedresseur"];
                    IdUtilisateur = (int)reader["IdUtilisateur"];
                    DateDebut = (DateTime)reader["Date"];
                    OrdreFabrication = (string)reader["OrdreFabrication"];
                    EtatFin = (ETATFIN)reader["EtatFin"];
                    DateFin = (DateTime)reader["DateFin"];   
                    Type = (MODES)Enum.Parse(typeof(MODES), (string)reader["Type"]);
        }

        public static ObservableCollection<Historique> GetHistoriquesFromRedresseurId(int idRedresseur)
        {
            ObservableCollection<Historique> historiques = new ObservableCollection<Historique>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Historique(DateDebut, DateFin, IdRedresseur, Type, OrdreFabrication, EtatFin, IdUtilisateur) ", connection))
                { 
                Historique historique = new Historique();
                historique.DateDebut = DateTime.Now;
                historique.DateFin = DateTime.Now.AddDays(1);
                historique.IdRedresseur = 1;
                historique.Type = MODES.Connected;
                historique.OrdreFabrication = "DDD567";
                historique.EtatFin = ETATFIN.Arret_par_utilisateur;
                historique.IdUtilisateur = 1;
                historiques.Add(historique);
                }   
            }
            return historiques;
        }


        #endregion
    }
}
