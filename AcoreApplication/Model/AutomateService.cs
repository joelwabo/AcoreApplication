using AcoreApplication.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AcoreApplication.Model
{
    public class AutomateService 
    {        
        public static ObservableCollection<Automate> GetAllData()
        {
            ObservableCollection<Automate> result = new ObservableCollection<Automate>();
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    List<Automate> automates = bdd.Automate.ToList();
                    foreach (Automate aut in automates)
                    {
                        aut.CreateThread();
                        result.Add(aut);
                    }
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
