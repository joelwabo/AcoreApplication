using System.Threading;

namespace AcoreApplication.DataService
{
    public partial class Historique
    {
        #region ATTRIBUTS
        public Model.Recette Recette2 = new Model.Recette(); //A supprimer
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
