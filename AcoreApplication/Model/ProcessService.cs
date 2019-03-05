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
    public class ProcessService : IProcessService
    {
        public ObservableCollection<Process> GetAllData()
        {
            ObservableCollection<Process> result = new ObservableCollection<Process>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Process", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Process process = new Process(reader);
                        result.Add(process);
                    }
                    reader.Close();
                }
            }

            return result;
        }
    }
}
