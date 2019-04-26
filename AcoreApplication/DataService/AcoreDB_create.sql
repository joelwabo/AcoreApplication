-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2019-03-27 16:23:48.516

-- tables
-- Table: Automate
CREATE TABLE Automate (
    IpAdresse nvarchar(50)  NOT NULL,
    Mode nvarchar(50)  NOT NULL,
    CONSTRAINT Automate_pk PRIMARY KEY  (IpAdresse)
);

-- Table: Historique
CREATE TABLE Historique (
    Id int  NOT NULL IDENTITY,
    DateDebut datetime  NOT NULL DEFAULT 0,
    IdUtilisateur int  NOT NULL DEFAULT 1,
    IdRedresseur int  NOT NULL,
    IdRecette int  NULL,
    DateFin datetime  NULL,
    OrdreFabrication nvarchar(50)  NULL,
    EtatFin nvarchar(50)  NULL,
    Type nvarchar(50)  NULL,
    CONSTRAINT Historique_pk PRIMARY KEY  (Id)
);

-- Table: HistoriqueData
CREATE TABLE HistoriqueData (
    Id int  NOT NULL IDENTITY,
    IdHistorique int  NOT NULL,
    ConsigneV int  NULL DEFAULT 0,
    ConsigneA int  NULL DEFAULT 0,
    LectureV int  NULL DEFAULT 0,
    LectureA int  NULL DEFAULT 0,
    CONSTRAINT HistoriqueData_pk PRIMARY KEY  (Id)
);

-- Table: Options
CREATE TABLE Options (
    Id int  NOT NULL IDENTITY,
    IdRedresseur int  NOT NULL,
    IdSegment int  NOT NULL,
    IdRecette int  NOT NULL,
    IdProcess int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    CONSTRAINT Options_pk PRIMARY KEY  (Id)
);

-- Table: Process
CREATE TABLE Process (
    Id int  NOT NULL IDENTITY,
    Nom nvarchar(50)  NOT NULL,
    UMax int  NOT NULL,
    IMax int  NOT NULL,
    Pulse bit  NOT NULL DEFAULT 0,
    Inverseur bit  NOT NULL DEFAULT 0,
    AH bit  NOT NULL DEFAULT 0,
    CONSTRAINT Process_pk PRIMARY KEY  (Id)
);

-- Table: Recette
CREATE TABLE Recette (
    Id int  NOT NULL IDENTITY,
    IdProcess int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    Cyclage int  NULL,
    SegCours int  NULL,
    TempsRestant time(7)  NULL,
    CONSTRAINT id PRIMARY KEY  (Id)
);

-- Table: Redresseur
CREATE TABLE Redresseur (
    Id int  NOT NULL IDENTITY,
    IpAdresse nvarchar(50)  NOT NULL DEFAULT N'192.168.1.111',
    IdProcess int  NOT NULL,
    OnOff bit  NOT NULL DEFAULT 0,
    MiseSousTension bit  NOT NULL DEFAULT 0,
    Etat nvarchar(50)  NOT NULL,
    Type nvarchar(50)  NOT NULL,
    CalibreV float  NULL,
    UMax int  NOT NULL,
    CalibreA float  NULL,
    IMax int  NOT NULL,
    ConsigneV int  NOT NULL,
    ConsigneA int  NOT NULL,
    LectureV int  NULL,
    LectureA int  NULL,
    Temperature int  NULL,
    AH bit  NULL,
    CompteurAH int  NULL,
    CalibreAH nvarchar(50)  NULL,
    Inverseur bit  NULL DEFAULT 0,
    Prevent bit  NOT NULL DEFAULT 0,
    Pulse bit  NULL,
    TempsOn int  NULL,
    TempsOff int  NULL,
    Temporisation bit  NULL,
    DureeTempo time(7)  NULL,
    DureeRestante time(7)  NULL,
    Rampe bit  NULL,
    DureeRampe time(7)  NULL,
    FichierIncident binary(50)  NULL,
    Defaut bit  NOT NULL DEFAULT 0,
    CONSTRAINT Redresseur_pk PRIMARY KEY  (Id)
);

-- Table: Registre
CREATE TABLE Registre (
    Id int  NOT NULL IDENTITY,
    IdRedresseur int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    TypeModbus nvarchar(50)  NOT NULL,
    Type nvarchar(50)  NOT NULL,
    AdresseDebut int  NOT NULL,
    AdresseFin int  NOT NULL,
    NumBit int  NOT NULL,
    CONSTRAINT Registre_pk PRIMARY KEY  (Id)
);

-- Table: Segment
CREATE TABLE Segment (
    Id int  NOT NULL IDENTITY,
    IdRecette int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    Etat bit  NOT NULL,
    Type nvarchar(50)  NOT NULL,
    Duree time(7)  NOT NULL,
    ConsigneDepartV int  NOT NULL,
    ConsigneDepartA int  NOT NULL,
    ConsigneArriveeV int  NOT NULL,
    ConsigneArriveeA int  NOT NULL,
    TempsRestant time(7)  NULL,
    Pulse bit  NULL DEFAULT 0,
    Temporisation bit  NULL DEFAULT 0,
    TempsOn time(7)  NULL,
    TempsOff time(7)  NULL,
    AH bit  NULL DEFAULT 0,
    CompteurAH int  NULL,
    CalibreAH nvarchar(50)  NULL,
    Rampe bit  NULL DEFAULT 0,
    DureeRampe time(7)  NULL,
    CONSTRAINT Segment_pk PRIMARY KEY  (Id)
);

-- Table: Utilisateur
CREATE TABLE Utilisateur (
    Id int  NOT NULL IDENTITY,
    Nom nvarchar(50)  NOT NULL,
    Mdp nvarchar(50)  NOT NULL,
    CONSTRAINT Utilisateur_pk PRIMARY KEY  (Id)
);

-- foreign keys
-- Reference: HistoriqueData_Historique (table: HistoriqueData)
ALTER TABLE HistoriqueData ADD CONSTRAINT HistoriqueData_Historique
    FOREIGN KEY (IdHistorique)
    REFERENCES Historique (Id);

-- Reference: Historique_Recette (table: Historique)
ALTER TABLE Historique ADD CONSTRAINT Historique_Recette
    FOREIGN KEY (IdRecette)
    REFERENCES Recette (Id);

-- Reference: Historique_Redresseur (table: Historique)
ALTER TABLE Historique ADD CONSTRAINT Historique_Redresseur
    FOREIGN KEY (IdRedresseur)
    REFERENCES Redresseur (Id);

-- Reference: Historique_Utilisateur (table: Historique)
ALTER TABLE Historique ADD CONSTRAINT Historique_Utilisateur
    FOREIGN KEY (IdUtilisateur)
    REFERENCES Utilisateur (Id);

-- Reference: Option_Process (table: Options)
ALTER TABLE Options ADD CONSTRAINT Option_Process
    FOREIGN KEY (IdProcess)
    REFERENCES Process (Id);

-- Reference: Option_Recette (table: Options)
ALTER TABLE Options ADD CONSTRAINT Option_Recette
    FOREIGN KEY (IdRecette)
    REFERENCES Recette (Id);

-- Reference: Option_Redresseur (table: Options)
ALTER TABLE Options ADD CONSTRAINT Option_Redresseur
    FOREIGN KEY (IdRedresseur)
    REFERENCES Redresseur (Id);

-- Reference: Option_Segment (table: Options)
ALTER TABLE Options ADD CONSTRAINT Option_Segment
    FOREIGN KEY (IdSegment)
    REFERENCES Segment (Id);

-- Reference: Recette_Process (table: Recette)
ALTER TABLE Recette ADD CONSTRAINT Recette_Process
    FOREIGN KEY (IdProcess)
    REFERENCES Process (Id);

-- Reference: Redresseur_Automate (table: Redresseur)
ALTER TABLE Redresseur ADD CONSTRAINT Redresseur_Automate
    FOREIGN KEY (IpAdresse)
    REFERENCES Automate (IpAdresse);

-- Reference: Redresseur_Process (table: Redresseur)
ALTER TABLE Redresseur ADD CONSTRAINT Redresseur_Process
    FOREIGN KEY (IdProcess)
    REFERENCES Process (Id);

-- Reference: Registre_Redresseur (table: Registre)
ALTER TABLE Registre ADD CONSTRAINT Registre_Redresseur
    FOREIGN KEY (IdRedresseur)
    REFERENCES Redresseur (Id);

-- Reference: Segment_Recette (table: Segment)
ALTER TABLE Segment ADD CONSTRAINT Segment_Recette
    FOREIGN KEY (IdRecette)
    REFERENCES Recette (Id);

INSERT INTO [dbo].[Process] ([Nom], [UMax], [IMax], [Pulse], [Inverseur], [AH]) VALUES (N'Anodisation', 500, 500, 0, 0, 0)
INSERT INTO [dbo].[Process] ([Nom], [UMax], [IMax], [Pulse], [Inverseur], [AH]) VALUES (N'Cathodisation', 500, 500, 0, 0, 0)

INSERT INTO [dbo].[Recette] ([IdProcess], [Nom], [Cyclage], [SegCours], [TempsRestant]) VALUES (1, N'Rec1', 0, 1, N'0:0:59')
INSERT INTO [dbo].[Recette] ([IdProcess], [Nom], [Cyclage], [SegCours], [TempsRestant]) VALUES (1, N'Rec2', 2, 1, N'0:30:59')
INSERT INTO [dbo].[Recette] ([IdProcess], [Nom], [Cyclage], [SegCours], [TempsRestant]) VALUES (2, N'Rec3', 0, 1, N'12:30:59')

INSERT INTO [dbo].[Segment] ([IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (1, N'Seg1', 0,  N'Anodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')
INSERT INTO [dbo].[Segment] ([IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (1, N'Seg1', 0,  N'Cathodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')
INSERT INTO [dbo].[Segment] ([IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (2, N'Seg1', 0,  N'Anodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')
INSERT INTO [dbo].[Segment] ([IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (3, N'Seg1', 0,  N'Cathodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')

INSERT INTO [dbo].[Automate] ([IpAdresse],  [Mode]) VALUES (N'192.168.1.111', N'Disconnected')
INSERT INTO [dbo].[Automate] ([IpAdresse],  [Mode]) VALUES (N'192.168.1.112',  N'Disconnected')

INSERT INTO [dbo].[Utilisateur] ([Nom], [Mdp]) VALUES ( N'Ayan', N'laptop')

INSERT INTO [dbo].[Redresseur] ([IdProcess], [IpAdresse], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (1, N'192.168.1.111', 0, 0, N'RemoteManuel', N'Anodique', 1, 30, 1, 400, 0, 0, 0, 0, 1, 1, 1000, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([IdProcess], [IpAdresse], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (2, N'192.168.1.111', 0, 0, N'RemoteRecette', N'Cathodique', 1, 500, 1, 400, 0, 80, 0, 0, 1, 1, 200, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([IdProcess], [IpAdresse], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (2, N'192.168.1.111', 0, 0, N'LocalRecette', N'Anodique', 1, 0, 1, 0, 0, 90, 9, 0, 1, 0, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([IdProcess], [IpAdresse], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (1, N'192.168.1.112', 0, 0, N'LocalManuel', N'Cathodique', 1, 99999, 1, 65, 0, 2, 0, 0, 1, 0, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([IdProcess], [IpAdresse], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (1, N'192.168.1.112', 0, 0, N'Disconnected', N'Anodique', 1, 333, 1, 5555, 20, 0, 0, 0, 1, 1, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([IdProcess], [IpAdresse], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (1, N'192.168.1.112', 0, 0, N'Disconnected', N'Anodique', 1, 30, 0, 400, 0, 20, 1, 0, 1, 0, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)

INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'MiseSousTension', N'CoilRegister', N'Bit', 2230, 2230, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'RetourOnOff', N'CoilRegister', N'Bit', 2240, 2340, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'Defaut', N'CoilRegister', N'Bit', 2250, 2250, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'ExistenceGroupe', N'CoilRegister', N'Bit', 2280, 2280, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'ConsigneV', N'HoldingRegister', N'Int', 2290, 2290, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'ConsigneA', N'HoldingRegister', N'Int', 2300, 2300, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'LectureV', N'HoldingRegister', N'Int', 2310, 2310, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'LectureA', N'HoldingRegister', N'Int', 2320, 2320, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'Mode', N'HoldingRegister', N'int', 2350, 2350, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'OnOff', N'CoilRegister', N'Bit', 2300, 2300, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 1, N'Nom', N'HoldingRegister', N'int', 2400, 2404, 5)

INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'MiseSousTension', N'CoilRegister', N'Bit', 2231, 2230, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'RetourOnOff', N'CoilRegister', N'Bit', 2241, 2340, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'Defaut', N'CoilRegister', N'Bit', 2251, 2251, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'ExistenceGroupe', N'CoilRegister', N'Bit', 2280, 2280, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'ConsigneV', N'HoldingRegister', N'Int', 2291, 2291, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'ConsigneA', N'HoldingRegister', N'Int', 2301, 2301, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'LectureV', N'HoldingRegister', N'Int', 2311, 2311, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'LectureA', N'HoldingRegister', N'Int', 2320, 2320, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'Mode', N'HoldingRegister', N'int', 2350, 2350, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'OnOff', N'CoilRegister', N'Bit', 2300, 2300, 1)
INSERT INTO [dbo].[Registre] ([IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES ( 2, N'Nom', N'HoldingRegister', N'int', 2400, 2404, 5)

INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'Statique')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'Pulse')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'MesureTemperature')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'Temporisation')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'Rampe')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'CompteurAH')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 1, 1, 1, 1, N'Cyclage')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (1, 1, 1, 1, N'Prevent')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 2, 2, 3, 2, N'Mecanique')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 2, 2, 3, 2, N'Pulse')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 2, 2, 2, 2, N'MesureTemperature')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 3, 3, 2, 2, N'Temporisation')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 3, 3, 2, 2, N'Rampe')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 3, 3, 2, 2, N'CompteurAH')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 4, 4, 3, 2, N'Cyclage')
INSERT INTO [dbo].[Options] ([IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES ( 4, 4, 3, 2, N'Prevent');

INSERT INTO [dbo].[Utilisateur] ([Nom], [Mdp]) VALUES ( N'Ayan', N'laptop')

INSERT INTO [dbo].[Historique] ([DateDebut], [IdRedresseur], [IdUtilisateur], [IdRecette], [DateFin], [OrdreFabrication], [EtatFin],  [Type]) VALUES ( N'2019-01-03 12:00:00', 1, 1, 1, N'2019-02-03 12:00:00', N'000567', N'Stopped_by_user', N'Manuel')

INSERT INTO [dbo].[HistoriqueData] ([IdHistorique]) VALUES ( 1);

-- End of file.

