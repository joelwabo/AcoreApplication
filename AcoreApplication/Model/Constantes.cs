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
        #region Constante 
        public const int Cst_SleepTime = 500;
        public const int Cst_PortModbus = 502;
        public const int Cst_NbRedresseurs = 10;
        public const int Cst_SlaveNb = 1;
        #endregion

        public enum MODES
        {
            //Automate mode
            Disconnected = -1,
            Connected,
            RemoteManuel,/*(Writing) Consigne Type, OnOff, Options
            (Reading) Lecture temperature 
            */
            RemoteRecette, /*(Writing) Consignes pour réaliser seg des recettes Type, OnOff, Options
            (Reading) Lecture temperature 
            */
            Supervision,

            //Redresseur mode
            LocalManuel, //(Reading) Consigne Lecture N°OF Type, Options
            LocalRecette, //(Reading) Consigne Lecture N°OF Type, Options, Info_Recette : seg, 
            Automatique
        }

        public enum OPTION
        {
            Statique,
            Mecanique,
            Pulse, //Quand on est on envoyer Ton TOff
            Temporisation, //envoie de de consigne en fonction de durée tempo
            Rampe, //envoie de consigne en fonction de durée de 0
            CompteurAH, //Calcul ou pas?
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

        public enum REGISTRE
        {
            Etat,
            MarcheArret,
            ConsigneV,
            ConsigneA,
            LectureV,
            LectureA,
            RetourOnOff,
            Defaut,
            Mode,
            MiseSousTension,
            ExistanceGroupe,
            OnOff,
            Nom,
            NumRecette,
            SegCours,
            NbSeg,
            NomRecette
        }

        public enum ETATFIN
        {
            Arret_par_utilisateur,
            Stopped_by_user,
            Arret_sur_défaut,
            Arret_sur_timer,
            Arret_A_H
        }

        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
