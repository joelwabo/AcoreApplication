using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class Option
    {
        #region ATTRIBUTS
        public int Id { get; set; }
        OPTION Nom { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Option()
        {
        }

        public Option(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Nom = (OPTION)Enum.Parse(typeof(OPTION), (string)reader["Nom"]);
        }
        #endregion

        #region METHODES
        public static ObservableCollection<Option> GetAllOptionsFromTableId(int id, string table)
        {
            ObservableCollection<Option> options = new ObservableCollection<Option>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Options  WHERE " + table + " = " + id, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Option option = new Option(reader);
                        options.Add(option);
                    }
                }
            }
            return options;
        }

        #endregion
    }
}