using System.Collections.ObjectModel;
using AcoreApplication.DataService;

namespace AcoreApplication.Model
{
    public interface IRegistreService
    {
        ObservableCollection<Registre> GetAllData();
        bool Insert();
        bool Insert(Registre registre);
        bool Delete(Registre registre);
        bool Update(Registre registre);
    }
}