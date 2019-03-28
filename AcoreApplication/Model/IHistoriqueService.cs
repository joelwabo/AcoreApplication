using AcoreApplication.DataService;
using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public interface IHistoriqueService
    {
        ObservableCollection<Historique> GetAllData();
        bool Insert(Historique historique);
    }
}
