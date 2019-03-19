using System;
using NModbus;
using System.Net.Sockets;
using System.Collections.Generic;
using static AcoreApplication.Model.Constantes;
using System.Data.SqlClient;
using System.Windows;
using System.Net;
using System.Threading;
using static AcoreApplication.Model.Redresseur;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AcoreApplication.Model
{
    public class Automate 
    {
        #region ATTRIBUTS
        public int Id { get; set; }
        public MODES Mode { get; set; }
        public string IpAdresse { get; set; }

        public ObservableCollection<Redresseur> Redresseurs { get; set; }

        TcpClient ClientTcp { get; set; }
        IModbusMaster ModBusMaster { get; set; }

        private Thread ModbusPoolingTask { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Automate()
        {
        }

        public Automate(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Mode = (MODES)Enum.Parse(typeof(MODES), (string)reader["Mode"]);
            IpAdresse = (string)reader["IpAdresse"];
            Redresseurs = GetAllRedresseurFromAutotameId(Id);

            ClientTcp = new TcpClient();
            Connection();
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
            while(Mode == MODES.Connected)
            {
                // read Etat, Read On/Off, Read Marche/Arret
                foreach (Redresseur redresseur in Redresseurs)
                {
                    foreach (Registre registre in redresseur.Registres)
                    {
                        switch(registre.Nom)
                        {
                            case REGISTRE.Defaut:
                                {
                                    ushort[] Defaut = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                    redresseur.Defaut = Convert.ToBoolean(Defaut[Redresseurs.IndexOf(redresseur)]);
                                }
                                break;
                            case REGISTRE.OnOff:
                                {
                                    ushort[] OnOff = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                    bool onOff = Convert.ToBoolean(OnOff[Redresseurs.IndexOf(redresseur)]);
                                    redresseur.OnOff = onOff;
                                }
                                break;
                            case REGISTRE.MarcheArret:
                                {
                                    ushort[] MarcheArret = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                    redresseur.MiseSousTension = Convert.ToBoolean(MarcheArret[Redresseurs.IndexOf(redresseur)]);
                                }
                                break;
                            case REGISTRE.Etat:
                                {
                                    ushort[] Etat = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(registre.AdresseDebut), Cst_NbRedresseurs);
                                    string etat = Etat[Redresseurs.IndexOf(redresseur)].ToString();
                                    redresseur.Etat = (MODES)Enum.Parse(typeof(MODES), etat);
                                }
                                break;
                        }
                    }
                }
                Thread.Sleep(Cst_SleepTime);
            }
        }
               
        public void Connection()
        {
            if (!ClientTcp.Connected)
            {
                try
                {
                    ClientTcp.Connect(IpAdresse, Cst_PortModbus);
                    var factory = new ModbusFactory();
                    ModBusMaster = factory.CreateMaster(ClientTcp);
                    Mode = MODES.Connected;

                    ModbusPoolingTask = new Thread(ModbusPooling);
                    ModbusPoolingTask.Start();
                    foreach (Redresseur redresseur in Redresseurs)
                        redresseur.ModBusMaster = ModBusMaster;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                finally
                {
                    Mode = MODES.Disconnected;
                }
            }
        }

        public void Disconnect()
        {
            if (ClientTcp.Connected)
            {
                ModbusPoolingTask.Abort();
                foreach (Redresseur redresseur in Redresseurs)
                    redresseur.ModBusMaster.Dispose();
                ModBusMaster.Dispose();
                ClientTcp.Close();
                Mode = MODES.Disconnected;
            }
        }
        #endregion
    }
}
