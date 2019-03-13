using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    public class RecetteService : IRecetteService
    {
        public bool DeleteRecette(Recette recette)
        {
            throw new NotImplementedException();
        }

        public bool InsertRecette()
        {
            try
            {
                using (var bdd = new DataBase.AcoreDBEntities())
                {
                    bdd.Recette.Add(new DataBase.Recette()
                    {
                        IdProcess = 1,
                        Nom = "new_recette",
                        Cyclage = 0,
                        SegCours = 0,
                        TempsRestant = new TimeSpan(0)
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

        public bool UpdateRecette(Recette recette)
        {
            try
            {
                using (var bdd = new DataBase.AcoreDBEntities())
                {
                    List<DataBase.Recette> pro = bdd.Recette.ToList();
                    DataBase.Recette recetteToUpdate = bdd.Recette.FirstOrDefault(recetteFound => recetteFound.Id == recette.Id);
                    if (recetteToUpdate != null)
                    {
                        recetteToUpdate.IdProcess = recette.IdProcess;
                        recetteToUpdate.Nom = recette.Nom;
                        recetteToUpdate.Cyclage = recette.Cyclage;
                        recetteToUpdate.SegCours = recette.SegCours;
                        bdd.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
