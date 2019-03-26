using AcoreApplication.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    class OptionsService : IOptionsService
    {
        public ObservableCollection<Options> GetAllOptionsFromTableId(int id, string table)
        {
            ObservableCollection<Options> result = new ObservableCollection<Options>();
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    string sqlCommand = "SELECT * FROM Options  WHERE " + table + " = " + id;
                    List<Options> options = bdd.Options.SqlQuery(sqlCommand).ToList();
                    foreach (Options opt in options)
                        result.Add(opt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            return result;
        }

    }
}
