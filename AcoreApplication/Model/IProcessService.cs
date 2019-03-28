using System.Collections.ObjectModel;
using AcoreApplication.DataService;

namespace AcoreApplication.Model
{
    public interface IProcessService
    {
        ObservableCollection<Process> GetAllData();
        bool Insert();
        bool Delete(Process process);
        bool Update(Process process);
    }
}