using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Recette.Add(new DataService.Recette()
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
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    List<DataService.Recette> pro = bdd.Recette.ToList();
                    DataService.Recette recetteToUpdate = bdd.Recette.FirstOrDefault(recetteFound => recetteFound.Id == recette.Id);
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

        public static ObservableCollection<Recette> GetListRecetteFromProcessId(int idProcess)
        {
            ObservableCollection<Recette> result = new ObservableCollection<Recette>();
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    List<DataService.Recette> recettes = bdd.Recette.Where(rec => rec.IdProcess == idProcess).ToList();
                    foreach (DataService.Recette rec in recettes)
                        result.Add(new Recette(rec));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return result;
            }
            return result;
        }

        public static Recette GetRecetteFromId(int id)
        {
            Recette result = new Recette();
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    DataService.Recette recette = bdd.Recette.Where(rec => rec.Id == id).First();
                    result = new Recette(recette);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return result;
            }
            return result;
        }
    }
}
