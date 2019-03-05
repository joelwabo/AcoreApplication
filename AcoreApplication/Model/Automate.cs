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
        #region Constante 
        private const int Cst_PortModbus = 502;
        private const ushort Cst_OffsetLong = 0x7000;
        private const int Cst_NbRedresseurs = 10;
        private const int Cst_SlaveNb = 1;
        private const int Cst_LatenceMinPooling = 500;//en millieconde
        #endregion

        #region ATTRIBUTS
        public int Id { get; set; }
        public MODES Mode { get; set; }
        public string IpAdresse { get; set; }

        public ObservableCollection<Redresseur> Redresseurs { get; set; }
        IModbusMaster ModBusMaster { get; set; }
        TcpClient ClientTcp { get; set; }
        private Timer _tacheModbus = null;
        private Timer _tacheModbus2 = null;
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
        }

        #endregion

        #region METHODES
        public void StartRecipe()
        {

            if (_tacheModbus2 is null)
            {
                _tacheModbus2 = new Timer(LoopWriting, new AutoResetEvent(false), 500, Cst_LatenceMinPooling);
            }
        }

        private void LoopWriting(Object stateInfo)
        {
            /*try
            {
                Connection();
                ushort[] readConsigneV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseConsigneV), Cst_NbRedresseurs);
                ushort[] readConsigneA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseConsigneA), Cst_NbRedresseurs);

                foreach (Redresseur redresseur in Redresseurs)
                {
                    if (readConsigneV[Redresseurs.IndexOf(redresseur)] < redresseur.ConsigneV)
                    {
                        // Writre Consigne, Lecture, OnOff, Defau
                        ushort[] writeConsigneV = new ushort[Cst_NbRedresseurs];
                        ushort[] writeConsigneA = new ushort[Cst_NbRedresseurs];
                        writeConsigneV[Redresseurs.IndexOf(redresseur)] = Convert.ToUInt16((readConsigneV[Redresseurs.IndexOf(redresseur)] + 1) & 0xFF);
                        writeConsigneA[Redresseurs.IndexOf(redresseur)] = Convert.ToUInt16((readConsigneV[Redresseurs.IndexOf(redresseur)] + 1) & 0xFF00);

                        ModBusMaster.WriteMultipleRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseConsigneV), writeConsigneV);
                        ModBusMaster.WriteMultipleRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseConsigneA), writeConsigneA);

                    }
                }
            }
            catch (Exception ex)
            {
                _tacheModbus.Dispose();
                _tacheModbus = null;
                ModBusMaster = null;
                ClientTcp.Close();
                ClientTcp = null;
                MessageBox.Show("Pool Modbus Error :\n" + ex.Message);
            }*/

        }

        private void ModbusPooling(Object stateInfo)
        {
            /*try
            {
                Connection();
                // read Consigne, Lecture, OnOff, Defaut
                ushort[] readConsigneV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseConsigneV), Cst_NbRedresseurs);
                ushort[] readConsigneA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseConsigneA), Cst_NbRedresseurs);
                ushort[] readLectureV = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseLectureV), Cst_NbRedresseurs);
                ushort[] readLectureA = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseLectureA), Cst_NbRedresseurs);
                ushort[] readOnOff = ModBusMaster.ReadHoldingRegisters(1, Convert.ToUInt16(AdresseOnOff), Cst_NbRedresseurs);
                ushort[] readDefaut = ModBusMaster.ReadHoldingRegisters(1, Convert.ToUInt16(AdresseDefaut), Cst_NbRedresseurs);
                ushort[] readNumRecette = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseNumRecette), Cst_NbRedresseurs);
                ushort[] readSegCours = ModBusMaster.ReadHoldingRegisters(Cst_SlaveNb, Convert.ToUInt16(AdresseSegCours), Cst_NbRedresseurs);
                ushort[] readNbSeg = ModBusMaster.ReadHoldingRegisters(1, Convert.ToUInt16(AdresseNbSeg), Cst_NbRedresseurs);
                ushort[] readNomRecette = ModBusMaster.ReadHoldingRegisters(1, Convert.ToUInt16(AdresseNomRecette), Cst_NbRedresseurs);

                foreach (Redresseur redresseur in Redresseurs)
                {
                    redresseur.ConsigneV = readConsigneV[Redresseurs.IndexOf(redresseur)];
                    redresseur.ConsigneA = readConsigneA[Redresseurs.IndexOf(redresseur)];
                    redresseur.LectureV = readLectureV[Redresseurs.IndexOf(redresseur)];
                    redresseur.LectureA = readLectureA[Redresseurs.IndexOf(redresseur)];
                    redresseur.NumRecette = readNumRecette[Redresseurs.IndexOf(redresseur)];
                    redresseur.SegCours = readSegCours[Redresseurs.IndexOf(redresseur)];
                    redresseur.NbSeg = readNbSeg[Redresseurs.IndexOf(redresseur)];
                    redresseur.Nom = readNomRecette[Redresseurs.IndexOf(redresseur)].ToString();
                    if (readOnOff[Redresseurs.IndexOf(redresseur)] == 1)
                        redresseur.OnOff = true;
                    else
                        redresseur.OnOff = false;
                    if (readDefaut[Redresseurs.IndexOf(redresseur)] == 1)
                        redresseur.Defaut = true;
                    else
                        redresseur.Defaut = false;
                    if (redresseur.ValuesA.Count < 300)
                    {
                        redresseur.ValuesA.Add(redresseur.ConsigneV);
                        redresseur.ValuesB.Add(redresseur.ConsigneA);
                    }
                    else
                    {
                        for (int i = 0; i < redresseur.ValuesA.Count; i++)
                        {
                            redresseur.ValuesA[i] = redresseur.ValuesA[i + 1];
                            redresseur.ValuesB[i] = redresseur.ValuesB[i + 1];
                        }
                        redresseur.ValuesA[redresseur.ValuesA.Count - 1] = redresseur.ConsigneV;
                        redresseur.ValuesB[redresseur.ValuesB.Count - 1] = redresseur.ConsigneA;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pool Modbus Error :\n" + ex.Message);
                _tacheModbus.Dispose();
                _tacheModbus = null;
                ModBusMaster = null;
                ClientTcp.Close();
                ClientTcp = null;
            }*/
        }

        public void StartModbusService()
        {
            if (_tacheModbus is null)
            {
                _tacheModbus = new Timer(ModbusPooling, new AutoResetEvent(false), 1000, Cst_LatenceMinPooling);
            }
        }

        public void StopModbusService()
        {
            foreach (Redresseur redresseur in Redresseurs)
            {
                redresseur.OnOff = false;
                redresseur.MiseSousTension = false;
            }
            if (!(_tacheModbus is null))
            {
                _tacheModbus.Dispose();
                _tacheModbus = null;
                ModBusMaster = null;
                ClientTcp.Close();
                ClientTcp = null;
            }
        }

        public void Connection()
        {
            if (ClientTcp == null)
            {
                ClientTcp = new TcpClient(IpAdresse, Cst_PortModbus);
                var factory = new ModbusFactory();
                ModBusMaster = factory.CreateMaster(ClientTcp);
            }
        }

        #endregion
    }
}
