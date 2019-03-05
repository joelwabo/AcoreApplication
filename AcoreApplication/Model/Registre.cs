using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class Registre
    {
        #region ATTRIBUTS
        public int Id { get; set; }
        public int IdRedresseur { get; set; }
        public string Nom { get; set; }
        public TYPEMODBUS TypeModbus { get; set; }
        public Type Type { get; set; }
        public int AdresseDebut { get; set; }
        public int AdresseFin { get; set; }
        public int NumBit { get; set; }

        #endregion 

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Registre()
        {

        }

        public Registre(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            IdRedresseur = (int)reader["IdRedresseur"];
            Nom = (string)reader["Nom"];
            Type = Type.GetType(reader["Type"].ToString());
            TypeModbus = (TYPEMODBUS)Enum.Parse(typeof(TYPEMODBUS), (string)reader["TypeModbus"]);
            AdresseDebut = (int)reader["AdresseDebut"];
            AdresseFin = (int)reader["AdresseFin"];
            NumBit = (int)reader["NumBit"];
        }

        #endregion 

        #region METHODES
        public static ObservableCollection<Registre> GetAllRegisterFromRedresseurId(int id)
        {
            ObservableCollection<Registre> registres = new ObservableCollection<Registre>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Registre  WHERE IdRedresseur = " + id, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Registre option = new Registre(reader);
                        registres.Add(option);
                    }
                }
            }
            return registres;
        }

        #endregion
    }
}
