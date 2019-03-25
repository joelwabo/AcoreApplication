using AcoreApplication.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AcoreApplication.Model
{
    public class HistoriqueService : IHistoriqueService
    {
        public ObservableCollection<Historique> GetAllData()
        {
            ObservableCollection<Historique> result = new ObservableCollection<Historique>();
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    List<Historique> historiques = bdd.Historique.ToList();
                    foreach (Historique his in historiques)
                        result.Add(his);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            return result;
        }

        public bool InsertHistorique(Historique historique)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Historique.Add(historique);
                    bdd.HistoriqueData.AddRange(historique.HistoriqueData);
                    bdd.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return false;
            }
        }                
    }
}
