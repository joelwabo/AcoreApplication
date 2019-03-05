using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcoreApplication.Model
{
    public class Constantes
    {
        public enum MODES
        {
            Recette,
            Remote,
            Manuel,
            Lecture,
            CommandeLocal, 
            LocalDistance,
            Supervision
        }

        public enum OPTION
        {
            Statique,
            Mecanique,
            Pulse,
            Temporisation,
            Rampe,
            CompteurAH,
            Cyclage,
            Prevent,
            MesureTemperature
        }

        public enum CALIBRE
        {
            A_H,
            A_S,
            A_MN
        }

        public enum TYPEREDRESSEUR
        {
            Anodique,
            Cathodique
        }

        public enum TYPEMODBUS
        {
            HoldingRegister,
            InputRegister,
            CoilRegister
        }

        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
