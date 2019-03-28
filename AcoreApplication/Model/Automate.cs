using System;
using NModbus;
using System.Net.Sockets;
using static AcoreApplication.Model.Constantes;
using System.Threading;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Ioc;
using System.Linq;

namespace AcoreApplication.DataService
{
    public partial class Automate 
    {
        #region ATTRIBUTS
        //public MODES Mode { get; set; }

        public ObservableCollection<Model.Redresseur> Redresseurs { get; set; }
        public ObservableCollection<Registre> Registres { get; set; }

        TcpClient ClientTcp { get; set; }
        IModbusMaster ModBusMaster { get; set; }

        private Thread ModbusPoolingTask { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)

        public void CreateThread()
        {
            Registres = Registre.GetAllRegisterFromRedresseurId(1);
            Redresseurs = new ObservableCollection<Model.Redresseur>();
            ClientTcp = new TcpClient();
            ModbusPoolingTask = new Thread(ModbusPooling);
            ModbusPoolingTask.Start();
        }

        ~Automate()
        {
            ModbusPoolingTask.Abort();
            Disconnect();
        }

        #endregion

        #region METHODES

        private void ModbusPooling(Object stateInfo)
        {
            while (true)
            {
                if (Mode == MODES.Connected.ToString())
                {
                    // read Etat, Read On/Off, Read Marche/Arret
                    foreach (Model.Redresseur redresseur in Redresseurs.ToList())
                    {
                        foreach (Registre registre in redresseur.Registres)
                        {
                            switch ((REGISTRE)Enum.Parse(typeof(REGISTRE), registre.Nom))
                            {
                                case REGISTRE.Defaut:
                                    {
                                        ushort[] Defaut = redresseur.ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                        redresseur.Defaut = Convert.ToBoolean(Defaut[0]);
                                    }
                                    break;
                                case REGISTRE.OnOff:
                                    /*{
                                        ushort[] OnOff = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                        bool onOff = Convert.ToBoolean(OnOff[0]);
                                        redresseur.OnOff = onOff;
                                    }*/
                                    break;
                                case REGISTRE.MarcheArret:
                                    {
                                        ushort[] MarcheArret = redresseur.ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                        redresseur.MiseSousTension = Convert.ToBoolean(MarcheArret[0]);
                                    }
                                    break;
                                case REGISTRE.Etat:
                                    {
                                        ushort[] Etat = redresseur.ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                        string etat = Etat[0].ToString();
                                        redresseur.Etat = (MODES)Enum.Parse(typeof(MODES), etat);
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                    Connection();
                Thread.Sleep(Cst_SleepTime);
            }
        }
        
        public void DeleteUnexistingGroupe()
        {
            foreach (Registre registre in Registres)
            {
                if ((REGISTRE)Enum.Parse(typeof(REGISTRE), registre.Nom) == REGISTRE.ExistenceGroupe)
                {
                    bool[] ExistenceGroupe = ModBusMaster.ReadCoils(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), 10);
                    for (int i = 0; i < ExistenceGroupe.Count(); i++)
                        if (!ExistenceGroupe[i])
                        {
                            SimpleIoc.Default.GetInstance<Model.IRedresseurService>().Delete(i+1);
                        }
                    break;
                }
            }
        }
        
        public void Connection()
        {
            try
            {
                ClientTcp.Connect(IpAdresse, Cst_PortModbus); 
                var factory = new ModbusFactory();
                ModBusMaster = factory.CreateMaster(ClientTcp);
                DeleteUnexistingGroupe();
                Redresseurs = Model.Redresseur.GetAllRedresseurFromAutotameId(IpAdresse);
                foreach (Model.Redresseur redresseur in Redresseurs)
                    redresseur.ModBusMaster = ModBusMaster;
                Mode = MODES.Connected.ToString();
            }
            catch (ArgumentNullException e)
            {
                Mode = MODES.Disconnected.ToString();
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Mode = MODES.Disconnected.ToString();
                Console.WriteLine("SocketException: {0}", e);
            }         
        }

        public void Disconnect()
        {
            if (ClientTcp.Connected)
            {
                Mode = MODES.Disconnected.ToString();
                ModbusPoolingTask.Abort();
                ModBusMaster.Dispose();
                ClientTcp.Close();
                Redresseurs = null;
            }
        }
        #endregion
    }
}
