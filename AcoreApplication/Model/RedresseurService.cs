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
    public class RedresseurService : IRedresseurService
    {
        SqlConnection connection;

        public ObservableCollection<Redresseur> GetAllData()
        {
            ObservableCollection<Redresseur> result = new ObservableCollection<Redresseur>();
            /*using (connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Redresseur", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Redresseur redresseur = new Redresseur();
                    redresseur.GetRedresseurFromDB((int)reader["Id"]);
                    result.Add(redresseur);
                }
                reader.Close();
            }*/

            return result;
        }
    }
}
