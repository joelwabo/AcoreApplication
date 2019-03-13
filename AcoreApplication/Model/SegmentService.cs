using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    class SegmentService : ISegmentService
    {

        public bool DeleteSegment(Segment segment)
        {
            throw new NotImplementedException();
        }

        public bool InsertSegment()
        {
            try
            {
                using (var bdd = new DataBase.AcoreDBEntities())
                {
                    bdd.Segment.Add(new DataBase.Segment()
                    {
                        Id = 2,
                        IdRecette = 1,
                        Nom = "new_seg",
                        Etat = false,
                        Type = "Anodique",
                        Duree = new TimeSpan(0),
                        ConsigneDepartV = 0,
                        ConsigneDepartA = 0,
                        ConsigneArriveeV = 0,
                        ConsigneArriveeA = 0,
                        TempsRestant = new TimeSpan(0),
                        Pulse = false,
                        CompteurAH = 0,
                        Temporisation = false,
                        TempsOn = new TimeSpan(0),
                        TempsOff = new TimeSpan(0),
                        AH = false,
                        //CompteurAH = 0,
                        CalibreAH = "A_H",
                        Rampe = false,
                        DureeRampe = new TimeSpan(0)
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

        public bool UpdateSegment(Segment segment)
        {
            try
            {
                using (var bdd = new DataBase.AcoreDBEntities())
                {
                    List<DataBase.Segment> seg = bdd.Segment.ToList();
                    DataBase.Segment segmentToUpdate = bdd.Segment.FirstOrDefault(segmentFound => segmentFound.Id == segment.Id);
                    if (segmentToUpdate != null)
                    {
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