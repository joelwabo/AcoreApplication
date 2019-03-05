using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class Historique
    {
        #region ATTRIBUTS
        public DateTime DateDebut { get; set; }
        public int IdRedresseur { get; set; }
        public int IdUtilisateur  { get; set; }
        public string OrderFabrication { get; set; }
        public string EtatFin { get; set; }
        public DateTime DateFin { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Historique()
        {

        }

        public void GetHistoriqueFromDB(int id)
        {
            SqlConnection connection;
            using (connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Historique WHERE IdRedresseur = " + id, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IdRedresseur = (int)reader["IdRedresseur"];
                    IdUtilisateur = (int)reader["IdUtilisateur"];
                    DateDebut = (DateTime)reader["Date"];
                    OrderFabrication = (string)reader["OrdreFabrication"];
                    EtatFin = (string)reader["EtatFin"];
                    DateFin = (DateTime)reader["DateFin"];
                }
            }
        }
        #endregion
    }
}
