using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;
using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public class HistoriqueService : IHistoriqueService
    {
        SqlConnection connection;
        public ObservableCollection<Historique> GetAllData()
        {
            ObservableCollection<Historique> result = new ObservableCollection<Historique>();
            using (connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Historique", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Historique historique = new Historique();
                    result.Add(historique);
                }
                reader.Close();
            }

            return result;
        }
    }
}
