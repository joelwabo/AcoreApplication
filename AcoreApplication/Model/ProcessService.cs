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
    public class ProcessService : IProcessService
    {
        public ObservableCollection<Process> GetAllData()
        {
            ObservableCollection<Process> result = new ObservableCollection<Process>();
            try
            {
                using (var bdd = new AcoreDBEntities())
                {
                    List<Process> processes = bdd.Process.ToList();
                    foreach (Process pro in processes)
                    {
                        pro.Recettes = RecetteService.GetListRecetteFromProcessId(pro.Id);
                        pro.Options = OptionsService.GetAllOptionsFromTableId(pro.Id, "Id" + pro.GetType().Name);
                        result.Add(pro);
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
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Process.Add(new DataService.Process()
                    {
                        Nom = "new_Process"
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

        public bool Update(Process process)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    List<DataService.Process> pro = bdd.Process.ToList();
                    DataService.Process processToUpdate = bdd.Process.FirstOrDefault(processFound => processFound.Id == process.Id);
                    if (processToUpdate != null)
                    {
                        processToUpdate.Nom = process.Nom;
                        processToUpdate.UMax = process.UMax;
                        processToUpdate.IMax = process.IMax;
                        processToUpdate.Pulse = process.Pulse;
                        processToUpdate.Inverseur = process.Inverseur;
                        processToUpdate.AH = process.AH;
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

        public bool Delete(Process process)
        {
            throw new NotImplementedException();
        }
    }
}
