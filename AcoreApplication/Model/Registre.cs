using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.DataService
{
    public partial class Registre
    {
        #region ATTRIBUTS

        #endregion 

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Registre()
        {
        }

        public Registre(int id)
        {
            this.IdRedresseur = id;
        }

        #endregion 

        #region METHODES
        public static ObservableCollection<Registre> GetAllRegisterFromRedresseurId(int id)
        {
            ObservableCollection<Registre> registres = new ObservableCollection<Registre>();
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    List<Registre> regs = bdd.Registre.Where(reg => reg.IdRedresseur == id).ToList();
                    foreach (Registre reg in regs)
                        registres.Add(reg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            return registres;
        }

        //Alert this should not be here
        public static void insert(ObservableCollection<Registre> registres) {
            using (var bdd = new AcoreDBEntities())
            {
                Registre x = bdd.Registre.OrderByDescending(r => r.Id).FirstOrDefault();
                foreach (Registre reg in registres) {
               
                    if (reg.Id == 0)
                    {
                        


                        bdd.Registre.Add(new Registre() {

                            IdRedresseur =  reg.IdRedresseur,
                            Nom = reg.Nom,
                            Type = reg.Type,
                            TypeModbus = reg.TypeModbus,
                            NumBit = reg.NumBit,
                            AdresseDebut = reg.AdresseDebut,
                            AdresseFin= reg.AdresseFin
                        });
                            
                        bdd.SaveChanges();
                    }
                    else
                    {
                        //TODO:UPDATE

                    }
                    

                }

            }
            
        }
        #endregion
    }
}
