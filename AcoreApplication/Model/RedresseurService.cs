using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AcoreApplication.Model.Constantes;

namespace AcoreApplication.Model
{
    public class RedresseurService : IRedresseurService
    {
        public bool DeleteRedresseur(Redresseur redresseur)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    string sql = "DELETE From Redresseur  WHERE Id = " + redresseur.Id;
                    bdd.Redresseur.SqlQuery(sql);
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

        public ObservableCollection<Redresseur> GetAllData()
        {
            ObservableCollection<Redresseur> result = new ObservableCollection<Redresseur>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Redresseur", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Redresseur redresseur = new Redresseur(reader);
                        result.Add(redresseur);
                    }
                }
            }
            return result;
        }


        public bool InsertRedresseur()
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Redresseur.Add(new DataService.Redresseur()
                    {
                        IdProcess = 1,
                        IpAdresse = "192.168.1.111",
                        OnOff = false,
                        MiseSousTension = false,
                        Etat = "",
                        Type = "",
                        UMax = 0,
                        IMax = 0,
                        ConsigneV = 0,
                        ConsigneA = 0,
                        LectureV = 0,
                        LectureA = 0,
                        Temperature = 0,
                        AH = false,
                        CompteurAH = 0,
                        CalibreAH = "",
                        Pulse = false,
                        Temporisation = false,
                        TempsOn = 0,
                        TempsOff = 0,
                        DureeTempo = new TimeSpan(0),
                        DureeRestante = new TimeSpan(0),
                        Rampe = false,
                        DureeRampe = new TimeSpan(0),
                        Defaut = false
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

        public bool UpdateRedresseur(Redresseur redresseur)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    List<DataService.Redresseur> pro = bdd.Redresseur.ToList();
                    DataService.Redresseur redresseurToUpdate = bdd.Redresseur.FirstOrDefault(redresseurFound => redresseurFound.Id == redresseur.Id);
                    if (redresseurToUpdate != null)
                    {
                        redresseurToUpdate.IdProcess = redresseur.IdProcess;
                        redresseurToUpdate.IpAdresse = redresseur.IpAdresse;
                        redresseurToUpdate.OnOff = redresseur.OnOff;
                        redresseurToUpdate.MiseSousTension = redresseur.MiseSousTension;
                        redresseurToUpdate.Etat = redresseur.Etat.ToString();
                        redresseurToUpdate.Type = redresseur.Type.ToString();
                        redresseurToUpdate.UMax = redresseur.UMax;
                        redresseurToUpdate.IMax = redresseur.IMax;
                        redresseurToUpdate.ConsigneV = redresseur.ConsigneV;
                        redresseurToUpdate.ConsigneA = redresseur.ConsigneA;
                        redresseurToUpdate.LectureV = redresseur.LectureV;
                        redresseurToUpdate.LectureA = redresseur.LectureA;
                        redresseurToUpdate.Temperature = redresseur.Temperature;
                        redresseurToUpdate.AH = redresseur.AH;
                        redresseurToUpdate.CompteurAH = redresseur.CompteurAH;
                        redresseurToUpdate.CalibreAH = redresseur.CalibreAH.ToString();
                        redresseurToUpdate.Pulse = redresseur.Pulse;
                        redresseurToUpdate.Temporisation = redresseur.Temporisation;
                        redresseurToUpdate.TempsOn = redresseur.TempsOn;
                        redresseurToUpdate.TempsOff = redresseur.TempsOff;
                        //redresseurToUpdate.DureeTempo = DateTime.Parse(redresseur.DureeTempo.ToString());
                        //redresseurToUpdate.DureeRestante = DateTime.Parse(redresseur.DureeRestante.ToString());
                        redresseurToUpdate.Rampe = redresseur.Rampe;
                        //redresseurToUpdate.DureeRampe = DateTime.Parse(redresseur.DureeRampe.ToString());
                        redresseurToUpdate.Defaut = redresseur.Defaut;

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
