using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class AutomateService : IAutomateService
    {
        
        public ObservableCollection<Automate> GetAllData()
        {
            ObservableCollection<Automate> result = new ObservableCollection<Automate>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Automate", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Automate automate = new Automate(reader);
                        result.Add(automate);
                    }
                }
            }

            return result;
        }
    }
}
