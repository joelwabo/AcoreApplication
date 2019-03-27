using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;
using static AcoreApplication.Model.Recette;
using System;
using GalaSoft.MvvmLight.Ioc;
using AcoreApplication.Model;

namespace AcoreApplication.DataService
{
    public partial class Process
    {
        #region ATTRIBUTS
        public ObservableCollection<Model.Recette> Recettes { get; set; }
        #endregion 

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)

        #endregion

        #region METHODES

        #endregion
    }
}
