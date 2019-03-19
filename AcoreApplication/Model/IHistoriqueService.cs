
using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public interface IHistoriqueService
    {
        ObservableCollection<Historique> GetAllData();
        bool InsertHistorique();
        bool UpdateHistorique(Historique historique);
    }
}
