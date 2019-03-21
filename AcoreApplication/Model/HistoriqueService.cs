using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class HistoriqueService : IHistoriqueService
    {
        public ObservableCollection<Historique> GetAllData()
        {
            ObservableCollection<Historique> result = new ObservableCollection<Historique>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Historique", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Historique historique = new Historique(reader);
                        result.Add(historique);
                    }
                    reader.Close();
                }
            }

            return result;
        }

        public bool InsertHistorique()
        {
            try
            {
                using (var bdd = new DataBase.AcoreDBEntities())
                {
                    bdd.Historique.Add(new DataBase.Historique()
                    {
                        IdRedresseur = 1,
                        IdUtilisateur = 1,
                        OrdreFabrication = "",
                        Date = new DateTime(),
                        DateFin = new DateTime(),
                        EtatFin = "",
                        Type = ""
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

        public bool UpdateHistorique(Historique historique)
        {
            try
            {
                using (var bdd = new DataBase.AcoreDBEntities())
                {
                    List<DataBase.Historique> hist = bdd.Historique.ToList();
                    DataBase.Historique historiqueToUpdate = bdd.Historique.FirstOrDefault(historiqueFound => historiqueFound.IdRedresseur == historique.IdRedresseur);
                    if (historiqueToUpdate != null)
                    {
                        historiqueToUpdate.IdRedresseur = historique.IdRedresseur;
                        historiqueToUpdate.IdUtilisateur = historique.IdUtilisateur;
                        historiqueToUpdate.OrdreFabrication = historique.OrdreFabrication;
                        historiqueToUpdate.Date = historique.DateDebut;
                        historiqueToUpdate.DateFin = historique.DateFin;
                        historiqueToUpdate.EtatFin = historique.EtatFin.ToString();
                        historiqueToUpdate.Type = historique.Type.ToString();
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
