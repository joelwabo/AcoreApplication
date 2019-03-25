using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static AcoreApplication.Model.Constantes;
using System.Collections.ObjectModel;
using static AcoreApplication.Model.Redresseur;
using System.Threading;

namespace AcoreApplication.DataService
{
    public partial class Historique
    {
        #region ATTRIBUTS
        bool Recording = false;
        private Thread RedresseurPoolingTask { get; set; }
        #endregion

        #region CONSTRUCTEUR(S)/DESTRUCTEUR(S)
        public Historique(bool recording)
        {
            Recording = recording;
        }
        #endregion

        #region METHODS

        #endregion
    }
}
