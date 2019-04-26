using System.Collections.ObjectModel;

namespace AcoreApplication.Model
{
    public interface IRegistreService
    {
        ObservableCollection<DataService.Registre> GetAllData();
        ObservableCollection<DataService.Registre> GetAllData(int redId);
        bool Insert();
        bool Insert(DataService.Registre registre);
        bool Delete(DataService.Registre registre);
        bool Update(DataService.Registre registre);
    }
}