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
        public bool Delete(Registre registre)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Registre> GetAllData()
        {
            throw new NotImplementedException();
        }

        public bool Insert()
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Registre.Add(new DataService.Registre()
                    {
                        IdRedresseur = 3,
                        Nom = "ConsigneV",
                        AdresseDebut = 2292,
                        AdresseFin = 2292,
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
        public bool Insert(Registre reg)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Registre.Add(new DataService.Registre()
                    {
                        IdRedresseur = 3,
                        Nom = "ConsigneV",
                        AdresseDebut = 2292,
                        AdresseFin = 2292,
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

            public bool Update(Registre registre)
        {

            return true;
            //throw new NotImplementedException();
        }
    }
}
