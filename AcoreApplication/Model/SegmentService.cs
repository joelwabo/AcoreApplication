using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    public class SegmentService : ISegmentService
    {

        public bool Delete(Segment segment)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Segment segment)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    bdd.Segment.Add(new DataService.Segment()
                    {
                        IdRecette = segment.IdRecette,
                        Nom = segment.Nom,
                        Etat = segment.Etat,
                        Type = segment.Type.ToString(),
                        Duree = segment.Duree,
                        ConsigneDepartV = segment.ConsigneDepartV,
                        ConsigneDepartA = segment.ConsigneDepartA,
                        ConsigneArriveeV = segment.ConsigneArriveeV,
                        ConsigneArriveeA = segment.ConsigneArriveeA,
                        TempsRestant = segment.TempsRestant,
                        Pulse = segment.Pulse,
                        CompteurAH = segment.CompteurAH,
                        Temporisation = segment.Temporisation,
                        TempsOn = segment.TempsOn,
                        TempsOff = segment.TempsOff,
                        AH = segment.AH,
                        //CompteurAH = CompteurAH,
                        CalibreAH = segment.CalibreAH.ToString(),
                        Rampe = segment.Rampe,
                        DureeRampe = segment.DureeRampe
                    });
                    bdd.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return false;
            }
        }

        public bool Update(Segment segment)
        {
            try
            {
                using (var bdd = new DataService.AcoreDBEntities())
                {
                    List<DataService.Segment> seg = bdd.Segment.ToList();
                    DataService.Segment segmentToUpdate = bdd.Segment.FirstOrDefault(segmentFound => segmentFound.Id == segment.Id);
                    if (segmentToUpdate != null)
                    {
                        segmentToUpdate.IdRecette = segment.IdRecette;
                        segmentToUpdate.Nom = segment.Nom;
                        segmentToUpdate.Etat = segment.Etat;
                        segmentToUpdate.Type = segment.Type.ToString();
                        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        segmentToUpdate.Duree = segment.Duree;
                        segmentToUpdate.ConsigneDepartV = segment.ConsigneDepartV;
                        segmentToUpdate.ConsigneDepartA = segment.ConsigneDepartA;
                        segmentToUpdate.ConsigneArriveeV = segment.ConsigneArriveeV;
                        segmentToUpdate.ConsigneArriveeA = segment.ConsigneArriveeA;
                        segmentToUpdate.TempsRestant = segment.TempsRestant;
                        segmentToUpdate.Pulse = segment.Pulse;
                        segmentToUpdate.CompteurAH = segment.CompteurAH;
                        segmentToUpdate.Temporisation = segment.Temporisation;
                        segmentToUpdate.TempsOn = segment.TempsOn;
                        segmentToUpdate.TempsOff = segment.TempsOff;
                        segmentToUpdate.AH = segment.AH;
                        //CompteurAH = CompteurAH;
                        segmentToUpdate.CalibreAH = segment.CalibreAH.ToString();
                        segmentToUpdate.Rampe = segment.Rampe;
                        segmentToUpdate.DureeRampe = segment.DureeRampe;

                        bdd.SaveChanges();
                    }
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