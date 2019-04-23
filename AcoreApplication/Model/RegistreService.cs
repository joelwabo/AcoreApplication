using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AcoreApplication.Model.Constantes;
using AcoreApplication.DataService;

namespace AcoreApplication.Model
{
    public class RegistreService : IRegistreService
    {
        public bool Delete(DataService.Registre registre)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<DataService.Registre> GetAllData()
        {
            ObservableCollection<Registre> result = new ObservableCollection<Registre>();
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    List<Registre> regs = bdd.Registre.ToList();
                    foreach (Registre reg in regs)
                    {
                        result.Add(reg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            return result;
        }

        public bool Insert()
        {
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    bdd.Registre.Add(new DataService.Registre()
                    {
                        IdRedresseur = 3,
                        Nom = "ConsigneA",
                        AdresseDebut = 2302,
                        AdresseFin = 2302,
                        Type = "Int",
                        NumBit = 1,
                        TypeModbus = "HoldingRegister"
                    });
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
        public bool Insert(DataService.Registre reg)
        {
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    bdd.Registre.Add(reg);
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

            public bool Update(Registre registre)
        {

            return true;
            //throw new NotImplementedException();
        }
    }
}
