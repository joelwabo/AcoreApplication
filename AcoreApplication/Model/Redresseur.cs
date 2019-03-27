using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using NModbus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using static AcoreApplication.Model.Constantes;
using GalaSoft.MvvmLight.Messaging;
using AcoreApplication.DataService;
using GalaSoft.MvvmLight.Ioc;

namespace AcoreApplication.Model
{
    public class Redresseur : ObservableObject
    {
        #region Constante 
        private const int Cst_PortModbus = 502;
        private const int Cst_NbRedresseurs = 10;
        private const int Cst_SlaveNb = 1;
        private const int Cst_SleepTime = 1000;
        #endregion

        #region ATTRIBUTS
        private int id;
        public int Id
        {
            get { return id; }
            set { NotifyPropertyChanged(ref id, value); }
        }
        private int idProcess;
        public int IdProcess
        {
            get { return idProcess; }
            set { NotifyPropertyChanged(ref idProcess, value); }
        }
        private string ipAdresse;
        public string IpAdresse
        {
            get { return ipAdresse; }
            set { NotifyPropertyChanged(ref ipAdresse, value); }
        }
        private bool onOff;
        public bool OnOff
        {
            get { return onOff; }
            set
            {
                NotifyPropertyChanged(ref onOff, value);
                if (OnOff)
                {
                    if (Etat == MODES.RemoteRecette)
                    {
                        if (SelectedRecette != null)
                        {
                            SelectedRecette.TempsDebut = DateTime.Now;
                        }
                    }
                    Historique = new Historique();
                    Historique.DateDebut = DateTime.Now;
                    Historique.IdUtilisateur = 1;
                    Historique.IdRedresseur = Id;
                    if ((Etat == MODES.LocalRecette) || (Etat == MODES.RemoteRecette))
                    {
                        Historique.IdRecette = SelectedRecette.Id;
                        //Historique.Recette = SelectedRecette;
                    }
                    Historique.OrdreFabrication = OrdreFabrication;
                    Historique.EtatFin = ETATFIN.Finish.ToString();
                    Historique.Type = Etat.ToString();

                }
                else
                {
                    if(Historique!=null)
                    {
                        Historique.DateFin = DateTime.Now;
                        SimpleIoc.Default.GetInstance<IHistoriqueService>().InsertHistorique(Historique);
                        Historique = null;
                    }
                }
            }
        }
        private bool miseSousTension;
        public bool MiseSousTension
        {
            get { return miseSousTension; }
            set { NotifyPropertyChanged(ref miseSousTension, value); }
        }
        private MODES etat;
        public MODES Etat
        {
            get { return etat; }
            set { NotifyPropertyChanged(ref etat, value); }
        }
        private TYPEREDRESSEUR type;
        public TYPEREDRESSEUR Type
        {
            get { return type; }
            set { NotifyPropertyChanged(ref type, value); }
        }
        private float calibreV;
        public float CalibreV
        {
            get { return calibreV; }
            set { NotifyPropertyChanged(ref calibreV, value); }
        }
        private int uMax;
        public int UMax
        {
            get { return uMax; }
            set { NotifyPropertyChanged(ref uMax, value); }
        }
        private float calibreA;
        public float CalibreA
        {
            get { return calibreA; }
            set { NotifyPropertyChanged(ref calibreA, value); }
        }
        private int iMax;
        public int IMax
        {
            get { return iMax; }
            set { NotifyPropertyChanged(ref iMax, value); }
        }
        private int consigneV;
        public int ConsigneV
        {
            get { return consigneV; }
            set { NotifyPropertyChanged(ref consigneV, value); }
        }
        private int consigneA;
        public int ConsigneA
        {
            get { return id; }
            set { NotifyPropertyChanged(ref consigneA, value); }
        }
        private int? lectureV;
        public int? LectureV
        {
            get { return lectureV; }
            set { NotifyPropertyChanged(ref lectureV, value); }
        }
        private int? lectureA;
        public int? LectureA
        {
            get { return lectureA; }
            set { NotifyPropertyChanged(ref lectureA, value); }
        }
        private int? temperature;
        public int? Temperature
        {
            get { return temperature; }
            set { NotifyPropertyChanged(ref temperature, value); }
        }
        private bool? aH;
        public bool? AH
        {
            get { return aH; }
            set { NotifyPropertyChanged(ref aH, value); }
        }
        private int? compteurAH;
        public int? CompteurAH
        {
            get { return compteurAH; }
            set { NotifyPropertyChanged(ref compteurAH, value); }
        }
        private CALIBRE calibreAH;
        public CALIBRE CalibreAH
        {
            get { return calibreAH; }
            set { NotifyPropertyChanged(ref calibreAH, value); }
        }
        private bool? inverseur;
        public bool? Inverseur
        {
            get { return inverseur; }
            set { NotifyPropertyChanged(ref inverseur, value); }
        }
        private bool? prevent;
        public bool? Prevent
        {
            get { return prevent; }
            set { NotifyPropertyChanged(ref prevent, value); }
        }
        private bool? pulse;
        public bool? Pulse
        {
            get { return pulse; }
            set { NotifyPropertyChanged(ref pulse, value); }
        }
        private bool? temporisation;
        public bool? Temporisation
        {
            get { return temporisation; }
            set { NotifyPropertyChanged(ref temporisation, value); }
        }
        private int? tempsOn;
        public int? TempsOn
        {
            get { return tempsOn; }
            set { NotifyPropertyChanged(ref tempsOn, value); }
        }
        private int? tempsOff;
        public int? TempsOff
        {
            get { return tempsOff; }
            set { NotifyPropertyChanged(ref tempsOff, value); }
        }
        private DateTime dureeTempo;
        public DateTime DureeTempo
        {
            get { return dureeTempo; }
            set { NotifyPropertyChanged(ref dureeTempo, value); }
        }
        private DateTime dureeRestante;
        public DateTime DureeRestante
        {
            get { return dureeRestante; }
            set { NotifyPropertyChanged(ref dureeRestante, value); }
        }
        private bool? rampe;
        public bool? Rampe
        {
            get { return rampe; }
            set { NotifyPropertyChanged(ref rampe, value); }
        }
        private DateTime dureeRampe;
        public DateTime DureeRampe
        {
            get { return dureeRampe; }
            set { NotifyPropertyChanged(ref dureeRampe, value); }
        }
        private string fichierIncident;
        public string FichierIncident
        {
            get { return fichierIncident; }
            set { NotifyPropertyChanged(ref fichierIncident, value); }
        }
        private bool defaut;
        public bool Defaut
        {
            get { return defaut; }
            set { NotifyPropertyChanged(ref defaut, value); }
        }

        private string ordreFabrication;
        public string OrdreFabrication
        {
            get { return ordreFabrication;  }
            set { NotifyPropertyChanged(ref ordreFabrication, value);  }
        }

        private ETATFIN etatFin;
        public ETATFIN EtatFin
        {
            get { return etatFin;  }
            set { NotifyPropertyChanged(ref etatFin, value); }
        }

        private ObservableCollection<int> tab;
        public ObservableCollection<int> Tab
        {
            get { return tab; }
            set { NotifyPropertyChanged(ref tab, value); }
        }

        public ChartValues<double> valuesA;
        public ChartValues<double> ValuesA
        {
            get { return valuesA; }
            set { NotifyPropertyChanged(ref valuesA, value); }
        }
        public ChartValues<double> valuesB;
        public ChartValues<double> ValuesB
        {
            get { return valuesB; }
            set { NotifyPropertyChanged(ref valuesB, value); }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public ObservableCollection<DataService.Options> options;
        public ObservableCollection<DataService.Options> Options
        {
            get { return options; }
            set { NotifyPropertyChanged(ref options, value); }
        }
        private ObservableCollection<Registre> registres;
        public ObservableCollection<Registre> Registres
        {
            get { return registres; }
            set { NotifyPropertyChanged(ref registres, value); }
        }
        private Historique historiques = null;
        public Historique Historique
        {
            get { return historiques; }
            set { NotifyPropertyChanged(ref historiques, value); }
        }

        private ObservableCollection<Recette> listRecette;
        public ObservableCollection<Recette> ListRecette
        {
            get { return listRecette; }
            set { NotifyPropertyChanged(ref listRecette, value); }
        }
        public Recette SelectedRecette = null;
        public IModbusMaster ModBusMaster { get; set; }
        private Thread RedresseurPoolingTask { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Redresseur()
        {

        }

        public Redresseur(DataService.Redresseur red)
        {
            ValuesA = new ChartValues<double> { 0 };
            ValuesB = new ChartValues<double> { 0 };
            Id = red.Id;
            IdProcess = red.IdProcess;
            IpAdresse = red.IpAdresse;
            OnOff = red.OnOff;
            MiseSousTension = red.MiseSousTension;
            Etat = (MODES)Enum.Parse(typeof(MODES), red.Etat);
            Type = (TYPEREDRESSEUR)Enum.Parse(typeof(TYPEREDRESSEUR), red.Type);
            UMax = red.UMax;
            IMax = red.IMax;
            ConsigneV = red.ConsigneV;
            ConsigneA = red.ConsigneA;
            LectureV = red.LectureV;
            LectureA = red.LectureA;
            Temperature = red.Temperature;
            AH = red.AH;
            CompteurAH = red.CompteurAH;
            CalibreAH = (CALIBRE)Enum.Parse(typeof(CALIBRE), red.CalibreAH);
            Pulse = red.Pulse;
            Temporisation = red.Temporisation;
            TempsOn = red.TempsOn;
            TempsOff = red.TempsOff;
            DureeTempo = DateTime.Parse(red.DureeTempo.ToString());
            DureeRestante = DateTime.Parse(red.DureeRestante.ToString());
            Rampe = red.Rampe;
            DureeRampe = DateTime.Parse(red.DureeRampe.ToString());
            Defaut = red.Defaut;

            Options = OptionsService.GetAllOptionsFromTableId(Id, "Id" + this.GetType().Name);
            Registres = Registre.GetAllRegisterFromRedresseurId(Id);
            ListRecette = RecetteService.GetListRecetteFromProcessId(IdProcess);
            RedresseurPoolingTask = new Thread(RedresseurPooling);
            RedresseurPoolingTask.Start();
        }

        public Redresseur(SqlDataReader reader)
        {
            ValuesA = new ChartValues<double> { 0 };
            ValuesB = new ChartValues<double> { 0 };
            Id = (int)reader["Id"];
            IdProcess = (int)reader["IdProcess"];
            IpAdresse = (string)reader["IpAdresse"];
            OnOff = (bool)reader["OnOff"];
            MiseSousTension = (bool)reader["MiseSousTension"];
            Etat = (MODES)Enum.Parse(typeof(MODES), (string)reader["Etat"]);
            Type = (TYPEREDRESSEUR)Enum.Parse(typeof(TYPEREDRESSEUR), (string)reader["Type"]);
            UMax = (int)reader["UMax"];
            IMax = (int)reader["IMax"];
            ConsigneV = (int)reader["ConsigneV"];
            ConsigneA = (int)reader["ConsigneA"];
            LectureV = (int)reader["LectureV"];
            LectureA = (int)reader["LectureA"];
            Temperature = (int)reader["Temperature"];
            AH = (bool)reader["AH"];
            CompteurAH = (int)reader["CompteurAH"];
            CalibreAH = (CALIBRE)Enum.Parse(typeof(CALIBRE), (string)reader["CalibreAH"]);
            Pulse = (bool)reader["Pulse"];
            Temporisation = (bool)reader["Temporisation"];
            TempsOn = (int)reader["TempsOn"];
            TempsOff = (int)reader["TempsOff"];
            DureeTempo = DateTime.Parse(reader["DureeTempo"].ToString());
            DureeRestante = DateTime.Parse(reader["DureeRestante"].ToString());
            Rampe = (bool)reader["Rampe"];
            DureeRampe = DateTime.Parse(reader["DureeRampe"].ToString());
            Defaut = (bool)reader["Defaut"];
            //OrdreFabrication = (string)reader["OrdreFabrication"];
            //EtatFin = (ETATFIN)Enum.Parse(typeof(ETATFIN), (string)reader["EtatFin"]);

            Options = OptionsService.GetAllOptionsFromTableId(Id, "Id" + this.GetType().Name);
            Registres = Registre.GetAllRegisterFromRedresseurId(Id);
            ListRecette = RecetteService.GetListRecetteFromProcessId(IdProcess);

            RedresseurPoolingTask = new Thread(RedresseurPooling);
            RedresseurPoolingTask.Start();
        }

        #endregion

        #region METHODES

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public static ObservableCollection<Redresseur> GetAllRedresseurFromAutotameId(string ipAdresse)
        {
            ObservableCollection<Redresseur> redresseurs = new ObservableCollection<Redresseur>();
            using (SqlConnection connection = new SqlConnection(CnnVal("AcoreDataBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Redresseur WHERE IpAdresse = N'" + ipAdresse + "'", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Redresseur redresseur = new Redresseur(reader);
                        redresseurs.Add(redresseur);
                    }
                }
            }
            return redresseurs;
        }

        private void RedresseurPooling()
        {
            while (true)
            {
                if (OnOff)
                {
                    HistoriqueData tempData = new HistoriqueData();
                    switch (Etat)
                    {
                        case MODES.LocalManuel:
                            LocaleManuel();
                            break;
                        case MODES.LocalRecette:
                            LocaleRecette();
                            break;
                        case MODES.RemoteManuel:
                            RemoteManuel();
                            break;
                        case MODES.RemoteRecette:
                            RemoteRecette();
                            break;
                        case MODES.Supervision:
                            break;
                    }
                    tempData.IdHistorique = Historique.Id;
                    tempData.LectureA = LectureA;
                    tempData.LectureV = LectureV;
                    tempData.ConsigneV = ConsigneV;
                    tempData.ConsineA = ConsigneA;
                    Historique.HistoriqueData.Add(tempData);
                }
                Thread.Sleep(Cst_SleepTime/5);
            }
        }

        private void LocaleRecette()
        {
            foreach (Registre registre in Registres)
            {
                switch ((REGISTRE)Enum.Parse(typeof(MODES), registre.Nom))
                {
                    case REGISTRE.ConsigneA:
                        {
                            ushort[] readConsigneA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            ConsigneA = readConsigneA[0];
                        }
                        break;
                    case REGISTRE.ConsigneV:
                        {
                            ushort[] readConsigneV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            ConsigneV = readConsigneV[0];
                            if (ValuesA.Count < 500)
                                ValuesA.Add(ConsigneV);
                            else
                            {
                                for (int i = 0; i < ValuesA.Count - 1; i++)
                                    ValuesA[i] = ValuesA[i + 1];
                                ValuesA[ValuesA.Count - 1] = ConsigneV;
                            }
                        }
                        break;
                    case REGISTRE.LectureA:
                        {
                            ushort[] readLectureA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            LectureA = readLectureA[0];
                            if (ValuesB.Count < 500)
                                ValuesB.Add(ConsigneA);
                            else
                            {
                                for (int i = 0; i < ValuesA.Count - 1; i++)
                                    ValuesB[i] = ValuesB[i + 1];
                                ValuesB[ValuesA.Count - 1] = ConsigneA;
                            }
                        }
                        break;
                    case REGISTRE.LectureV:
                        {
                            ushort[] readLectureV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            LectureV = readLectureV[0];
                        }
                        break;
                }
            }

        }

        private void LocaleManuel()
        {
            foreach (Registre registre in Registres)
            {
                switch ((REGISTRE)Enum.Parse(typeof(MODES), registre.Nom))
                {
                    case REGISTRE.ConsigneA:
                        {
                            ushort[] readConsigneA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            ConsigneA = readConsigneA[0];
                        }
                        break;
                    case REGISTRE.ConsigneV:
                        {
                            ushort[] readConsigneV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            ConsigneV = readConsigneV[0];
                        }
                        break;
                    case REGISTRE.LectureA:
                        {
                            ushort[] readLectureA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            LectureA = readLectureA[0];
                        }
                        break;
                    case REGISTRE.LectureV:
                        {
                            ushort[] readLectureV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                            LectureV = readLectureV[0];
                        }
                        break;
                }
            }
        }
        
        private void RemoteManuel()
        {
            WriteModbus();
        }

        private void RemoteRecette()
        {
            if (SelectedRecette != null) //Consigne = coefDirect*t + consigneD
            {
                if (SelectedRecette.TempsDebut.Add(SelectedRecette.TempsRestant) >= DateTime.Now)
                {
                    if (SelectedRecette.TempsDebut.Add(SelectedRecette.Segments[SelectedRecette.SegCours].Duree) >= DateTime.Now)
                    {
                        TimeSpan t = DateTime.Now - SelectedRecette.TempsDebut;
                        float coefDirectA = (SelectedRecette.Segments[SelectedRecette.SegCours].ConsigneArriveeA - SelectedRecette.Segments[SelectedRecette.SegCours].ConsigneDepartA) / (float)SelectedRecette.Segments[SelectedRecette.SegCours].Duree.TotalSeconds;
                        float coefDirectV = (SelectedRecette.Segments[SelectedRecette.SegCours].ConsigneArriveeV - SelectedRecette.Segments[SelectedRecette.SegCours].ConsigneDepartV) / (float)SelectedRecette.Segments[SelectedRecette.SegCours].Duree.TotalSeconds;

                        ConsigneA = (int)(coefDirectA * (float)t.TotalSeconds + SelectedRecette.Segments[SelectedRecette.SegCours].ConsigneDepartA);
                        ConsigneV = (int)(coefDirectV * (float)t.TotalSeconds + SelectedRecette.Segments[SelectedRecette.SegCours].ConsigneDepartV);
                        if (ValuesA.Count < 500)
                        {
                            ValuesA.Add(ConsigneV);
                            ValuesB.Add(ConsigneA);
                        }
                        else
                        {
                            for (int i = 0; i < ValuesA.Count - 1; i++)
                            {
                                ValuesA[i] = ValuesA[i + 1];
                                ValuesB[i] = ValuesB[i + 1];
                                ValuesA[ValuesA.Count - 1] = ConsigneV;
                                ValuesB[ValuesA.Count - 1] = ConsigneA;
                            }
                        }
                    }
                    else
                    {
                        SelectedRecette.SegCours++;
                        if (SelectedRecette.SegCours >= SelectedRecette.Segments.Count)
                        {
                            SelectedRecette.SegCours--;
                            OnOff = false;
                            Messenger.Default.Send(SelectedRecette);
                        }
                    }
                }
                WriteModbus();
            }
        }

        private void WriteModbus()
        {
            foreach (Registre registre in Registres)
            {
                switch ((REGISTRE)Enum.Parse(typeof(MODES), registre.Nom))
                {
                    case REGISTRE.ConsigneA:
                        {
                            ModBusMaster.WriteSingleRegister(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), (ushort)ConsigneA);
                        }
                        break;
                    case REGISTRE.ConsigneV:
                        {
                            ModBusMaster.WriteSingleRegister(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), (ushort)ConsigneV);
                        }
                        break;
                    case REGISTRE.LectureA:
                        {
                            ushort[] readLectureA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), 1);
                            LectureA = readLectureA[0];
                        }
                        break;
                    case REGISTRE.LectureV:
                        {
                            ushort[] readLectureV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), 1);
                            LectureV = readLectureV[0];
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
