-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2019-03-04 13:17:16.977

-- tables
-- Table: Automate
CREATE TABLE Automate (
    Id int  NOT NULL,
    Mode nvarchar(50)  NOT NULL,
    IpAdresse nvarchar(50)  NOT NULL,
    CONSTRAINT Automate_pk PRIMARY KEY  (Id)
);

-- Table: Historique
CREATE TABLE Historique (
    Date datetime  NOT NULL,
    IdRedresseur int  NOT NULL,
    IdUtilisateur int  NOT NULL,
    OrdreFabrication nvarchar(50)  NOT NULL,
    EtatFin nvarchar(50)  NULL,
    DateFin datetime  NULL,
    CONSTRAINT Historique_pk PRIMARY KEY  (Date)
);

-- Table: Options
CREATE TABLE Options (
    Id int  NOT NULL,
    IdRedresseur int  NOT NULL,
    IdSegment int  NOT NULL,
    IdRecette int  NOT NULL,
    IdProcess int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    CONSTRAINT Options_pk PRIMARY KEY  (Id)
);

-- Table: Process
CREATE TABLE Process (
    Id int  NOT NULL,
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
    Id int  NOT NULL,
    IdProcess int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    Cyclage int  NOT NULL,
    SegCours int  NULL,
    TempsRestant time(7)  NULL,
    CONSTRAINT id PRIMARY KEY  (Id)
);

-- Table: Redresseur
CREATE TABLE Redresseur (
    Id int  NOT NULL,
    IdProcess int  NOT NULL,
    IdAutomate int  NOT NULL,
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
    Temporisation bit  NULL,
    TempsOn int  NULL,
    TempsOff int  NULL,
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
    Id int  NOT NULL,
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
    Id int  NOT NULL,
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
    Id int  NOT NULL,
    Nom nvarchar(50)  NOT NULL,
    Mdp nvarchar(50)  NOT NULL,
    CONSTRAINT Utilisateur_pk PRIMARY KEY  (Id)
);

-- foreign keys
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
    FOREIGN KEY (IdAutomate)
    REFERENCES Automate (Id);

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

INSERT INTO [dbo].[Process] ([Id], [Nom], [UMax], [IMax], [Pulse], [Inverseur], [AH]) VALUES (1, N'Anodisation', 500, 500, 0, 0, 0)
INSERT INTO [dbo].[Process] ([Id], [Nom], [UMax], [IMax], [Pulse], [Inverseur], [AH]) VALUES (2, N'Cathodisation', 500, 500, 0, 0, 0)

INSERT INTO [dbo].[Recette] ([Id], [IdProcess], [Nom], [Cyclage], [SegCours], [TempsRestant]) VALUES (1, 1, N'Rec1', 0, 1, N'0:0:59')
INSERT INTO [dbo].[Recette] ([Id], [IdProcess], [Nom], [Cyclage], [SegCours], [TempsRestant]) VALUES (2, 1, N'Rec2', 2, 1, N'0:30:59')
INSERT INTO [dbo].[Recette] ([Id], [IdProcess], [Nom], [Cyclage], [SegCours], [TempsRestant]) VALUES (3, 2, N'Rec3', 0, 1, N'12:30:59')

INSERT INTO [dbo].[Segment] ([Id], [IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (1, 1, N'Seg1', 0,  N'Anodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')
INSERT INTO [dbo].[Segment] ([Id], [IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (2, 1, N'Seg1', 0,  N'Cathodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')
INSERT INTO [dbo].[Segment] ([Id], [IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (3, 2, N'Seg1', 0,  N'Anodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')
INSERT INTO [dbo].[Segment] ([Id], [IdRecette], [Nom], [Etat], [Type], [Duree], [ConsigneDepartV], [ConsigneDepartA], [ConsigneArriveeV], [ConsigneArriveeA], [TempsRestant], [Pulse], [Temporisation], [TempsOn], [TempsOff], [AH], [CompteurAH], [CalibreAH], [Rampe], [DureeRampe]) VALUES (4, 3, N'Seg1', 0,  N'Cathodique', N'0:0:10', 50, 10, 10, 0, N'0:0:10', 0, 0, N'0:0:10', N'0:0:10', 0, 0, N'A_H', 0, N'0:0:10')

INSERT INTO [dbo].[Automate] ([Id],  [Mode], [IpAdresse]) VALUES (1, N'Remote', N'192.168.1.111')
INSERT INTO [dbo].[Automate] ([Id],  [Mode], [IpAdresse]) VALUES (2,  N'CommandeLocal', N'192.168.1.111')

INSERT INTO [dbo].[Redresseur] ([Id], [IdProcess], [IdAutomate], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (1, 1, 1, 0, 0, N'Manuel', N'Anodique', 1, 30, 1, 400, 0, 0, 0, 0, 1, 1, 1000, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([Id], [IdProcess], [IdAutomate], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (2, 2, 2, 0, 0, N'Manuel', N'Cathodique', 1, 500, 1, 400, 0, 80, 0, 0, 1, 1, 200, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([Id], [IdProcess], [IdAutomate], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (3, 2, 1, 0, 0, N'Recette', N'Anodique', 1, 0, 1, 0, 0, 90, 9, 0, 1, 0, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([Id], [IdProcess], [IdAutomate], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (4, 1, 2, 0, 0, N'Supervision', N'Cathodique', 1, 99999, 1, 65, 0, 2, 0, 0, 1, 0, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([Id], [IdProcess], [IdAutomate], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (5, 1, 1, 0, 0, N'Lecture', N'Anodique', 1, 333, 1, 5555, 20, 0, 0, 0, 1, 1, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)
INSERT INTO [dbo].[Redresseur] ([Id], [IdProcess], [IdAutomate], [OnOff], [MiseSousTension], [Etat], [Type], [CalibreV], [UMax], [CalibreA], [IMax], [ConsigneV], [ConsigneA], [LectureV], [LectureA], [Temperature], [AH], [CompteurAH], [CalibreAH], [Inverseur], [Prevent], [Pulse], [Temporisation], [TempsOn], [TempsOff], [DureeTempo], [DureeRestante], [Rampe], [DureeRampe], [FichierIncident], [Defaut]) VALUES (6, 1, 1, 0, 0, N'Manuel', N'Anodique', 1, 30, 0, 400, 0, 20, 1, 0, 1, 0, 0, N'A_H', 0, 0, 1, 1, 100, 200, N'12:30:59', N'12:30:59', 1, N'01:00:00', NULL, 1)

INSERT INTO [dbo].[Registre] ([Id], [IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES (1, 1, N'ConsigneV', N'HoldingRegister', N'Int', 1010, 1010, 1)
INSERT INTO [dbo].[Registre] ([Id], [IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES (2, 1, N'ConsigneA', N'HoldingRegister', N'Int', 1011, 1011, 1)
INSERT INTO [dbo].[Registre] ([Id], [IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES (3, 1, N'LectureV', N'HoldingRegister', N'Int', 1006, 1006, 1)
INSERT INTO [dbo].[Registre] ([Id], [IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES (4, 1, N'LectureA', N'HoldingRegister', N'Int', 1007, 1007, 1)
INSERT INTO [dbo].[Registre] ([Id], [IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES (5, 1, N'OnOff', N'HoldingRegister', N'Int', 1008, 1008, 1)
INSERT INTO [dbo].[Registre] ([Id], [IdRedresseur], [Nom], [TypeModbus], [Type], [AdresseDebut], [AdresseFin], [NumBit]) VALUES (6, 1, N'Defaut', N'HoldingRegister', N'Int', 1009, 1009, 1)

INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (1, 1, 1, 1, 1, N'Statique')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (3, 1, 1, 1, 1, N'Pulse')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (4, 1, 1, 1, 1, N'MesureTemperature')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (5, 1, 1, 1, 1, N'Temporisation')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (6, 1, 1, 1, 1, N'Rampe')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (7, 1, 1, 1, 1, N'CompteurAH')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (8, 1, 1, 1, 1, N'Cyclage')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (9, 1, 1, 1, 1, N'Prevent')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (10, 2, 2, 3, 2, N'Mecanique')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (11, 2, 2, 3, 2, N'Pulse')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (12, 2, 2, 2, 2, N'MesureTemperature')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (13, 3, 3, 2, 2, N'Temporisation')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (14, 3, 3, 2, 2, N'Rampe')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (15, 3, 3, 2, 2, N'CompteurAH')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (16, 4, 4, 3, 2, N'Cyclage')
INSERT INTO [dbo].[Options] ([Id], [IdRedresseur], [IdSegment], [IdRecette], [IdProcess], [nom]) VALUES (17, 4, 4, 3, 2, N'Prevent');;

-- End of file.

